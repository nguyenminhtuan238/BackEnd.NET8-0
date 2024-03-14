namespace ProjectCV.Server.IServices.IAccountservices
{
    public interface IResetPasswordServices
    {
        Task<object> ResetPassword(string newpassword, string password, int id);
    }
}
