using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels.SystemNotificationModels
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

    }
}
