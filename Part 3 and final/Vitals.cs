using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoAText
{
    public class Vitals
    {
        public int BaseHP { get;  set; }
        public int BaseSpeed { get;  set; }
        public int BaseAttack { get;  set; }
        public int BaseDefense { get;  set; }
        public int BaseMagicAttack { get;  set; }
        public int BaseMagicDefense { get;  set; }
        public int BaseMana { get;  set; }
        public bool _isTargetable { get; set; }
        public List<Ability> Effects { get; private set; } = new List<Ability>();


        public Vitals(int baseHP, int baseAttack, int baseDefense, int baseMagicAttack,int baseMagicDefense, int baseSpeed, int baseMana)
        {
            BaseHP = baseHP;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            BaseSpeed = baseSpeed;
            BaseMana = baseMana;
            BaseMagicAttack = baseMagicAttack;
            BaseMagicDefense = baseMagicDefense;
            _isTargetable = true;
            ResetCurrentStats();
        }
        // Backing fields for current stats
        private int _currentHP;
        private int _currentSpeed;
        private int _currentAttack;
        private int _currentDefense;
        private int _currentMagicAttack;
        private int _currentMagicDefense;
        private int _currentMana;
        // Properties with clamped private setters
        public int CurrentHP
        {
            get => _currentHP;
            private set => _currentHP = Math.Clamp(value, 0, BaseHP);
        }
        public int CurrentSpeed
        {
            get => _currentSpeed;
            private set => _currentSpeed = Math.Max(0, value);
        }
        public int CurrentAttack
        {
            get => _currentAttack;
            private set => _currentAttack = Math.Max(0, value);
        }
        public int CurrentDefense
        {
            get => _currentDefense;
            private set => _currentDefense = Math.Max(0, value);
        }
        public int CurrentMagicAttack
        {
            get => _currentMagicAttack;
            private set => _currentMagicAttack = Math.Max(0, value);
        }
        public int CurrentMagicDefense
        {
            get => _currentMagicDefense;
            private set => _currentMagicDefense = Math.Max(0, value);
        }
        public int CurrentMana
        {
            get => _currentMana;
            private set => _currentMana = Math.Clamp(value, 0, BaseMana);
        }

  

        //Modify stats
        public void addEffect(Ability ability)=> Effects.Add(ability);
        public void removeEffect(Ability ability)=> Effects.Remove(ability);
        public void ModifyCurrentHP(int amount) => CurrentHP = CurrentHP + amount;
        public void ModifyCurrentSpeed(int amount) => CurrentSpeed = CurrentSpeed + amount;
        public void ModifyCurrentAttack(int amount) => CurrentAttack = CurrentAttack + amount;
        public void ModifyCurrentDefense(int amount) => CurrentDefense = CurrentDefense + amount;
        public void ModifyCurrentMana(int amount) => CurrentMana = CurrentMana + amount;
        public bool CheckForZeroStats(Vitals vitals)
        {
            if (CurrentAttack <= 0|| CurrentDefense<=0||CurrentSpeed<=0){
                return true;
            }else
                return false;
        }
        // Reset current stats to base values
        public void ResetCurrentStats()
        {
            CurrentHP = BaseHP;
            CurrentSpeed = BaseSpeed;
            CurrentAttack = BaseAttack;
            CurrentDefense = BaseDefense;
            CurrentMagicAttack = BaseMagicAttack;
            CurrentMagicDefense = BaseMagicDefense;
            CurrentMana = BaseMana;
            Effects = new List<Ability>();
            _isTargetable = true;
        }
        public void EndOfCombatCleanUp()
        {
            CurrentSpeed = BaseSpeed;
            CurrentAttack = BaseAttack;
            CurrentDefense = BaseDefense;
            CurrentMagicAttack = BaseMagicAttack;
            CurrentMagicDefense = BaseMagicDefense;
            Effects = new List<Ability>();
            _isTargetable = true;
        }
    }
}
