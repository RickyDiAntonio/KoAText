using System;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using KoAText.Abilities;
using KoAText.Monsters;
using static KoAText.Constants;

namespace KoAText
{

    public class Player : IActor, Actions
    {
        public Vitals Vitals { get; private set; }
        public string PlayerName { get; set; }
        public DestinyBase DestinyType { get; set; }
        public int Level { get; set; }
        public List<Ability> Abilities;
        public int Experience { get; private set; } = 0;
        public int ExperienceToNextLevel = 100;
                public int[] DestinySpread= new int[3];
                public List<DestinyProgress> Destinies { get; private set; } = new();
                public List<Item> Inventory { get; private set; }=new();
        public List<Quest> ActiveQuests { get; private set; } = new List<Quest>();
//------------------------------------------------------------------------------------------------------------------------------------------------------------

public Player(string Name,DestinyBase dest)
        {
            Vitals = new Vitals(dest.startingHP, dest.startingAttack, dest.startingDefense,dest.startingMagicAttack,dest.startingMagicDefense, dest.startingSpeed, dest.startingMana);
            PlayerName = Name;
            DestinyType = dest;
           
            Level = 1;
            Inventory.Add(new Gold(0));
            Ability basicAttack = AbilityFactory.CreateAbility(AbilityNames.basicAttack);
            Abilities = new List<Ability>();
            Abilities.Add(basicAttack);
            AddAbilityList(dest.getAbilitiesByLevel(1));
            Destinies.Add(new DestinyProgress(dest));


        }
        public void GainExperience(int amount)
        {
            Experience += amount;
            Scribe.WriteLineColor($"{PlayerName} gains {amount} XP!", ConsoleColor.Green);
            while (Experience >= ExperienceToNextLevel)
            {
                Experience -= ExperienceToNextLevel;
                LevelUp();
            }
        }
        public void LevelUp()
        {
            Console.WriteLine("Choose a destiny to level up but not finesse yet:");

            var all = DestinyLibrary.All; 
            for (int i = 0; i < all.Count; i++)
            {
                var existing = Destinies.FirstOrDefault(d => d.Destiny.Name == all[i].Name);
                int level = existing?.Level ?? 0;
                Console.WriteLine($"{i + 1}. {all[i].Name} (Level {level})");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > all.Count)
            {
                Console.WriteLine("Invalid choice. Try again.");
            }

            var selected = all[choice - 1];
            GainLevelInDestiny(selected);
            Level++;
            ExperienceToNextLevel = 100 * Level;
           
        }
        public void GainLevelInDestiny(DestinyBase chosen)
        {
            var existing = Destinies.FirstOrDefault(d => d.Destiny.Name == chosen.Name);

            if (existing != null)
            {
                existing.LevelUp(this.Vitals);
                Console.WriteLine($"You gained a level in {chosen.Name}! (Now level {existing.Level} in {chosen.Name})");
                var newAbilities = chosen.getAbilitiesByLevel(existing.Level)
                        .Where(a => !Abilities.Any(existingA => existingA.name == a.name)) // avoid duplicates
                        .ToList();
                AddAbilityList(newAbilities);
                StatDump();
            }
            else
            {
                chosen.ApplyLevelUpGrowth(this.Vitals);
                var newProgress = new DestinyProgress(chosen);
                Destinies.Add(newProgress);

                var newAbilities = chosen.getAbilitiesByLevel(1)
                        .Where(a => !Abilities.Any(existingA => existingA.name == a.name)) // avoid duplicates
                        .ToList();
                AddAbilityList(newAbilities);
                StatDump();


                Console.WriteLine($"You began your journey into {chosen.Name} (Level 1)!");
            }
        }
        private Monster? ChooseMonsterTarget(List<Monster> monsters, Action refreshBattleUI)
        {
            var aliveMonsters = monsters.Where(m => m.isAlive()).ToList();
            if (aliveMonsters.Count == 0) return null;

            void DisplayTargets()
            {
                Scribe.WriteLine("Choose a target or type 'D' for menu:");
                for (int i = 0; i < aliveMonsters.Count; i++)
                {
                    var m = aliveMonsters[i];
                    Scribe.WriteLineColor($"{i + 1}. {m.Name} - {m.Vitals.CurrentHP}/{m.Vitals.BaseHP} HP", ConsoleColor.Yellow);
                    Scribe.WriteLine(m.monsterArt);
                }
            }

            DisplayTargets();

            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "d")
                {
                    GameUI.GlobalPlayerMenu(this);
                    Console.Clear();
                    refreshBattleUI();
                    DisplayTargets();
                    


                    // refresh monster display after returning from menu
                    continue;
                }

                if (int.TryParse(input, out int choice) &&
                    choice >= 1 && choice <= aliveMonsters.Count)
                {
                    return aliveMonsters[choice - 1];
                }

                Scribe.WriteLineColor("Invalid choice. Try again.", ConsoleColor.Red);
            }
        }
        public string GetName()
        {
            return PlayerName;
        }
        public List<Item> GetInventory()
        {
            return this.Inventory;
        }
        public void AddAbility(Ability ability)
        {
            this.Abilities.Add(ability);
        }
        public void AddAbilityList(List<Ability> abilities)
        {
            foreach (Ability a in abilities)
            {
               this.Abilities.Add(a);
            }
        }
        public void TurnStart()
        {
            //dots?
            foreach (Ability ability in this.Vitals.Effects.ToList())
            {
                turnStartDot(ability, ability.baseDamage);

            }
        }
        public void CombatCleanup()
        {
            Vitals.EndOfCombatCleanUp();
            

        }
        public bool isAlive()
        {
            if (this.GetVitals().CheckForZeroStats(this.GetVitals())||Vitals.CurrentHP<=0)
            {
                return false;
            }
            return (Vitals.CurrentHP > 0);
        }        
        public void TakeDamage(int amount)
        {
            Vitals.ModifyCurrentHP(-amount);
        }        
        public void Healing(int amount)
        {
            Vitals.ModifyCurrentHP(amount);
            Scribe.WriteLineColor($"{PlayerName} has been healed for {amount} HP, current HP:{Vitals.CurrentHP}/{Vitals.BaseHP}", ConsoleColor.Green);
        }
        public void StatDump()
        {
            Console.WriteLine($"HP: {Vitals.CurrentHP}/{Vitals.BaseHP}");
            Console.WriteLine($"Speed: {Vitals.CurrentSpeed}/{Vitals.BaseSpeed}");
            Console.WriteLine($"Attack: {Vitals.CurrentAttack}/{Vitals.BaseAttack}");
            Console.WriteLine($"Defense: {Vitals.CurrentDefense}/{Vitals.BaseDefense}");
            Console.WriteLine($"Magic attack: {Vitals.CurrentMagicAttack}/{Vitals.BaseMagicAttack}");
            Console.WriteLine($"Magic defense: {Vitals.CurrentMagicDefense}/{Vitals.BaseMagicDefense}");
            Console.WriteLine($"Mana: {Vitals.CurrentMana}/{Vitals.BaseMana}");

        }
        public void ActionOptions(List<Ability> Abilities)
        {
            Scribe.WriteLineColor("Choose your move",ConsoleColor.Green);
            
            int index = 0;
            foreach (var ability in Abilities)
            {
                Console.WriteLine($"{index+1}.{ability.name}");
                index++;
            }
        }
        public void DisplayHP()
        {
          
            Scribe.WriteColor($"{PlayerName} HP: { Vitals.CurrentHP}/{ Vitals.BaseHP}",ConsoleColor.Red);
            Scribe.WriteLineColor($" MP: {Vitals.CurrentMana}/{Vitals.BaseMana}",ConsoleColor.Blue);
        }
        public void SetHP(int value)
        {
            Vitals.ModifyCurrentHP(value);
        }       
        public void TakeTurn(Player player, List<Monster> monsters, Action refreshBattleUI)
        {
            var  target = ChooseMonsterTarget(monsters,refreshBattleUI);
            if (target == null)
            {
                Scribe.WriteLineColor("There are no valid targets to attack.", ConsoleColor.Yellow);
                return;
            }
                bool isValidAction = false;
            while (!isValidAction)
            {
                ActionOptions(Abilities); // Show available actions
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice)
                    && choice >= 1 && choice <= Abilities.Count)
                {
                    Ability chosenAbility = Abilities[choice - 1];

                    if (chosenAbility.manaCost > Vitals.CurrentMana)
                    {
                        Scribe.WriteLineColor("Not enough mana. Try another move.", ConsoleColor.Red);
                        continue;
                    }

                    Console.Clear();
                    player.targettedAction(target, chosenAbility);
                    isValidAction = true;
                }
                else
                {
                    Scribe.WriteLineColor("Invalid input. Try again.", ConsoleColor.DarkRed);
                }

            }

        }
       

        public void targettedAction(IActor affectedActor,Ability ability)
        {
            

            bool wasTextSent = false;
            foreach (EffectTypes effect in ability.Effects)
            {
                int damageModifier = ability.isMagicBased() ? Vitals.CurrentMagicAttack : Vitals.CurrentAttack;
                damageModifier=(int)Math.Round(damageModifier*ability.scalingMultiplier);

                switch (effect)
                {                                 
                    case EffectTypes.damage:
                        if (!wasTextSent) { Scribe.WriteLine(PlayerName + ability.text); }
                        int damage = ability.baseDamage +damageModifier;
                        affectedActor.TakeEffectType(EffectTypes.damage, damage );//calc defense here
                        this.Vitals.ModifyCurrentMana(-ability.manaCost);
                        break;
                    case EffectTypes.heal:
                        if (!wasTextSent) { Scribe.WriteLine(PlayerName + ability.text); }
                        int healing = ability.baseDamage + damageModifier;
                        this.TakeEffectType(EffectTypes.heal, healing);
                        this.Vitals.ModifyCurrentMana(-ability.manaCost);
                        break;
                    case EffectTypes.DoT:
                        if (!wasTextSent) { Scribe.WriteLine(PlayerName + ability.text); }
                        Ability newAbility = AbilityFactory.CreateAbilityEffect(ability, this);                        
                        affectedActor.GetVitals().addEffect(ability);
                        Console.WriteLine($"Added {ability.name} to {affectedActor.GetName()}'s effects.");
                        this.Vitals.ModifyCurrentMana(-ability.manaCost);
                        break;
                }
                wasTextSent = true;
            }
           
            
        }
        public void TakeEffectType(EffectTypes effectType, int amount)
        {
            switch (effectType)
            {
                case EffectTypes.damage:
                    Vitals.ModifyCurrentHP(-amount);
                    Scribe.WriteRightColor($"{PlayerName} took {amount} damage, current HP: {Vitals.CurrentHP}/{Vitals.BaseHP}", ConsoleColor.DarkRed);
                    break;
                case EffectTypes.heal:
                    Vitals.ModifyCurrentHP(amount);
                    Scribe.WriteLineColor($"{PlayerName} has been healed for {amount} HP, current HP:{Vitals.CurrentHP}/{Vitals.BaseHP}", ConsoleColor.Green);
                    break;
                case EffectTypes.statChangeSpeed:
                    Vitals.ModifyCurrentSpeed(amount);
                    Scribe.WriteLineColor($"{PlayerName} has been slowed! speed reduced to {Vitals.CurrentSpeed}/{Vitals.BaseSpeed}", ConsoleColor.DarkMagenta);
                    break;
            }
        }
        public void turnStartDot(Ability ability, int amount)
        {
            foreach (EffectTypes e in ability.Effects)
            {

                switch (e)
                {
                    case EffectTypes.damage:
                        Scribe.WriteLineColor(($"{PlayerName} took {amount} damage from {ability.name}, current HP: {Vitals.CurrentHP}/{Vitals.BaseHP}"), ConsoleColor.Red);
                                       this.TakeDamage(amount); //calculate defenses here
                        break;

                    case EffectTypes.heal:
                        Scribe.WriteLine(PlayerName + $" healed {amount} from " + ability.name + $" HP: {Vitals.CurrentHP}/{Vitals.BaseHP}");
                        int healing = this.Vitals.CurrentAttack + this.Vitals.CurrentAttack * ability.baseDamage;
                        this.Healing(healing);
                        break;

                }
            }
        }
        public Vitals GetVitals()
        {
            return this.Vitals;
        }
        public void AddItem(Item item)
        {          
            Inventory.Add(item);
            Scribe.WriteLootLine(PlayerName, item);
        }
        public void RemoveItem(Item item)
        {
            if (item == null)
            {
                Scribe.WriteLineColor("Cannot remove a null item.", ConsoleColor.Red);
                return;
            }
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Scribe.WriteLineColor($"{item.Name} has been removed from inventory", ConsoleColor.Gray);
            }
            else
            {
                Scribe.WriteLineColor($"{item.Name} not found in inventory.", ConsoleColor.Red);
            }

        }
        public void UseItem(int index)
        {
            if (index < 0|| index >= Inventory.Count)
            {
                Scribe.WriteLineColor("Invalid item selection.",ConsoleColor.Red);
                return;
            }
            Item item = Inventory[index];
            item.Use(this);
            Inventory.RemoveAt(index);
        }
        public void AddGold(int amount)
        {
            var existingGold = Inventory.OfType<Gold>().FirstOrDefault();
            if (existingGold != null)
            {
                existingGold.GetGold(amount);
                Scribe.WriteLineColor($"{PlayerName} gains {amount} gold (now {existingGold.Amount})!", ConsoleColor.Yellow);
            }
        }
        public int GetGoldAmount()
        {
            var gold = Inventory.OfType<Gold>().FirstOrDefault();
            return gold?.Amount ?? 0;
        }
        public bool SpendGold(int amount)
        {
            var existingGold = Inventory.OfType<Gold>().FirstOrDefault();
            if (existingGold == null || existingGold.Amount < amount)
            {
                Scribe.WriteLineColor($"{PlayerName} doesn't have enough gold!", ConsoleColor.Red);
                return false;
            }

            existingGold.Amount -= amount;
            Scribe.WriteLineColor($"{PlayerName} spends {amount} gold (remaining: {existingGold.Amount})", ConsoleColor.DarkYellow);

            if (existingGold.Amount <= 0)
            {
                Inventory.Remove(existingGold);
            }

            return true;
        }
        public void DisplayInventory()
        {
            Console.WriteLine();
            Scribe.WriteLine("=== Inventory ===");

            //gold
            var gold = Inventory.OfType<Gold>().FirstOrDefault();
            int goldAmount = gold?.Amount ?? 0;
            Scribe.WriteLineColor($"Gold: {goldAmount}", ConsoleColor.DarkYellow);

            var grouped = Inventory
                .Where(i=> i is not Gold)
                .GroupBy(item=>item.Name)
                .ToList();

            if (grouped.Count == 0)
            {
                Console.WriteLine("(No items)");
            }
            else
            {
                int index = 1;
                foreach (var group in grouped)
                {
                    var firstItem = group.First();
                    int quantity = group.Count();
                    Scribe.WriteLineColor($"{index}. {firstItem.Name} - {firstItem.Description} (x{quantity})",ConsoleColor.White);
                    index++;
                }
            }
            Console.WriteLine("=================");

            Scribe.WriteLine("Use an item? Enter the number of the item or press Enter to skip:");
            string? input = Console.ReadLine();
            if (int.TryParse(input,out int itemIndex))
            {
                this.UseItem(itemIndex);
            }
        }

        //look up a better way to do the below to satisfy the interface reqs
        public void TakeTurn(Player player, List<Monster> monsters)
        {
            throw new NotImplementedException();
        }
    }     
}