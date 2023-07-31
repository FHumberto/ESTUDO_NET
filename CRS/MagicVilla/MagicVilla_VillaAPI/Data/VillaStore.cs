using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDto> VillaList { get; set; } = new()
    {
            new VillaDto{ Id = 1, Name = "Pool View", Sqft=100, Occupancy = 4 },
            new VillaDto{ Id = 2, Name = "Beach View", Sqft=300, Occupancy = 3 }
    };
}
