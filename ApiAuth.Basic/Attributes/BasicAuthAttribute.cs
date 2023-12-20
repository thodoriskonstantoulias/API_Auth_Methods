using ApiAuth.Basic.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Basic.Attributes
{
    public class BasicAuthAttribute : ServiceFilterAttribute
    {
        public BasicAuthAttribute() : base(typeof(BasicAuthFilter))
        {
        }
    }
}
