using Newtonsoft.Json;
using Practice_Store.Application.Interfaces.ZarinPal;
using System.Text;

namespace Practice_Store.Infrastructure.ZarinPal
{
    public class ManageZarinPal : IManageZarinPal
    {
        private readonly HttpClient client;
        public ManageZarinPal()
        {
            client = new HttpClient();
        }

        public async Task<ResultRequestToZarinPalDto> RequestToZarinPal(RequestToZarinPalDto Request)
        {
            var requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
            var jsonContent = JsonConvert.SerializeObject(new
            {
                merchant_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                amount = Request.Amount,
                description = $"خرید پوشاک از سایت به شماره",
                callback_url = $"http://localhost:5215/order/ValidateRequestOrder?Guid={Request.OrderRequestGuid}&Shipping={Request.Shipping}",
            });
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);

            var Result = new ResultRequestToZarinPalDto
            {
                Authority = responseJson.data.authority
            };
            return Result;
        }

        public async Task<ValidateRequestFromZarinPalDto> ValidateRequestFromZarinPal(ResultValidateRequestFromZarinPalDto Request)
        {
            var requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
            var jsonContent = JsonConvert.SerializeObject(new
            {
                merchant_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                amount = Request.Amount,
                authority = Request.Authority,
            });
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
            int Code = responseJson.data?.code ?? responseJson.errors.code;

            var Result = new ValidateRequestFromZarinPalDto
            {
                RefId = responseJson.data?.ref_id ?? 0,
                Code = Code,
            };
            return Result;
        }
    }
}
