using System.ComponentModel.DataAnnotations;

namespace HRWebApp.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Action { get; set; } // Create, Update, Delete, Login, etc.

        [Required]
        public string EntityType { get; set; } // Employee, Department, etc.

        public string EntityId { get; set; } // ID of the affected entity
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string IPAddress { get; set; }
    }
}
