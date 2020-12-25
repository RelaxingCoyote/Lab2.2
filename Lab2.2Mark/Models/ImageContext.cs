using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab2._2Mark.Models
{
    public class ImageContext: DbContext
    {
     
            public DbSet<Image> Images { set; get; }
    }
}