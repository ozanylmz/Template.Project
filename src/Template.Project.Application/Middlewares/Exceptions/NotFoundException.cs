namespace Template.Project.Application.Middlewares.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        { }
    }
}
