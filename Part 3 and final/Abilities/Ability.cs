using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoAText
{
    public class Ability
    {
        //elements,duration,casting time?? 
        public string name { get; private set; }       
        public string text = "";
        public int reqLevel { get; set; }
        public int manaCost { get; private set; }
        public EffectTypes[] Effects { get; private set; }
        public float scalingMultiplier { get; private set; }
        public int baseDamage { get; private set; }
        
        public Ability(string name, string text,int baseDamage,int reqLevel, float scalingMultiplier, int manaCost =0,params EffectTypes[] extraEffects) { 
            this.name = name;            
            this.text = text;
            this.baseDamage = baseDamage;
            this.reqLevel = reqLevel;
            this.Effects = extraEffects ?? new EffectTypes[0];
            this.scalingMultiplier = scalingMultiplier;
            this.manaCost = manaCost;
        }
        public bool isMagicBased()
        {
            if (this.Effects.Contains(EffectTypes.magic)) return true;
            else return false;
        }

    }
}
