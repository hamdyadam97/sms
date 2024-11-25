using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class Transition
    {
        [Key]
        public int Id { get; set; }
        public int? FromActionId { get; set; }
        public int? ToStepId { get; set; }
        public string? Condition { get; set; }
        public int? StatusId { get; set; }
        public virtual WorkFlowActions? FromAction { get; set; }
        public virtual Step? ToStep { get; set; }
        public virtual Status? Status { get; set; }
    }
}
