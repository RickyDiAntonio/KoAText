using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText;

namespace KoAText
{
    public class WildernessArea
    {
        public string Name { get; }
        public string Description { get; }
        public MonsterTypes[] possibleEncounters { get; } = new MonsterTypes[0];

        public WildernessArea(string name, string description, MonsterTypes[] possibleEncounters)
        {
            Name = name;
            Description = description;
            this.possibleEncounters = possibleEncounters;
        }
        public MonsterTypes[] GenerateEncounter()
        {
            int count = Constants.rand.Next(1, 3);
            MonsterTypes[] encounter = new MonsterTypes[count];
            for (int i = 0; i < count; i++)
            {
                int index = Constants.rand.Next(possibleEncounters.Length);
                encounter[i] = possibleEncounters[index];
            }
            return encounter;
        }

    }
    public static class WildernessLibrary
    {
        public static int GetRandomCreatureCount()
        {
            return Constants.rand.Next(1, 4);
        }

        public static WildernessArea RiverwoodForest = new WildernessArea(
            "Riverwood Forest",
            "A quiet forest known for wolves and goblins.",
            new[] { MonsterTypes.goblin, MonsterTypes.wolf }
        );
        public static WildernessArea StoneValeCrags = new WildernessArea(
            "StoneValeCrags", "What lurks in the deep? its bats and thunderbeasts....",
            new[] { MonsterTypes.bat, MonsterTypes.thunderbeast }
            );

        public static List<WildernessArea> All = new() { RiverwoodForest };
    }
}
