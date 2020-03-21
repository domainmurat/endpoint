using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using FluentAssertions;
using endpoint.EntityFrameworkCore.EntityFrameworkCore;
using endpoint.Application.Shared.Products.Dto;

namespace endpoint.Tests
{
    public class EndpointTest : IDisposable
    {
        private TestServer _server;

        public HttpClient Client { get; private set; }

        public EndpointTest()
        {
            SetUpClient();
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task CreateTestCase()
        {
            await SeedData();

            var createForm0 = GenerateCreateForm("Product6", 10);
            var response = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().BeEquivalentTo(201);

            var product6 = JsonConvert.DeserializeObject<ProductDto>(response.Content.ReadAsStringAsync().Result);
            product6.Id.Should().BeGreaterOrEqualTo(6);
        }

        [Fact]
        public async Task UpdateTestCase()
        {
            await SeedData();

            var createForm0 = GenerateCreateForm("Product6", 10);
            var response = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().BeEquivalentTo(201);

            var product6 = JsonConvert.DeserializeObject<ProductDto>(response.Content.ReadAsStringAsync().Result);
            product6.Id.Should().BeGreaterOrEqualTo(6);
        }

        private void SetUpClient()
        {

            var builder = new WebHostBuilder()
                .UseStartup<endpoint.Web.Host.Startup>()
                .ConfigureServices(services =>
                {
                    var context = new EndpointDbContext(new DbContextOptionsBuilder<EndpointDbContext>()
                        .UseSqlite("DataSource=:memory:")
                        .EnableSensitiveDataLogging()
                        .Options);

                    services.RemoveAll(typeof(EndpointDbContext));
                    services.AddSingleton(context);

                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();

                    // Clear local context cache
                    foreach (var entity in context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }

        private async Task SeedData()
        {

            // Create entry with id 1 
            var createForm0 = GenerateCreateForm("Product1", 10);
            var response0 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

            // Create entry with id 2 
            var createForm1 = GenerateCreateForm("Product2", 20);
            var response1 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));

            // Create entry with id 3 
            var createForm2 = GenerateCreateForm("Product3", 30);
            var response2 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm2), Encoding.UTF8, "application/json"));

            // Create entry with id 4 
            var createForm3 = GenerateCreateForm("Product4", 40);
            var response3 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm3), Encoding.UTF8, "application/json"));

            // Create entry with id 5
            var createForm4 = GenerateCreateForm("Product5", 50);
            var response4 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createForm4), Encoding.UTF8, "application/json"));

        }

        private ProductDto GenerateCreateForm(string name, int stock)
        {
            return new ProductDto()
            {
                Name = name,
                Stock = stock
            };
        }
    }
}
