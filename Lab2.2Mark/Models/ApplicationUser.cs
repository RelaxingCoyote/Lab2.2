using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab2._2Mark.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
        }
    }
}