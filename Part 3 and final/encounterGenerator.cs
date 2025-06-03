using System.Collections.Generic;
using KoAText.Monsters;
using static KoAText.Constants;

namespace KoAText
{
    public static class EncounterGenerator
    {
        public static List<Monster> GenerateMonsters(MonsterTypes[] types)
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
