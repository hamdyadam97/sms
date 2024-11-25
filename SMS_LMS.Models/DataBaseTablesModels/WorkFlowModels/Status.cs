using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }

        public virtual ICollection<WorkflowInstance>? WorkflowInstances { get; set; }
        public virtual ICollection<Transition>? Transitions { get; set; }
    }
}
