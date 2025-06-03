using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText.Monsters;
using KoAText.Towns;
using static KoAText.Constants;

namespace KoAText.Towns.shops
{
    public class ShopInventory
    {
        public string ShopName { get; }
        public List<Item> ItemsForSale { get; }
        public ShopInventory(string shopName, List<Item> items)
        {
            ShopName = shopName;
            ItemsForSale = items;
        }
    }

    public static class Shop
    {
        public static void Show(Player player, ShopInventory inventory)
        {
            bool shopping = true;
            while (shopping)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to {inventory.ShopName} how can I help you?");
                Console.WriteLine($"Gold: {player.GetGoldAmount()}");
                Console.WriteLine("1. Buy Items");
                Console.WriteLine("2. Sell Items");
                Console.WriteLine("3. Leave Shop");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        BuyItems(player, inventory.ItemsForSale);
                        break;
                    case "2":
                        SellItems(player);
                        break;
                    case "3":
                        shopping = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again");
                        Console.ReadLine();
                        break;

                }
            }
        }

        private static void BuyItems(Player player, List<Item> stock)
        {
            bool buying = true;
            while (buying)
            {
                Console.Clear();
                Scribe.WriteLine("=== Shop Inventory ===");
                Console.WriteLine($"Your Gold: {player.GetGoldAmount()}");

                var grouped = stock.GroupBy(x => x.Name).ToList();

                if (grouped.Count == 0)
                {
                    Console.WriteLine("No items for sale at the moment.");
                    Console.WriteLine("Press Enter to return...");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < grouped.Count; i++)
                {
                    var item = grouped[i].First();
                    int quantity = grouped[i].Count();
                    Console.WriteLine($"{i + 1}. {item.Name} - {item.Description} | Cost: {item.goldValue} | Stock: {quantity}");
                }

                Console.WriteLine("Enter the item number to purchase or 0 to exit:");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > grouped.Count)
                {
                    Console.WriteLine("Invalid selection. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0)
                {
                    buying = false;
                    break;
                }

                var selectedGroup = grouped[choice - 1];
                var selectedItem = selectedGroup.First();

                if (player.SpendGold(selectedItem.goldValue))
                {
                    player.AddItem(selectedItem);
                    stock.Remove(selectedItem);
                    Console.WriteLine($"You purchased a {selectedItem.Name}!");
                }
                else
                {
                    Console.WriteLine("You don't have enough gold!");
                }

                Console.WriteLine("Press Enter to continue...");
            }
        }

        private static void SellItems(Player player)
        {
            bool selling = true;
            while (selling)
            {
                Console.Clear();
                Scribe.WriteLine("=== Your Inventory ===");

                var sellable = player.GetInventory()
                    .Where(i => i is not Gold)
                    .GroupBy(i => i.Name)
                    .ToList();

                if (sellable.Count == 0)
                {
                    Console.WriteLine("You have nothing to sell.");
                    Console.WriteLine("Press Enter to return...");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < sellable.Count; i++)
                {
                    var item = sellable[i].First();
                    int quantity = sellable[i].Count();
                    Console.WriteLine($"{i + 1}. {item.Name} - Sells for {item.goldValue / 2} | You have: {quantity}");
                }

                Console.WriteLine("Enter the item number to sell or 0 to exit:");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > sellable.Count)
                {
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0)
                {
                    selling = false;
                    break;
                }

                var group = sellable[choice - 1];
                var itemToSell = group.First();

                player.RemoveItem(itemToSell);
                player.AddGold(itemToSell.goldValue / 2);
                Console.WriteLine($"You sold a {itemToSell.Name} for {itemToSell.goldValue / 2} gold.");

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

    }
}
