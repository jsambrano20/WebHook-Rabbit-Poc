namespace Customer_API_Webhook_poc.Models
{
    public class StatusVeiculoWebhookDto
    {
        public int VeiculoId { get; set; }
        public string StatusAnterior { get; set; }
        public string StatusAtual { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
