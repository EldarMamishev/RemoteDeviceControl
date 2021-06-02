using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Data.Contracts.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Connection;
using Services.Facades.Base;
using ViewModel.LogEntity;
using ViewModel.Device;
using ViewModel.Shared;
using WebApi.Controllers.Base;
using ViewModel.DeviceType;
using ViewModel.Field;

namespace WebApi.Controllers
{
    [ApiController]
    public class DeviceTypeController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public DeviceTypeController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade)
            : base(userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mappersFacade = mappersFacade;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<DeviceTypeModel>> GetDeviceType(int deviceTypeId)
        {
            var deviceType = this.unitOfWork.DeviceTypeRepository.GetById(deviceTypeId);

            var fields = new List<FieldModel>();
            foreach (var field in deviceType.Fields)
            {
                var possibleValues = new List<FieldPossibleValueModel>();
                foreach (var possibleValue in field.FieldPossibleValues)
                {
                    possibleValues.Add(new FieldPossibleValueModel
                    {
                        Id = possibleValue.Id,
                        Type = possibleValue.Type,
                        Value = possibleValue.Value
                    });
                }

                fields.Add(new FieldModel()
                {
                    Id = field.Id,
                    Name = field.Name,
                    Type = field.Type,
                    PossibleValues = possibleValues
                });
            }

            var result = new DeviceTypeModel()
            {
                Id = deviceType.Id,
                Name = deviceType.Name,
                Fields = fields
            };

            return Ok(result);
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<DeviceTypeModel>> GetDeviceTypes()
        {
            var deviceTypes = this.unitOfWork.DeviceTypeRepository.Get();

            var result = new List<DeviceTypeModel>();

            foreach (var deviceType in deviceTypes)
            {
                result.Add(new DeviceTypeModel()
                {
                    Id = deviceType.Id,
                    Name = deviceType.Name
                });
            }

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddDeviceType(DeviceTypeModel model)
        {
            var deviceType = new DeviceType()
            {
                Name = model.Name
            };

            var fields = new List<Field>();
            foreach(var fieldModel in model.Fields)
            {

                var possibleValues = new List<FieldPossibleValue>();
                foreach (var valueModel in fieldModel.PossibleValues)
                {
                    possibleValues.Add(new FieldPossibleValue
                    {
                        Type = valueModel.Type,
                        Value = valueModel.Value
                    });
                }

                fields.Add(new Field() 
                { 
                    Name = fieldModel.Name,
                    Type = fieldModel.Type,
                    DeviceType = deviceType,
                    FieldPossibleValues = possibleValues
                });
            }

            deviceType.Fields = fields;

            await this.unitOfWork.DeviceTypeRepository.Add(deviceType);
            await this.unitOfWork.Commit();

            return Ok(deviceType.Id);
        }
    }
}
