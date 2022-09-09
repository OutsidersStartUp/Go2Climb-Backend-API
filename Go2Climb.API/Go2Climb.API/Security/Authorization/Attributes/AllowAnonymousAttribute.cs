using System;

namespace Go2Climb.API.Security.Authorization.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
        
    }
}