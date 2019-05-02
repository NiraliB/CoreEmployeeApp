using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Models
{
    public static class RoleExtensions 
    {
        public static string GetRoleName(this RoleTypes role)
        {
            return role.ToString();
        }
    }

    public enum RoleTypes
    {
        Admin,
        User1,
        SubUser
    }

}
