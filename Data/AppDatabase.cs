using SQLite;
using Pomocnik.Models;

namespace Pomocnik.Data;

public class AppDatabase
{
    private readonly SQLiteAsyncConnection _db;
    private bool _initialized;

    public AppDatabase(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
    }

    private async Task InitAsync()
    {
        if (_initialized) return;
        await _db.CreateTableAsync<Material>();
        _initialized = true;
    }

    // --- CRUD materiałów ---
    public async Task<List<Material>> GetMaterialsAsync()
    {
        await InitAsync();
        return await _db.Table<Material>()
                        .OrderBy(m => m.Name)
                        .ToListAsync();
    }

    public async Task<int> SaveMaterialAsync(Material m)
    {
        await InitAsync();
        return m.Id != 0 ? await _db.UpdateAsync(m) : await _db.InsertAsync(m);
    }

    public async Task<int> DeleteMaterialAsync(Material m)
    {
        await InitAsync();
        return await _db.DeleteAsync(m);
    }

    // Dane startowe (opcjonalnie)
    public async Task SeedAsync()
    {
        await InitAsync();
        var count = await _db.Table<Material>().CountAsync();
        if (count > 0) return;

        var items = new[]
        {
            new Material { Name = "RHS 60x40x2", Category="profile", Unit="mb", StdLengthM=6, PriceNetCents=2200, VatRatePct=23 },
            new Material { Name = "Rura Ø20x2",   Category="profile", Unit="mb", StdLengthM=6, PriceNetCents=1500, VatRatePct=23 },
            new Material { Name = "Płaskownik 25x5", Category="profile", Unit="mb", StdLengthM=6, PriceNetCents=1200, VatRatePct=23 },
            new Material { Name = "Śruba M8x30",  Category="fastener", Unit="szt",               PriceNetCents=35,   VatRatePct=23 },
            new Material { Name = "Nakrętka M8",  Category="fastener", Unit="szt",               PriceNetCents=10,   VatRatePct=23 },
        };
        await _db.InsertAllAsync(items);
    }
}
