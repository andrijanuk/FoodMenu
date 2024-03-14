using Domain.Constants;
using Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Helpers.Utility
{
    public static class MealsHelper
    {
        public static List<string> GetFiveMealsIdByCategory(string values, int digit)
        {
            List<string> result = new List<string>();

            try
            {
                if (JObject.Parse(values)[JsonConstants.meals].Count() >= digit)
                {
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(JObject.Parse(values)[JsonConstants.meals].ToString());

                    foreach (var data in jsonData)
                    {
                        if (result.Count < digit)
                        {
                            result.Add(data[JsonConstants.idMeal].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0}:{1}",ExceptionsConstants.GetFiveMealsIdByCategory, ex.Message));
            }
            return result;
        }
        public static List<string> GetThreeMealsIdByArea(string values, int digit)
        {
            List<string> result = new List<string>();

            try
            {
                if (JObject.Parse(values)[JsonConstants.meals].Count() >= digit)
                {
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(JObject.Parse(values)[JsonConstants.meals].ToString());

                    foreach (var data in jsonData)
                    {
                        if (result.Count < digit)
                        {
                            result.Add(data[JsonConstants.idMeal].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0}:{1}", ExceptionsConstants.GetThreeMealsIdByArea, ex.Message));
            }
            return result;
        }
        public static List<FoodMeal> GetMealsFromJson(List<string> jsonArray)
        {
            List<FoodMeal> result = new List<FoodMeal>();

            try
            {
                foreach (string meal in jsonArray)
                {
                    if (JObject.Parse(meal)[JsonConstants.meals].Count() > 0)
                    {
                        string Name = JObject.Parse(meal)[JsonConstants.meals][0][JsonConstants.strMeal] == null ? string.Empty : JObject.Parse(meal)[JsonConstants.meals][0][JsonConstants.strMeal].ToString();
                        string Category = JObject.Parse(meal)[JsonConstants.meals][0][JsonConstants.strCategory] == null ? string.Empty : JObject.Parse(meal)[JsonConstants.meals][0][JsonConstants.strCategory].ToString();
                        string Area = JObject.Parse(meal)[JsonConstants.meals][0][JsonConstants.strArea] == null ? string.Empty : JObject.Parse(meal)[JsonConstants.meals][0][JsonConstants.strArea].ToString();

                        result.Add(new FoodMeal() { area = Area, category = Category, name = Name });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0}:{1}", ExceptionsConstants.GetMealsFromJson, ex.Message));
            }

            return result;
        }
    }
}
