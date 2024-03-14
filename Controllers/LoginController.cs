using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServicescs _loginServicescs;

        public LoginController(ILoginServicescs loginServicescs)
        {
           
            _loginServicescs = loginServicescs;
          
        }
        [HttpPost]
        public async Task<ActionResult<object>> Login(User user)
        {
            try
            {
                return await _loginServicescs.Login(user);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
