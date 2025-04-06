using System.ComponentModel;

namespace Mechanic_API_Webhook_poc.Domain.Enum
{
    public enum StatusEnum
    {
        [Description("Criado")]
        Criado = 1,

        [Description("Iniciado Orçamento")]
        Iniciado_Orcamento = 2,

        [Description("Aguardando Aprovação")]
        Aguardando_Aprovacao = 3,

        [Description("Orçamento Recusado")]
        Orcamento_Recusado = 4,

        [Description("Orçamento Aprovado")]
        Orcamento_Aprovado = 5,

        [Description("Serviço Iniciado")]
        Servico_Iniciado = 6,

        [Description("Serviço Finalizado")]
        Servico_Finalizado = 7,

        [Description("Retirada Disponível")]
        Retirada_Disponivel = 8,

        [Description("Pagamento Realizado")]
        Pagamento_Realizado = 9
    }
}
