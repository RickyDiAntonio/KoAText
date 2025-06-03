using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAtext;
using KoAText.events;
using static KoAText.Constants;


namespace KoAText
{
    
    public abstract class Quest:IEventListener
    {
        public string Title { get; }
        public string Description { get; }
        public bool IsCompleted { get; protected set; }

        public Quest(string title, string description)
        {
            Title = title; Description = description;IsCompleted = false;
        }
        public abstract void CheckProgress(Player player);

        public abstract void OnEvent(GameEvent gameEvent);

    }
    //kill quest

    public class KillQuest : Quest,IEventListener
    {
        private MonsterTypes targetMonsterType;
        private int requiredKills;
        private int currentKills;

        public KillQuest(string title, string description,MonsterTypes monsterType,int killsRequired)
            : base(title, description)
        {
            targetMonsterType = monsterType;
            requiredKills=killsRequired;
            currentKills = 0;
            EventHub.Subscribe(this);
        }
        public override void OnEvent(GameEvent gameEvent)
        {
            if (gameEvent.QuestType == QuestType.Kill&& gameEvent is MonsterKillEvent evt) {
                if (evt.MonsterType == targetMonsterType)
                {
                    currentKills++;
                    if (currentKills == requiredKills) 
                    { 
                        IsCompleted = true;
                        Scribe.WriteLineColor($"Quest Completed: {Title}", ConsoleColor.Green);

                    }
                }
            }
        }

        public override void CheckProgress(Player player)
        {
            Scribe.WriteLineColor($"{Title}: {currentKills}/{requiredKills} {targetMonsterType}s defeated.",ConsoleColor.Magenta);
        }
    }
    //fetch quest
    public class CollectQuest : Quest
    {
        private string itemName;
        private int requiredAmount;
        private int currentAmount;

        public CollectQuest(string title, string description, string itemName, int amountRequired)
            : base(title, description)
        {
            this.itemName = itemName;
            this.requiredAmount = amountRequired;
            currentAmount = 0;
            EventHub.Subscribe(this);

        }
        public override void OnEvent(GameEvent gameEvent)
        {
            if (gameEvent.QuestType == QuestType.Collect && gameEvent is ItemCollectedEvent evt)
            {
                if (evt.ItemName == itemName)
                {
                    currentAmount++;
                    if (currentAmount == requiredAmount)
                    {
                        IsCompleted = true;
                        Scribe.WriteLineColor($"Quest Completed: {Title}", ConsoleColor.Green);

                    }
                }
            }
        }

        public override void CheckProgress(Player player)
        {
            int count = player.GetInventory().Count(i => i.Name == itemName);
            Scribe.WriteLineColor($"{Title} - {count}/{requiredAmount} collected", ConsoleColor.Green);
            if (count >= requiredAmount)
            {
                IsCompleted = true;
                Scribe.WriteLineColor($"Quest Complete: {Title}", ConsoleColor.Cyan);
            }
        }
    }
}
