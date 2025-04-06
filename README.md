# WebHook-Rabbit-Poc

## 📦 Tecnologias Utilizadas

- .NET 9
- RabbitMQ
- MassTransit
- SQL Server
- Docker
- Swagger (para testes via interface)

---

## 🚀 Iniciando o Projeto

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)

---

## 🐳 Subindo os Containers com Docker

### 1. Clone o repositório

```bash
git clone https://github.com/jsambrano20/WebHook-Rabbit-Poc.git
cd WebHook-Rabbit-Poc
```

### 2. Suba os containers com Docker Compose

```bash
docker-compose up -d
```

Esse comando irá subir os seguintes serviços:

- **RabbitMQ**  
  - Porta AMQP: `5672`  
  - Painel: [http://localhost:15672](http://localhost:15672)  
  - Usuário/Senha: `guest` / `guest`

- **SQL Server**  
  - Porta: `1433`  
  - Usuário: `sa`  
  - Senha: `Your_password123`

---

## ⚙️ Configuração do Projeto

A aplicação está configurada para:

- Registrar o consumidor `MessageConsumer` usando **MassTransit**
- Conectar-se ao RabbitMQ e automaticamente criar os endpoints
- Persistir os dados em um banco SQL Server

### Configuração MassTransit

O `Program.cs` está configurado da seguinte forma:

```csharp
builder.Services.AddHttpClient();
var rabbitSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitSettings.Host, rabbitSettings.VirtualHost, h =>
        {
            h.Username(rabbitSettings.Username);
            h.Password(rabbitSettings.Password);
        });

        cfg.ConfigureEndpoints(context);
    });
});
```

### appsettings.Development.json

```json
"RabbitMQ": {
  "Host": "localhost",
  "Port": 5672,
  "VirtualHost": "/",
  "Username": "guest",
  "Password": "guest"
},
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=WebhookDb;User Id=sa;Password=Your_password123;"
}
```

---

## ▶️ Executando a Aplicação

### 1. Compile e rode o projeto WebApi

A API estará disponível em:

```
https://localhost:5001
```

### 2. Acesse o Swagger

```
https://localhost:5001/swagger
```

Você poderá testar os endpoints diretamente pela interface.

---

## 🧪 Testando o WebHook

1. **Criar WebHook**
   - Acesse o endpoint de criação de WebHook via Swagger.
   - Envie um payload com a URL desejada para receber as chamadas.

2. **Disparar Evento**
   - Use o endpoint que simula o envio de um evento.
   - A aplicação enviará o evento para uma fila no RabbitMQ.

3. **Consumir Evento**
   - O consumidor `MessageConsumer` será automaticamente ativado pelo MassTransit.
   - Ele consumirá a mensagem da fila e fará a chamada HTTP para o WebHook cadastrado.
    
4. **Criado txt que armazena as logs**
   - Após chamar o endpoint cadastrado no webHook ele cria um arquivo de texto,
  para garantir que está sendo recebido

---

## 🤝 Contribuição

Sinta-se à vontade para abrir uma issue ou PR com melhorias, correções ou novas ideias para a PoC.

---
