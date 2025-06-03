using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText.events;
using KoAText.Monsters;
using static KoAText.Constants;

namespace KoAText
{
    public class BattleState
    {
        //battlestate with player and list of monsters
        private Player _player;
        private List<Monster>? _monsters;
        private Queue<IActor> _turnQueue;
        private bool _battleOver = false;

        public BattleState(Player player, List<Monster> monsters)
        {
            _player = player;
            _monsters = monsters;
            _turnQueue = new Queue<IActor>();
            InitializeTurnOrder();
        }

        //turn order
        private List<IActor> InitializeTurnOrder()
        {
            var actors= new List<IActor>() { _player };
            if(_monsters ==null) throw new ArgumentNullException(nameof(_monsters));           
            actors.AddRange(_monsters.Where(m => m.Vitals.CurrentHP > 0));

            var sorted = actors.OrderByDescending(a => a.GetVitals().CurrentSpeed).ToList();
            _turnQueue.Clear();
            foreach (var actor in sorted)
            {
                _turnQueue.Enqueue(actor);
            }
            return sorted;

        }
        //startbattle
        public void StartBattle()
        {
            DisplayStatus();
            if (_monsters == null) throw new ArgumentNullException(nameof(_monsters));
            while (!_battleOver)
            {
                if (_turnQueue.Count == 0)
                {
                    InitializeTurnOrder(); // Only refill the queue when it's empty
                    DisplayQuickStatus();

                }

                var currentActor = _turnQueue.Dequeue();

                if (!currentActor.isAlive()) continue;

                currentActor.TurnStart();

                if (!currentActor.isAlive()) continue;

                currentActor.TakeTurn(_player, _monsters, DisplayStatus);
                
                CheckBattleEnd();
            }
        }
        private void CheckBattleEnd()
        {
            if (!_player.isAlive())
            {
                _battleOver = true;
                Scribe.WriteLineColor("You have been defeated....", ConsoleColor.Red);
                _player.CombatCleanup();
            }
            if (_monsters != null && !_monsters.Any(m => m.isAlive()))
            {
                _battleOver = true;
                Scribe.WriteLineColor("You defeated all foes!", ConsoleColor.Cyan);

                int totalXP = _monsters.Sum(m => m.XPreward);
                
                _player.GainExperience(totalXP);
                int totalGold = 0;
                foreach(var monster in _monsters) 
                    {
                    //player broadcast monster defeated event here
                    EventHub.Publish(new MonsterKillEvent(monster.Type));
                        foreach(var loot in monster.LootTable)
                            {
                        if(loot is Gold g)
                        {
                            totalGold += g.Amount;
                        }
                        //player broadcast quest item collection event here
                        EventHub.Publish(new ItemCollectedEvent(loot.Name, 1));
                            _player.AddItem(loot);
                            } 
                    }
                if (totalGold > 0)
                {
                    _player.AddGold(totalGold);
                }
                _player.Vitals.EndOfCombatCleanUp();
            }
        }
        private void DisplayStatus()
        {
            string turnText = $"----BattleStats----";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (turnText.Length / 2)) + "}", turnText));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));
            _player.DisplayHP();
            
            if(_monsters != null)
            foreach (var m in _monsters)
            {
                    m.DisplayHP();
            }
            Console.WriteLine();
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));

        }
        private void DisplayQuickStatus()
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));

            _player.DisplayHP();
            if (_monsters != null)
                foreach (var m in _monsters)
                {
                    m.DisplayHP();
                }
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (asciiDrawing.line.Length / 2)) + "}", asciiDrawing.line));

        }



        //choosing targets
        //ending battle
    }
}
