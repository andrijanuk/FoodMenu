using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Domain.Helpers.Utility;
using System.Text.Json;
using Domain.Constants;

namespace Meal.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v1/meals")]
     public class MealController : Controller
    {
        private readonly IMealService _service;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public MealController(IMealService service, ILogger<MealController> logger, IConfiguration config)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet("{name}")]
        [HttpPost("{name}")]
        public async Task<object> GetMeal(string name)
        {
            var products = await _service.Find(name);
            Meals MealsResult = new Meals();

            try
            {
                if (JObject.Parse(products)[JsonConstants.meals].Count() > 0)
                {
                    string mealId = JObject.Parse(products)[JsonConstants.meals][0][JsonConstants.idMeal].ToString();
                    string category = JObject.Parse(products)[JsonConstants.meals][0][JsonConstants.strCategory].ToString();
                    string area = JObject.Parse(products)[JsonConstants.meals][0][JsonConstants.strArea].ToString();

                    var categoryMeals = await _service.GetMealByCategory(category);
                    var areaMeals = await _service.GetMealByArea(area);

                    string digitByCategory = _config.GetValue<string>(AppSettingsConstants.MealsByDigitByCategory);
                    string digitBeArea = _config.GetValue<string>(AppSettingsConstants.MealsByDigitByArea);

                    List<string> allMeals = new List<string>();
                    allMeals.Add(mealId);

                    if (!string.IsNullOrEmpty(digitByCategory) && !string.IsNullOrEmpty(digitBeArea))
                    {
                        allMeals.AddRange(MealsHelper.GetFiveMealsIdByCategory(categoryMeals, int.Parse(digitByCategory)));
                        allMeals.AddRange(MealsHelper.GetThreeMealsIdByArea(areaMeals, int.Parse(digitBeArea)));
                        allMeals.Distinct();
                    }

                    var Meals = await _service.GetMealsByIds(allMeals);
                    MealsResult.meals = MealsHelper.GetMealsFromJson(Meals);

                    _logger.LogInformation(string.Format(ControllersMessageConstants.mealControllerGetMeal, name, MealsResult.meals.Count));
                }
                else 
                {
                    return new NoMeal() { info = ControllersMessageConstants.notFoundMeal };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ControllersMessageExceptionsConstants.getMeal);
            }
            return MealsResult; 
        }
    }
}
