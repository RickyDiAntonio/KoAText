using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText.Abilities;
using static KoAText.Constants;


namespace KoAText
{
    public static class GameUI
    {
        public static void GlobalPlayerMenu(Player player)
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                Scribe.WriteLine("=== Game Menu ===");
                Scribe.WriteLine("1. Inventory");
                Scribe.WriteLine("2. Quest Log");
                Scribe.WriteLine("3. Player Stats");
                Scribe.WriteLine("4. Back");
                Scribe.WriteLine("=================");

                Console.Write("Choose an option: ");
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        player.DisplayInventory();
                        PauseForPlayer();
                        break;
                    case "2":
                        if (player.ActiveQuests.Count == 0)
                        {
                            Scribe.WriteLineColor("You have no active quests.", ConsoleColor.DarkGray);
                        }
                        else
                        {
                            Scribe.WriteLineColor("=== Quest Log ===", ConsoleColor.Magenta);
                            foreach (var quest in player.ActiveQuests)
                            {
                                quest.CheckProgress(player);
                            }
                        }
                        PauseForPlayer();
                        break;
                    case "3":
                        player.StatDump();
                        PauseForPlayer();
                        break;
                    case "4":
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        PauseForPlayer();
                        break;
                }
            }
        }

        private static void PauseForPlayer()
        {
            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
        }
    }
}

   