using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class WorkflowInstance
    {
        [Key]
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? RequestTypeId { get; set; }

        public int? WorkflowId { get; set; }
        public int? CurrentStepId { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? StatusId { get; set; }

        public Workflow? Workflow { get; set; }
        public Step? CurrentStep { get; set; }
        public Status? Status { get; set; }

        public virtual ICollection<WorkflowHistory>? History { get; set; }


    }

}
