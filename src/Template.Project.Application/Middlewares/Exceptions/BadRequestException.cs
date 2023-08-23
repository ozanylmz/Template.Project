namespace Template.Project.Application.Middlewares.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        { }
    }
}
