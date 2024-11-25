using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        public int? StageId { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public int? Sequence { get; set; }
        public bool? IsHumanTask { get; set; }

        public virtual Stage? Stage { get; set; }
        public virtual ICollection<WorkFlowActions>? Actions { get; set; }
        public virtual ICollection<UserAssignment>? Assignments { get; set; }
    }
}
