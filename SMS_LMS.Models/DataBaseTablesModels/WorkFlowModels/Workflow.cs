using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels
{
    public class Workflow
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public string? Description { get; set; }
        public ICollection<Stage>? Stages { get; set; }
    }

}
