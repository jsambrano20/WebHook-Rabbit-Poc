using Mechanic_API_Webhook_poc.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using User_API_Webhook_poc.Domain.Util.ClassGenerics;

namespace Mechanic_API_Webhook_poc.Domain.Dto
{
    public class VeiculoUpdate
    {
        public int Id { get; set; }
        public StatusEnum Status { get; set; }
        public string Comentario { get; set; }
        public decimal ValorOrcamento { get; set; }
    }
}
