namespace Myra.Application.Exception
{
    public class UnauthorizedException : IOException
    {
        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
