using Newtonsoft.Json;
using PaymentSimplify.Application.CentralBank.Commands.Response;
using PaymentSimplify.Application.Common.Interfaces;

namespace PaymentSimplify.Api.Services;

public class CentralBankService : ICentralBankService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CentralBankService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> TransactionAuthorized()
    {
        var requestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://run.mocky.io/v3/8fafdd68-a090-496f-8c9a-3442cf30dae6");
        
        var client = _httpClientFactory.CreateClient();

        var response = await client.SendAsync(requestMessage);

        var content = await response.Content.ReadAsStringAsync();

        var deserealize = JsonConvert.DeserializeObject<CentralBankResponse>(content) ?? new CentralBankResponse();

        return deserealize.Message.Equals("Autorizado");
    }
}