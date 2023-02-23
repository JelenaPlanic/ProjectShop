using ShopService.Models.DTO;

namespace ShopService.Interfaces
{
    public interface IStatisticsRepository
    {
        StatisticsDTO GetStatistics();
    }
}
