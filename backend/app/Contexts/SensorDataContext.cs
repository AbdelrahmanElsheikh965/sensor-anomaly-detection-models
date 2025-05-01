using Early_warning;
using Early_warning.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Early_warning
{
    public class SensorDataContext : DbContext
    {
        public SensorDataContext(DbContextOptions<SensorDataContext> options) : base(options) { }

        public DbSet<SensorData> SensorsData { get; set; }  // تعريف جدول بيانات المستشعرات
        public DbSet<AnomalousData> AnomalousSensorData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add any additional configuration here
        }
    }
}
