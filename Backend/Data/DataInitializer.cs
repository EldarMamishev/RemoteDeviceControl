using Data.Contracts;

namespace Data
{
    public class DataInitializer : IDataInitializer
    {
        private readonly AppDbContext _context;

        public DataInitializer(AppDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();
        }

        //private void InitializeRoles()
        //{
        //    AddRole(Settings.AdminRoleName, UseCase.CreateDevice);
        //    AddRole(Settings.PlainRoleName, UseCase.AssignDevice, UseCase.CreateShelving, UseCase.CreateProduct);

        //    _unitOfWork.SaveChanges();
        //}

        //private void AddRole(string roleName, params UseCase[] useCases)
        //{
        //    _unitOfWork.Repository<Role>().Add(new Role
        //    {
        //        Name = roleName,
        //        RoleUseCases = useCases.Select(uc => new RoleUseCase { UseCase = uc }).ToList()
        //    });
        //}
    }
}