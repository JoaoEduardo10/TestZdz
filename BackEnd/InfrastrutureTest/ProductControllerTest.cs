using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Dtos.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace InfrastrutureTest
{
    public class ProductControllerTest : IClassFixture<InfrastrutureApplicationFactory<Program>>
    {
        private readonly HttpClient _Client;
        private readonly InfrastrutureApplicationFactory<Program> _Factory;

        public ProductControllerTest(InfrastrutureApplicationFactory<Program> factory)
        {
            _Factory = factory;
            _Client = _Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task Should_Created_Product()
        {
            var produto = new ProductRequestDto("Bola", 10);

            var body = JsonSerializer.Serialize(produto);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _Client.PostAsync("api/v1/product", stringContent);


            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        [Fact]
        public async Task Should_Not_Create_The_Product_Pro_Add_Name_With_Less_Than_Three_Characters()
        {
            var produto = new ProductRequestDto("B", 10);

            var body = JsonSerializer.Serialize(produto);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _Client.PostAsync("api/v1/product", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<object>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Minimo de caracteres são 3", baseResponse.ErrorMessage.Message);
        }

        [Fact]
        public async Task Should_Not_Create_The_Product_Pro_Add_The_Value_Negative()
        {
            var produto = new ProductRequestDto("Bola", -1);

            var body = JsonSerializer.Serialize(produto);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _Client.PostAsync("api/v1/product", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<Product>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Valor não pode ser menor ao igual a zero", baseResponse.ErrorMessage.Message);
        }

        [Fact]
        public async Task Should_Get_All_Product()
        {
            var response = await _Client.GetAsync("api/v1/product");

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<List<Product>>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Should_Get_Product_By_Id()
        {
            var response = await _Client.GetAsync("api/v1/product/2");

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<object>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Sucesso na busca do produto!", baseResponse.Message);
        }

        [Fact]
        public async Task Should_Put_Product_By_Id()
        {
            var product = new ProductRequestDto("Bola de test", 1000);

            

            var response = await _Client.GetAsync("api/v1/product/1");

            var responseContent = await response.Content.ReadAsStringAsync();

            var baseResponse = JsonSerializer.Deserialize<BaseResponse<Product>>
                (responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(product.Value, baseResponse.Result.Value);
            Assert.NotEqual(product.Name, baseResponse.Result.Name);


            var body = JsonSerializer.Serialize(product);
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            await _Client.PutAsync("api/v1/product/1", stringContent);

            var newResponse = await _Client.GetAsync("api/v1/product/1");

            var newResponseContent = await newResponse.Content.ReadAsStringAsync();

            var actualBaseResponse = JsonSerializer.Deserialize<BaseResponse<Product>>
                (newResponseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(product.Value, actualBaseResponse.Result.Value);
            Assert.Equal(product.Name, actualBaseResponse.Result.Name);
        }

        [Fact]
        public async Task Should_Delete_Product_By_Id()
        {
            var response = await _Client.DeleteAsync("api/v1/product/1");

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
