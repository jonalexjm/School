using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace School.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<School.Common.Models.User> Users { get; set; }
    }
}