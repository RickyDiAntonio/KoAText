using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoAText.Towns.shops
{
    public static class ShopLibrary
    {
        public static readonly ShopInventory RiverwoodShop = new ShopInventory(
            "Riverwood Supply Co.",
            new List<Item>
            {
                new HealthPotion(),
                new HealthPotion(),
                //new ManaPotion(),
                //new BasicSword()
            });

        public static readonly ShopInventory StonevaleShop = new ShopInventory(
            "Stonevale Armory",
            new List<Item>
            {
                //new BasicSword(),
                //new BasicShield(),
                new HealthPotion(),
                new HealthPotion()
            });

        public static readonly ShopInventory EmberfallShop = new ShopInventory(
            "Emberfall Enchantments",
            new List<Item>
            {
                //new ManaPotion(),
                //new ManaPotion(),
                //new FireScroll()
            });

        public static List<ShopInventory> AllShops = new()
        {
            RiverwoodShop,
            StonevaleShop,
            EmberfallShop
        };

        public static ShopInventory GetShopByTownName(string townName)
        {
            return townName switch
            {
                "Riverwood" => RiverwoodShop,
                "Stonevale" => StonevaleShop,
                "Emberfall" => EmberfallShop,
                _ => RiverwoodShop // Default fallback
            };
        }
        public static ShopInventory GetShopByTown(TownInfo town)
        {
            return GetShopByTownName(town.Name);
        }
    }
}
