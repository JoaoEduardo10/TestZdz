using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Dtos.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace InfrastrutureTest
{
    public class OrderControllerTest : IClassFixture<InfrastrutureApplicationFactory<Program>>
    {
        private readonly HttpClient _Client;
        private readonly InfrastrutureApplicationFactory<Program> _Factory;

        public OrderControllerTest(InfrastrutureApplicationFactory<Program> factory)
        {
            _Factory = factory;
            _Client = _Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task Should_Create_Order()
        {
            var produtoRequest = new ProductRequestDto("Bola", 10);

            var body = JsonSerializer.Serialize(produtoRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _Client.PostAsync("api/v1/product", stringContent);

            var orderRequest = new OrderRequestDto(1, 7);

            var bodyOrder = JsonSerializer.Serialize(orderRequest);

            var stringContentOrder = new StringContent(bodyOrder, Encoding.UTF8, "application/json");

            var responseOrder = await _Client.PostAsync("api/v1/order", stringContentOrder);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, responseOrder.StatusCode);
        }

        [Fact]
        public async Task Should_Not_Create_An_Order_By_Adding_A_NonExistent_Product()
        {

            var order = new OrderRequestDto(90, 7);

            var body = JsonSerializer.Serialize(order);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _Client.PostAsync("api/v1/order", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<object>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            Assert.Equal("Produto não encontrado", baseResponse.ErrorMessage.Message);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Should_Not_Create_A_Request_By_Adding_A_Value_Less_Than_Or_Even_To_Zero()
        {
            await CreateOrder();

            var orderWithQuantityNegative = new OrderRequestDto(1, -1);

            var bodyWithQuantityNegative = JsonSerializer.Serialize(orderWithQuantityNegative);

            var stringContentWithQuantityNegative = new StringContent(bodyWithQuantityNegative, Encoding.UTF8, "application/json");

            var responseWithQuantityNegative = await _Client.PostAsync("api/v1/order", stringContentWithQuantityNegative);

            Assert.Equal(HttpStatusCode.BadRequest, responseWithQuantityNegative.StatusCode);
        }


        [Fact]
        public async Task Should_Get_All_Orders()
        {
            await CreateOrder();

            var responseAll = await _Client.GetAsync("api/v1/order");

            var responseContent = await responseAll.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<List<Order>>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.StrictEqual(2, baseResponse!.Result.Count);
        }

        [Fact]
        public async Task Should_Get_Order_By_Id()
        {

            var response = await _Client.GetAsync("api/v1/order/1");

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<Order>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Pedido Encontrado com sucesso!", baseResponse.Message);
        }


        [Fact]
        public async Task Should_Delete_Order_By_Id()
        {
            var order = await CreateOrder();

            var response = await _Client.DeleteAsync("api/v1/order/" + order.Result.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Should_Update_Order_By_Id()
        {
            var produtoRequest = new ProductRequestDto("Bola", 10);

            var bodyProduct = JsonSerializer.Serialize(produtoRequest);

            var ProductStringContent = new StringContent(bodyProduct, Encoding.UTF8, "application/json");

            await _Client.PostAsync("api/v1/product", ProductStringContent);

            var orderRequest = new OrderRequestDto(1, 7);

            var bodyOrder = JsonSerializer.Serialize(orderRequest);

            var stringContentOrder = new StringContent(bodyOrder, Encoding.UTF8, "application/json");

            var responseOrder = await _Client.PostAsync("api/v1/order", stringContentOrder);


            var newOrder = new OrderRequestDto(1, 40);
            var body = JsonSerializer.Serialize(newOrder);
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");


            var response = await _Client.PutAsync("api/v1/order/1", stringContent);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<BaseResponse<Order>> CreateOrder()
        {
            var produtoRequest = new ProductRequestDto("Bola", 10);

            var body = JsonSerializer.Serialize(produtoRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            await _Client.PostAsync("api/v1/product", stringContent);

            var orderRequest = new OrderRequestDto(2, 7);

            var bodyOrder = JsonSerializer.Serialize(orderRequest);

            var stringContentOrder = new StringContent(bodyOrder, Encoding.UTF8, "application/json");

             await _Client.PostAsync("api/v1/order", stringContentOrder);

            var response = await _Client.GetAsync("api/v1/order/2");

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<Order>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return baseResponse!;
        }


    }
}
