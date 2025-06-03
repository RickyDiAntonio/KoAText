using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using KoAText.Abilities;
using static System.Net.Mime.MediaTypeNames;

namespace KoAText
{
    public class DestinyBase
    {

        public string Name { get; }
        public int startingHP { get; }       
        public  int startingAttack { get; }
        public  int startingDefense { get; }
        public int startingMagicAttack { get; }
        public int startingMagicDefense {  get; }
        public int startingSpeed { get; }
        public int startingMana { get; }

        public List<Ability> AbilitiesByLevel = new List<Ability>();
       
        public DestinyBase(string Name,int HP,int Att, int Def,int magicatt,int magicDef, int sp, int mana)
        {
            this.Name = Name;
            this.startingHP = HP;
            this.startingSpeed = sp;
            this.startingAttack = Att;
            this.startingDefense = Def;
            this.startingMagicAttack = magicatt;
            this.startingMagicDefense = magicDef;
            this.startingMana = mana;
        }
        //AbilitiesByLevel.Where(ability => ability.level =< characterLevel)
        public List<Ability> getAbilitiesByLevel(int level)
        {
          return  AbilitiesByLevel
                .Where(ability => ability.reqLevel <= level)
                .ToList();
            
        }
        public virtual void ApplyLevelUpGrowth(Vitals vitals)
        {
            vitals.BaseHP += 15;
            vitals.BaseAttack += 2;
            vitals.BaseDefense += 2;
            vitals.BaseMagicAttack += 2;
            vitals.BaseMagicDefense += 2;
            vitals.BaseMana += 10;
        }
    }
   
    public class Sorcery:DestinyBase {

        // public DestinyBase(string Name,int HP,int Att, int Def,int magicatt,int magicDef, int sp, int mana)
        public Sorcery() : base("Sorcery", 80,5,5,15,15,15,200) {
            AbilitiesByLevel.Add(AbilityFactory.CreateAbility(AbilityNames.stormbolt));
            AbilitiesByLevel.Add(AbilityFactory.CreateAbility(AbilityNames.healingSurge));
            AbilitiesByLevel.Add(AbilityFactory.CreateAbility(AbilityNames.flameMark));            
        }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseMagicAttack += 10;
            vitals.BaseMagicDefense += 10;
            vitals.BaseMana += 100;
        }
    }

    public class Might : DestinyBase
    {
        public Might() : base("Might", 150, 15, 20,1,8, 12, 100) {
            AbilitiesByLevel.Add(AbilityFactory.CreateAbility(AbilityNames.slash));
            AbilitiesByLevel.Add(AbilityFactory.CreateAbility(AbilityNames.shockingStrike));
            AbilitiesByLevel.Add(AbilityFactory.CreateAbility(AbilityNames.secondWind));
        }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseHP += 50;
            vitals.BaseAttack += 10;
            vitals.BaseDefense += 10;
            vitals.BaseMana += 50;
        }
    }
    public class Finesse : DestinyBase
    {
        public Finesse() : base("Finesse", 100, 10, 10,3,10, 20, 100) { }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseSpeed += 10;
            vitals.BaseAttack += 15;
            vitals.BaseMagicAttack += 5;
            vitals.BaseMana += 70;
        }
    }
    public class BattleMage : DestinyBase
    {
        public BattleMage() : base("Battlemage", 0, 0, 0, 0, 0, 0, 0) { }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseAttack += 8;
            vitals.BaseMagicAttack += 8;
            vitals.BaseDefense += 8;
            vitals.BaseMagicDefense += 8;
            vitals.BaseMana += 75;
        }
        
    }
    public class Spellcloak : DestinyBase
    {
        public Spellcloak() : base("Spellcloak", 0, 0, 0, 0, 0, 0, 0) { }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseSpeed += 10;
            vitals.BaseAttack += 10;
            vitals.BaseMagicAttack += 8;
            vitals.BaseMagicDefense += 6;
            vitals.BaseMana += 75;
        }
    }
    public class Blademaster : DestinyBase
    {
        public Blademaster() : base("Blademaster", 0, 0, 0, 0, 0, 0, 0) { }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseSpeed += 10;
            vitals.BaseAttack += 10;
            vitals.BaseDefense += 8;
            vitals.BaseMana += 50;
        }
    }
    public class Universalist : DestinyBase
    {
        public Universalist() : base("Universalist", 0, 0, 0, 0, 0, 0, 0) { }
        public override void ApplyLevelUpGrowth(Vitals vitals)
        {
            base.ApplyLevelUpGrowth(vitals);
            vitals.BaseHP += 15;
            vitals.BaseAttack += 8;
            vitals.BaseDefense += 8;
            vitals.BaseMagicAttack += 8;
            vitals.BaseMagicDefense += 8;
            vitals.BaseMana += 50;
        }
    }

    public class DestinyProgress
    {
        public DestinyBase Destiny { get; }
        public int Level { get; private set; }

        public DestinyProgress(DestinyBase destiny)
        {
            Destiny = destiny;
            Level = 1;
        }


        public void LevelUp(Vitals vitals)
        {
            Level++;
            Destiny.ApplyLevelUpGrowth(vitals);
        }
    }
    public static class DestinyLibrary
    {
        public static readonly DestinyBase Sorcery = new Sorcery();
        public static readonly DestinyBase Might = new Might();
        public static readonly DestinyBase Finesse = new Finesse();
        public static readonly DestinyBase BattleMage = new BattleMage();
        public static readonly DestinyBase Spellcloak = new Spellcloak();
        public static readonly DestinyBase Blademaster = new Blademaster();
        public static readonly DestinyBase Universalist = new Universalist();


        public static readonly List<DestinyBase> All = new()
    {
        Sorcery,
        Might,
        Finesse
    };
    }
}
