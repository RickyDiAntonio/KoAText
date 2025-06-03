using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText.Monsters;
using KoAText.Towns;
using KoAText.Towns.shops;
using static KoAText.Constants;

namespace KoAText
{
    public static class Town
    {
        public static TownInfo Show(Player player, TownInfo currentTown)
        {
            bool stayInTown = true;
            while (stayInTown)
            {
                Console.Clear();
                Console.WriteLine(currentTown.Art); // ASCII town art
                Console.WriteLine($"=== Welcome to {currentTown.Name} ===");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Visit Shop");
                Console.WriteLine("2. Rest at the Inn");
                Console.WriteLine("3. Check the Tavern (Quests)");
                Console.WriteLine("4. Venture into the Wilderness");
                Console.WriteLine("5. View Inventory");
                Console.WriteLine("6. Travel to Another Town");
                Console.WriteLine("7. Leave Town");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        var shop = TownRegistry.GetShopForTown(currentTown);
                        Shop.Show(player, shop);
                        break;
                    case "2":
                        RestAtInn(player);
                        break;
                    case "3":
                        //Tavern.Show(player); // Placeholder for quest logic
                        break;
                    case "4":
                        ExploreWilderness(player, currentTown); // Leads to battle system
                        break;
                    case "5":
                        player.DisplayInventory();
                        Console.WriteLine("Press Enter to return...");
                        Console.ReadLine();
                        break;
                    case "6":
                        return ChooseNewTown(currentTown);                        
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
            return ChooseNewTown(currentTown);
        }

        private static void RestAtInn(Player player)
        {
            player.GetVitals().ResetCurrentStats();
            Console.WriteLine("You rest at the Inn and feel refreshed!");
            Console.WriteLine("All HP and Mana have been restored.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void ExploreWilderness(Player player, TownInfo currentTown)
        {
            Console.Clear();
            Scribe.WriteLine("Choose a wilderness to explore:");

            var all = TownRegistry.GetWildernessForTown(currentTown);
            if (all.Count>0 && all != null)
            {

                for (int i = 0; i < all.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {all[i].Name} - {all[i].Description}");
                }

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > all.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }

                var area = all[choice - 1];
                Console.Clear();
                Scribe.WriteLineColor($"You travel to {area.Name}...", ConsoleColor.Green);

                var monsters = EncounterGenerator.GenerateMonsters(area.GenerateEncounter());
                var battle = new BattleState(player, monsters);
                battle.StartBattle();
            }
            else
            {
                Scribe.WriteLineColor("You can't go adventuring here yet! It's too dangerous", ConsoleColor.Red);
            }
                Scribe.WriteLineColor("Returning to town...", ConsoleColor.DarkCyan);
            Console.ReadLine();
        }
        private static TownInfo ChooseNewTown(TownInfo current)
        {
            Console.Clear();
            Console.WriteLine("Choose a town to travel to:");

            var options = TownLibrary.All
                .Where(t => t != current)
                .ToList();
            for (int i = 0; i < options.Count; i++)
            {
                Scribe.WriteLineColor($"{i + 1}. {options[i].Name}", ConsoleColor.Yellow);
            }
            int choice;
            while ((!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > options.Count))
            {
                Scribe.WriteLineColor("Invalid choice.Try again",ConsoleColor.Red);
            }
            return options[choice - 1];

        }

        private static List<Monster> GenerateMonsters(MonsterTypes[] types)
        {
            var factory = new MonsterFactory();
            var monsters = new List<Monster>();
            foreach (var type in types)
            {
                monsters.Add(factory.CreateMonster(type));
            }
            return monsters;
        }
    }
}