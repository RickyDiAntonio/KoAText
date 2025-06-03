using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KoAText.Abilities;
using KoAText.Monsters;
using KoAText.Towns;
using static KoAText.Constants;

namespace KoAText
{
    public class Game
    {
        public void Start()
        {
            Player mainCharacter = createCharacter();
            bool gameIsRunning = true;
            TownInfo currentTown = TownLibrary.Riverwood;//starter town

            var goblinQuest = new KillQuest(
    "Goblin Menace",
    "Slay 1 Goblin to help the town.",
    MonsterTypes.goblin,
    1
);

            var wolfQuest = new KillQuest(
                "Wolf Trouble",
                "Eliminate 1 Wolf threatening the roads.",
                MonsterTypes.wolf,
                1
            );

            mainCharacter.ActiveQuests.Add(goblinQuest);
            mainCharacter.ActiveQuests.Add(wolfQuest);

            while (gameIsRunning)
            {
                currentTown =Town.Show(mainCharacter,currentTown);
            }
            
        }
        public Player createCharacter()
        {
            DestinyBase destinyChoice = new DestinyBase("oopsis", 1, 1, 1, 1, 1,1,1);
            Scribe.WriteLineColor("Welcome Hero! What is your name?",ConsoleColor.Cyan);
            string? name = "Synowka";
            //string? name = Console.ReadLine();

            bool validDestinyChoice = false;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));


            while (name != null && !validDestinyChoice)
            {

                Console.Write("Choose your destiny tree but not finesse yet");
                Scribe.WriteColor(" [1] Might ", ConsoleColor.Red);
                Scribe.WriteColor(" [2] Sorcery ", ConsoleColor.Blue);
                Scribe.WriteLineColor(" [3] Finesse ", ConsoleColor.DarkYellow);
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));
                switch ("2")
                {
                    case "2":
                        destinyChoice = new Sorcery();
                        validDestinyChoice = true;
                        break;
                }
                //switch (Console.ReadLine())
                //{
                //    case "1":
                //        destinyChoice = new Might();
                //        validDestinyChoice = true;
                //        break;
                //    case "2":
                //        destinyChoice = new Sorcery();
                //        validDestinyChoice = true;
                //        break;
                //    case "3":
                //        destinyChoice = new Finesse();
                //        validDestinyChoice = true;
                //        break;
                //    default:
                //        Console.WriteLine("Invalid choice.Try again");
                //        continue;
                //}
            }

            Player createdChar = new Player(name ?? "Doesn't know name", destinyChoice);
            Console.Clear();
            return createdChar;
        }
        

    }
}
