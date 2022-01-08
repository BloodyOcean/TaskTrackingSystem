using BLL.Models;
using System.Collections.Generic;

namespace BLL.Services
{
    public interface IStatisticService
    {
        IEnumerable<CompletionPercentage> GetCompletionPercentages(int count);

        IEnumerable<CompletionPercentage> GetCompletionPercentagesByManager(int id);
    }
}