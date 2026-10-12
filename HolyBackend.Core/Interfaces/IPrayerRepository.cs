using HolyBackend.Core.Models;

namespace HolyBackend.Core.Interfaces
{
    public interface IPrayerRepository
    {
        Task AddPrayerAsync(PrayerRequest prayer);
        Task<IEnumerable<PrayerRequest>> GetAllPrayersAsync();
    }
}