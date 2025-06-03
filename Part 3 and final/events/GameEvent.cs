using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoAText.events
{
    public abstract class GameEvent
    {
        public QuestType QuestType { get; }
        protected GameEvent(QuestType questType)
        {
            QuestType = questType;
        }
    }
    public interface IEventListener
    {
        void OnEvent(GameEvent gameEvent);
    }
    public static class EventHub
    {
        private static readonly List<IEventListener> listeners = new List<IEventListener>();
        public static void Subscribe(IEventListener subscriber)
        {
            listeners.Add(subscriber);
        }
        public static void UnSubscribe(IEventListener listener)
        {
            listeners.Remove(listener);
        }
        public static void Publish(GameEvent gameEvent)
        {
            foreach (var listener in listeners)
            {
                listener.OnEvent(gameEvent);
            }
        }
    }
    public class MonsterKillEvent : GameEvent
    {
        public MonsterTypes MonsterType { get; }

        public MonsterKillEvent(MonsterTypes monsterType) : base(QuestType.Kill)
        {
            MonsterType = monsterType;
        }

    }
    public class ItemCollectedEvent : GameEvent
    {
        public string ItemName { get; }
        public int Quantity { get; }
        public ItemCollectedEvent(string name, int quantity):base(QuestType.Collect)
        {
            ItemName = name;
            Quantity = quantity;
        }
    }
}
