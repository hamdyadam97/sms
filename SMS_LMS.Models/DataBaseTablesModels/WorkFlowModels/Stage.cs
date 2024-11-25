using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class Stage
    {
        [Key]
        public int Id { get; set; }
        public int? WorkflowId { get; set; }
        public string? Name { get; set; }
        public int? Sequence { get; set; }

        public virtual Workflow? Workflow { get; set; }
        public virtual ICollection<Step>? Steps { get; set; }
    }
}
