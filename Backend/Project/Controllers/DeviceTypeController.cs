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
using System.Text;

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
        public ActionResult<DeviceTypeModel> GetDeviceType(int deviceTypeId)
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
                        Name = possibleValue.Name
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
        public ActionResult<IEnumerable<DeviceTypeGridModel>> GetDeviceTypes()
        {
            var deviceTypes = this.unitOfWork.DeviceTypeRepository.Get().ToList();

            var result = new List<DeviceTypeGridModel>();

            foreach (var deviceType in deviceTypes)
            {
                var fields = new StringBuilder();

                foreach (var field in deviceType.Fields)
                {
                    fields.AppendLine($"{field.Id} Name: {field.Name} - Type: {field.Type}");
                }

                result.Add(new DeviceTypeGridModel()
                {
                    Id = deviceType.Id,
                    Name = deviceType.Name,
                    Fields = fields.ToString()
                });
            }

            return Ok(result);
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<GridFilterModel>> GetDeviceTypesFilter()
        {
            var deviceTypes = this.unitOfWork.DeviceTypeRepository.Get().ToList();

            var deviceTypeModels = new List<GridFilterModel>();
            var deviceTypeFilters = new List<GridFilterModel>();

            foreach (var deviceType in deviceTypes)
            {
                deviceTypeModels.Add(new GridFilterModel()
                {
                    Value = deviceType.Id,
                    Title = deviceType.Name
                });

                deviceTypeFilters.Add(new GridFilterModel()
                {
                    Value = deviceType.Name,
                    Title = deviceType.Name
                });
            }

            var result = new DeviceGridFilters()
            {
                DeviceTypeFilters = deviceTypeFilters,
                DeviceTypes = deviceTypeModels
            };
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
            foreach (var fieldModel in model.Fields)
            {
                var possibleValues = new List<FieldPossibleValue>();
                if (fieldModel.PossibleValues != null && fieldModel.PossibleValues.Any())
                {                     
                    foreach (var valueModel in fieldModel.PossibleValues)
                    {
                        possibleValues.Add(new FieldPossibleValue
                        {
                            Name = valueModel.Name
                        });
                    }
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
