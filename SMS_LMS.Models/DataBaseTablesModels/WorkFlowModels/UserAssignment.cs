using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class UserAssignment
    {
        [Key]
        public int Id { get; set; }
        public int? StepId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public Step Step { get; set; }
        public ApplicationRole Role { get; set; }
        public string Criteria { get; set; }
    }
}
