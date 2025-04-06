namespace Mechanic_API_Webhook_poc.Domain.Dto.Event
{
    public interface IStatusVeiculoAlteradoEvent
    {
        int VeiculoId { get; }
        string StatusAnterior { get; }
        string StatusAtual { get; }
        DateTime DataAlteracao { get; }
    }
}
