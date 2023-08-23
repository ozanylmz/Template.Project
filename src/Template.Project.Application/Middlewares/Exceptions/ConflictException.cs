namespace Template.Project.Application.Middlewares.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string msg) : base(msg)
        { }
    }
}
