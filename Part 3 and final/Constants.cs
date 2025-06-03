using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoAText
{
    public static class Constants
    {
        public static readonly Random rand = new Random();
        public static class asciiDrawing
        {
            public const string line =
        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            public const string Open =
        @"       " + "\n" +
        @"       " + "\n" +
        @"       " + "\n" +
        @"       ";
            public const string Town =
                @"╔══ ══╗" + "\n" +
                @"║▀▀ ▀▀║" + "\n" +
                @"║▄▄ ▄▄║" + "\n" +
                @"╚══ ══╝";
            public const string Castle =
                @"╔══ ══╗" + "\n" +
                @"║╔═══╗║" + "\n" +
                @"║╚═══╝║" + "\n" +
                @"╚══ ══╝";
            public const string Building =
                @" /---\ " + "\n" +
                @"/-----\" + "\n" +
                @"|     |" + "\n" +
                @"|  █  |";
            public const string Inn =
                @" /---\ " + "\n" +
                @"/-Inn-\" + "\n" +
                @"|     |" + "\n" +
                @"|  █  |";
            public const string Store =
                @" /---\ " + "\n" +
                @"/Store\" + "\n" +
                @"|     |" + "\n" +
                @"|  █  |";
            public const string Chest =
                @"       " + "\n" +
                @"  _._  " + "\n" +
                @" |___| " + "\n" +
                @"       ";
            public const string EmptyChest =
                @"       " + "\n" +
                @"       " + "\n" +
                @" |___| " + "\n" +
                @"       ";
            public const string riverwoodArt =
  @"        /\\        " + "\n" +
  @"       /  \\       " + "\n" +
  @"  / \\\_\_\_/ \\   " + "\n" +
  @" /           \     " + "\n" +
  @"|   Riverwood  |   " + "\n" +
  @" \  /\_\_\_\_/\  / " + "\n" +
  @"  \_\_\_\_\_\_\_/  ";

            public static string StonevaleArt =
        @"   /\   " + "\n" +
        @"  /__\  " + "\n" +
        @" |    | " + "\n" +
        @"_|____|_";

            public static string EmberfallArt =
        @"  ~~~   " + "\n" +
        @" /^^^\  " + "\n" +
        @"< Ember>" + "\n" +
        @"  ~~~   ";
        }
        public static class asciiMonsters
        {
           public const string babyDragon = 
                @"      `oo.'" + "\n" +
                @"  ,.  `-')" + "\n" +
                @" '^\`-' '" + "\n" +
                @"    c-L'-";
            public const string MageArt =
     @"    /^\" + "\n" +
     @"   (o o)" + "\n" +
     @"  /  V  \" + "\n" +
     @" /(  _  )\" + "\n" +
     @"   ^^ ^^";
            public const string goblin =
        @"        //        " + "\n" +
        @"        ││<>      " + "\n" +
        @"/‾‾‾\╭──╯╰──╮     " + "\n" +
        @"\___/│ o  O │/‾‾‾\" + "\n" +
        @"     │╭════╮│\___/" + "\n" +
        @"     ╰╰════╯╯     ";
            public const string electricMonster =
        @"     ₒ   ₒ       " + "\n" +
        @"    __╲_╱__      " + "\n" +
        @"   ╱▬▬ⱺ♦ⱺ▬▬╲     " + "\n" +
        @"╭─wwww/┅\wwww─╮  " + "\n" +
        @"├~~╮  ╷▄╷  ╭~~┤  " + "\n" +
        @"┝▬▬┽ww┤█├ww┾▬▬┥  " + "\n" +
        @"╰╰╰│▀▀ █ ▀▀│╯╯╯ ₒ" + "\n" +
        @"   ╰┬─┬▀┬─┬╯━─━─╯" + "\n" +
        @"    ╰┅╯ ╰┅╯      ";

            public const string wolf =              
    @"                    / V\" + "\n" +
    @"                  / `  /" + "\n" +
    @"                 <<   |" + "\n" +
    @"                 /    |" + "\n" +
    @"               /      |" + "\n" +
    @"             /        |" + "\n" +
    @"           /    \  \ /" + "\n" +
    @"          (      ) | |" + "\n" +
    @"  ________|   _/_  | |" + "\n" +
    @"<__________\______)\__)";
            public const string bat =
    @" /\                 /\" + "\n" +
    @"/ \'._   (\_/)   _.'/ \" + "\n" +
    @"|.''._'--(o.o)--'_.''.|" + "\n" +
    @" \_ / `;=/ "" \=;` \ _/" + "\n" +
    @"   `\__| \___/ |__/`" + "\n" +
    @"        \(_|_)/" + "\n" +
    @"         "" ` """;
        }

       
        public static class Destinies
        {
            public const string Might = "Might";
            public const string Sorcery = "Sorcery";
            public const string Finesse = "Finesse";
            public const string Battlemage = "Battlemage";
            public const string Spellcloak = "Spellcloak";
            public const string Blademaster = "Blademaster";
            public const string Universalist = "Universalist";
        }
        

        public static class Scribe
        {
            public static void WriteLine(string message)
            {
                Console.WriteLine(message);
            }
            public static void WriteLineColor(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public static void WriteColor(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor();
            }
            public static void WriteRight(string message) => Console.WriteLine(message.PadLeft(Console.WindowWidth));
            public static void WriteRightColor(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Scribe.WriteRight(message);
                Console.ResetColor();
            } 
            public static void HighlightItem(string itemName)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(itemName);
                Console.ResetColor();
            }
            public static void WriteLootLine(string playerName,Item item)
            {
                Console.Write($"{playerName} picked up ");
                HighlightItem(item.Name);
                Console.WriteLine("!");
            }



            public static void BasicTownRow(params string[] buildings)
            {
                //split the buildings rows up and put them in array
                var splitBuildings = buildings
                    .Select(b => b.Split('\n'))
                    .ToArray();
                int lineCount = splitBuildings[0].Length;

                for (int i = 0; i < lineCount; i++)
                {
                    foreach (var piece in splitBuildings)
                    {
                        Console.Write(piece[i] + " ");
                    }
                    Console.WriteLine();
                }

            }

        }
        //global-ish enums and console?
        
    }

    public enum WeaponTypes { longsword,greatsword,hammer,daggers,longbow,faeBlades,staff,chakrams,sceptre}
    public enum Stats { hp, att, def,magAtt,magDef, spd, mp }
    public enum MonsterTypes { goblin, wolf,bat,thunderbeast }
    public enum EffectTypes { damage,heal,DoT,shield,onHit,statChangeSpeed,physical,magic,fire,water,earth,air }
    public enum AbilityNames { stormbolt,basicAttack,sphereOfProtection,flameMark, flameMarkDoT,healingSurge,rend,slash,secondWind,shockingStrike }
    public enum TownBuildingTypes { inn, shop,smith,tavern,apothicary }
    public enum QuestType { Kill,Collect}
}
