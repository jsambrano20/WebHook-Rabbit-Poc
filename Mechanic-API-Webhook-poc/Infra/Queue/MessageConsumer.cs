using MassTransit;
using Mechanic_API_Webhook_poc.Domain.Dto;
using Mechanic_API_Webhook_poc.Domain.Dto.Event;
using Mechanic_API_Webhook_poc.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace Mechanic_API_Webhook_poc.Infra.Queue
{
    public class MessageConsumer : IConsumer<IStatusVeiculoAlteradoEvent>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _context;

        public MessageConsumer(IHttpClientFactory httpClientFactory,
            AppDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public async Task Consume(ConsumeContext<IStatusVeiculoAlteradoEvent> context)
        {
            var message = context.Message;

            Console.WriteLine($"[RabbitMQ] Veículo {message.VeiculoId} teve status alterado de {message.StatusAnterior} para {message.StatusAtual} em {message.DataAlteracao}");

            var payload = new
            {
                message.VeiculoId,
                message.StatusAnterior,
                message.StatusAtual,
                message.DataAlteracao
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var httpClient = new HttpClient(handler);

            // Implementar os a url via AppDbContext
            var response = await httpClient.PostAsync("https://localhost:4002/notificacao", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Falha ao chamar API externa: {response.StatusCode}");
            }
            else
            {
                Console.WriteLine($"✅ Notificação enviada com sucesso para a outra API");
            }
        }
    }
}
