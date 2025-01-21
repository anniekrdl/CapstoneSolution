using Data.Interfaces;
using Data.Models;
namespace Data.Services
{

    public class LoginService : ILoginService
    {
        private readonly ICustomerDatabaseService _databaseService;

        public LoginService(ICustomerDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }



        public async Task<CustomerEntity?> Login(string UserName)
        {
            List<CustomerEntity> user = await _databaseService.SearchCustomer(UserName);
            if (user.Count > 0)
            {
                return user[0];
            }
            else
            {
                return null;
            }


        }



    }

}