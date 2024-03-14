using Domain.Interfaces;
using Meal.API.Controllers;
using Meal.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Moq.Protected;
using System;
using System.Net;

namespace Domain.UnitTests
{
    public class MealServiceUnitTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Find_IsNotNull()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiFindHost", "https://www.themealdb.com/api/json/v1/1/search.php?s=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            var result = await controller.Find("pizza");

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task Find_NotExisted()
        { 
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiFindHost", "https://www.themealdb.com/api/json/v1/1/search.php?s=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            var result = await controller.Find("pizzaa");

            //Assert
            Assert.AreEqual(result, "{\"meals\":null}");
        }
        [Test]
        public async Task GetMealByCategory_IsNotNull()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiCategoryHost", "https://www.themealdb.com/api/json/v1/1/filter.php?c=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            var result = await controller.GetMealByCategory("Seafood");

            //Assert
            Assert.IsNotNull(result);
        }        
        [Test]
        public async Task GetMealByCategory_NotExisted()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiCategoryHost", "https://www.themealdb.com/api/json/v1/1/filter.php?c=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            var result = await controller.GetMealByCategory("Seafoodd");

            //Assert
            Assert.AreEqual(result, "{\"meals\":null}");
        }
        [Test]
        public async Task GetMealByArea_IsNotNull()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiAreaHost", "https://www.themealdb.com/api/json/v1/1/filter.php?a=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            var result = await controller.GetMealByArea("Canadian");

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetMealByArea_NotExisted()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiAreaHost", "https://www.themealdb.com/api/json/v1/1/filter.php?a=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            var result = await controller.GetMealByArea("Canadianss");

            //Assert
            Assert.AreEqual(result, "{\"meals\":null}");
        }
        [Test]
        public async Task GetMealsByIds_IsNotNull()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiByIdHost", "https://www.themealdb.com/api/json/v1/1/lookup.php?i=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            List<string> mealsIds = new List<string>();
            mealsIds.Add("52772");

            var result = await controller.GetMealsByIds(mealsIds);

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetMealsByIds_NotExisted()
        {
            //Arrange
            var mockRepoIMealIClient = new HttpClient();
            var mockRepoILogService = new Mock<ILogger<MealService>>();
            var inMemorySettings = new Dictionary<string, string> {
                { "ApiHosts:ApiByIdHost", "https://www.themealdb.com/api/json/v1/1/lookup.php?i=" }
            };

            var mockRepoIConfigurationService = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            MealService controller = new MealService(mockRepoIMealIClient, mockRepoIConfigurationService, mockRepoILogService.Object);

            //Act
            List<string> mealsIds = new List<string>();
            mealsIds.Add("52772999999999");

            var result = await controller.GetMealsByIds(mealsIds);

            List<string> notContains = new List<string>();
            notContains.Add("{\"meals\":null}");

            //Assert
            Assert.AreEqual(result, notContains);
        }
    }
}