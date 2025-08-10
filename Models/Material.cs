using SQLite;

namespace Pomocnik.Models;

[Table("Materials")]
public class Material
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Name { get; set; } = "";        // np. "RHS 60x40x2"

    [NotNull]
    public string Category { get; set; } = "profile"; // 'profile','fastener',...

    [NotNull]
    public string Unit { get; set; } = "mb";      // 'mb','szt','m2','kg'

    public double? StdLengthM { get; set; }       // długość handlowa (np. 6.0)

    [NotNull]
    public int PriceNetCents { get; set; }        // cena netto w groszach (np. 2200 = 22,00)

    [NotNull]
    public int VatRatePct { get; set; } = 23;     // VAT w %

    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
