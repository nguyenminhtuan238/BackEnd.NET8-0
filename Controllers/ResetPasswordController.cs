using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ProjectCV.Server.Helpers;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;
using ProjectCV.Server.Services;
using System.Security.Claims;

namespace ProjectCV.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {

        private readonly IResetPasswordServices _resetPassword;

        public ResetPasswordController(IResetPasswordServices resetPasswordServices)
        {

            _resetPassword = resetPasswordServices;

        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<object>> Login(ResetPasswordHelpers reset)
        {
            try
            {
                var id = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                return await _resetPassword.ResetPassword(reset.NewPassword, reset.Password, id);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
