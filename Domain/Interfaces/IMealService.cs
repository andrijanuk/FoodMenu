using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMealService
    {
        Task<string> Find(string name);
        Task<string> GetMealByCategory(string category);
        Task<string> GetMealByArea(string area);
        Task<List<string>> GetMealsByIds(List<string> ids);
    }
}
