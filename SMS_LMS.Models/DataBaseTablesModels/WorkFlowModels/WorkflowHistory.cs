using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class WorkflowHistory
    {
        [Key]
        public int Id { get; set; }
        public int? InstanceId { get; set; }
        public int? StepId { get; set; }
        public int? ActionId { get; set; }
        public int? StatusId { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime? PerformedAt { get; set; }
        public string? Comments { get; set; }
        // navigation properties
        public virtual WorkflowInstance? Instance { get; set; }
        public virtual Step? Step { get; set; }
        public virtual WorkFlowActions? Action { get; set; }
        public virtual Status? Status { get; set; }
    }
}
