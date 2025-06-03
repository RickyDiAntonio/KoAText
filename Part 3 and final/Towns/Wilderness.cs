using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText;
using static KoAText.Constants;

namespace KoAtext.Towns
{
  
    public static class Wilderness 
    {
        public static void Show(Player player)
        {
            Console.Clear();
            Scribe.WriteLine("Choose a wilderness to explore:");

            var all = WildernessLibrary.All;
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

            Scribe.WriteLineColor("Returning to town...", ConsoleColor.DarkCyan);
            Console.ReadLine();

        }
    }
}
