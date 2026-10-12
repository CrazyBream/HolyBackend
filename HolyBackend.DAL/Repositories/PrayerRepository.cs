using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyBackend.DAL.Repositories
{
    public class PrayerRepository : IPrayerRepository
    {
        private readonly HolyDbContext _context;

        public PrayerRepository(HolyDbContext context)
        {
            _context = context;
        }

        public async Task AddPrayerAsync(PrayerRequest prayer)
        {
            await _context.Prayers.AddAsync(prayer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PrayerRequest>> GetAllPrayersAsync()
        {
            return await _context.Prayers.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}