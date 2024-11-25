using Microsoft.EntityFrameworkCore;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataBaseTablesModels.SystemNotificationModels;
using SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data
{
    public class SMS_LMS_DBContext : DbContext
    {
        public SMS_LMS_DBContext(DbContextOptions<SMS_LMS_DBContext> options)
           : base(options)
        {
        }
        
        public DbSet<SMSConfigTbl> SMSConfigTbl { get; set; } = null!;
        public DbSet<SMSDataTbl> SMSDataTbl { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }



    }
}
