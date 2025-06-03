using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using KoAText.Abilities;
using static KoAText.Constants;

namespace KoAText
{
    public abstract class Item
    {
        public string Name { get; }
        public string Description { get; }
        public bool isSellable { get; set; }=true;
        public int goldValue {  get; set; }
        public Item(string name, string description)
        {
            Name = name; 
            Description = description;
        }
        public abstract void Use(Player player);
    }
    public class Gold:Item
    {
        public int Amount { get; set; }

        public Gold(int amount) : base("Gold", $"A stack of {amount} gold coins")
        {
            Amount = amount;
            isSellable=false;
        }
        public void SpendGold(int cost)
        {
            if (Amount >= cost)
            {
                Amount -= cost;
            }
            else
            {
                Scribe.WriteLineColor("Insufficient funds, go get more gold for this", ConsoleColor.Red);
            }
        }
        public override void Use(Player player)
        {
                
        }
        public void GetGold(int amount) => Amount += amount;
        public void GetGoldWithText(int amount, Player player)
        {
            Amount += amount;
            Scribe.WriteColor($"{player.PlayerName} adds ", ConsoleColor.DarkGreen);
            Scribe.WriteColor(amount.ToString(), ConsoleColor.DarkYellow);
            Scribe.WriteLineColor($" gold to their pouch.", ConsoleColor.DarkGreen);

        }
        public override string ToString()
        {
            return $"Gold: {Amount}";
        }
    }
    public class HealthPotion : Item
    {
        public int HealAmount { get; }
        public HealthPotion():base("Health Potion", "Restores 50 HP")
        {
            HealAmount = 50;
            goldValue = 15;
        }
        public override void Use(Player player)
        {
            player.Healing(HealAmount);
        }
    }
    public class WolfFang : Item
    {
        public WolfFang() : base("Wolf Fang", "Can be used in pairs to look like dracula, but that is also gross") {
            goldValue = 10;
        }
        public override void Use(Player player) { }
    }
}
