using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Mechanic_API_Webhook_poc.Domain.Enum;
using User_API_Webhook_poc.Domain.Util.ClassGenerics;

namespace Mechanic_API_Webhook_poc.Domain.Entities
{
    [Table("Veiculo")]
    public class EntityVeiculo
    {
        [Key]
        public int Id { get; set; }
    
        [Required]
        public Cpf Cpf{ get; set; }

        [Column("Modelo")]
        [StringLength(100)]
        public string Modelo { get; set; }

        [Column("Marca", TypeName = "varchar(50)")]
        public MarcaEnum Marca { get; set; }

        [Column("Ano")]
        public int Ano { get; set; }

        [Column("Status")]
        public StatusEnum Status { get; set; }

        [Column("Comentario")]
        [StringLength(1000)]
        public string Comentario { get; set; }

        [Column("ValorOrcamento", TypeName = "decimal(18,2)")]
        [Range(0, 999999.99, ErrorMessage = "O valor do orçamento deve estar entre 0 e 999.999,99")]
        public decimal ValorOrcamento { get; set; }
    }
}
