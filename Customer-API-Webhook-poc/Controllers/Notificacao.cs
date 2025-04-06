using Customer_API_Webhook_poc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customer_API_Webhook_poc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly string _caminhoArquivo = Path.Combine(
            Directory.GetCurrentDirectory(), "Logs", "notificacoes.txt");
        [HttpPost]
        public IActionResult ReceberNotificacao([FromBody] StatusVeiculoWebhookDto dto)
        {
            var log = $"📩 {DateTime.Now:yyyy-MM-dd HH:mm:ss} - Veículo {dto.VeiculoId} alterado de {dto.StatusAnterior} para {dto.StatusAtual}";
            Console.WriteLine(log);

            try
            {
                // Garante que o diretório exista
                var pasta = Path.GetDirectoryName(_caminhoArquivo);
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Escreve no arquivo fixo
                System.IO.File.AppendAllText(_caminhoArquivo, log + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao escrever no arquivo: {ex.Message}");
                return StatusCode(500, "Erro ao registrar notificação");
            }

            return Ok();
        }
    }

}
