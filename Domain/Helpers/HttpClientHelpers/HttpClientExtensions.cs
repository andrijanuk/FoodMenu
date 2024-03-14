using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Helpers.HttpClientHelpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
                throw new ApplicationException($"{CommonConstants.smthWrongAPI}: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<T>(
                dataAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }
    }
}
