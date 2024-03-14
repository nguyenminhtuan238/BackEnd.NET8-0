namespace ProjectCV.Server.Helpers
{
    public class ExeptionLogin:Exception
    {
        public ExeptionLogin():base("Username and password is wrong") { }
    }
}
