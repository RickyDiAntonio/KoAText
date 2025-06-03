using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText.Towns.shops;
using Microsoft.Win32;

namespace KoAText.Towns
{
    public class TownRegistryEntry
    {
        public TownInfo TownInfo { get; }
        public ShopInventory Shop {  get; }
        public List<WildernessArea> WildernessAreas { get; }
        public TownRegistryEntry(TownInfo info, ShopInventory shop, List<WildernessArea> areas)
        {
            TownInfo = info;
            Shop = shop;
            WildernessAreas = areas;
        }
    }
    public static class TownRegistry
    {
        private static readonly List<TownRegistryEntry> townRegistries = new List<TownRegistryEntry>()
        {
             new TownRegistryEntry(
                TownLibrary.Riverwood,
                ShopLibrary.RiverwoodShop,
                new List<WildernessArea> { WildernessLibrary.RiverwoodForest }
            ),
            new TownRegistryEntry(
                TownLibrary.Stonevale,
                ShopLibrary.StonevaleShop,
                new List<WildernessArea> {WildernessLibrary.StoneValeCrags }
            ),
            new TownRegistryEntry(
                TownLibrary.Emberfall,
                ShopLibrary.EmberfallShop,
                new List<WildernessArea> { /* Add Emberfall areas here */ }
            )
        };
        public static ShopInventory GetShopForTown(TownInfo town)
        {
            var entry = townRegistries.FirstOrDefault(r => r.TownInfo == town);
            return entry?.Shop ?? ShopLibrary.RiverwoodShop;
        }
        public static List<WildernessArea> GetWildernessForTown(TownInfo town)
        {
            List<WildernessArea> returnWilds = new List<WildernessArea>();
            var entry = townRegistries.FirstOrDefault(r => r.TownInfo == town);
            returnWilds = entry?.WildernessAreas ?? new List<WildernessArea>();

            return returnWilds;
        }

    }
}
