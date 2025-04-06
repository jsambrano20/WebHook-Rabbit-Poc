using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Mechanic_API_Webhook_poc.Domain.Enum;

namespace Mechanic_API_Webhook_poc.Domain.Entities
{
    [Table("WebHooks")]
    public class EntityWebHooks
    {
        [Key]
        public int Id { get; set; }

        [Column("IsAtivo")]
        public bool IsAtivo { get; set; }

        [Column("Url")]
        [StringLength(1000)]
        public string Url { get; set; }
    }
}
