using Examen_TH.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Utilidades {
    public class AppDBContext : DbContext {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {

        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}
