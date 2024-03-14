using Domain.Constants;
using Domain.Helpers.HttpClientHelpers;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System.Text.Json;


namespace Meal.API.Services
{
    public class MealService: IMealService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public MealService(HttpClient client, IConfiguration config, ILogger<MealService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> Find(string name)
        {
            string result = "";

            try
            {
                string apiUrl = _config.GetValue<string>(AppSettingsConstants.ApiFindHost);
                var response = await _client.GetAsync(string.Format("{0}{1}", apiUrl, name));
                result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, MealServiceConstants.Find);
            }
            return result;
        }

        public async Task<string> GetMealByCategory(string category)
        {
            string result = "";

            try
            {
                string apiUrl = _config.GetValue<string>(AppSettingsConstants.ApiCategoryHost);
                var response = await _client.GetAsync(string.Format("{0}{1}", apiUrl, category));
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MealServiceConstants.GetMealByCategory);
            }
            return result;
        }

        public async Task<string> GetMealByArea(string area)
        { 
            string result = "";

            try
            {
                string apiUrl = _config.GetValue<string>(AppSettingsConstants.ApiAreaHost);
                var response = await _client.GetAsync(string.Format("{0}{1}", apiUrl, area));
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MealServiceConstants.GetMealByArea);
            }
            return result;
        }

        public async Task<List<string>> GetMealsByIds(List<string> ids)
        {
            List<string> result = new List<string>();

            try
            {
                string apiUrl = _config.GetValue<string>(AppSettingsConstants.ApiByIdHost);
                foreach (string id in ids)
                {
                    var response = await _client.GetAsync(string.Format("{0}{1}", apiUrl, id));
                    var data = await response.Content.ReadAsStringAsync();
                    result.Add(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MealServiceConstants.GetMealsByIds);
            }
        
            return result;
        }
    }
}
