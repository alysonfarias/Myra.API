namespace Myra.Application.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreInjectionAttribute : Attribute
    {
        public bool Ignore { get; set; }

        public IgnoreInjectionAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }
    }
}

