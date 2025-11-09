namespace JBC.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<object> GetStatsAsync(DateOnly? start, DateOnly? end);
        Task<IEnumerable<object>> GetVansStatsAsync(DateOnly? start, DateOnly? end);
        Task<IEnumerable<object>> GetContractorsStatsAsync(DateOnly? start, DateOnly? end);
    }
}
