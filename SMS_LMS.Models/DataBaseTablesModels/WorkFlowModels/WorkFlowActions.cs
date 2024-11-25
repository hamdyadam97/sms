using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class WorkFlowActions
    {
        [Key]
        public int Id { get; set; }
        public int? StepId { get; set; }
        public string? Name { get; set; }
        public int? NextStatusId { get; set; }

        public virtual Step? Step { get; set; }
        public virtual Status? NextStatus { get; set; }
    }
}
