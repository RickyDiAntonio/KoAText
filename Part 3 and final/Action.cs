using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoAText
{
    internal interface Actions
    {
        void turnStartDot(Ability ability,int amount);
        
        //who its effecting, effect
        void  targettedAction(IActor affectedActor,Ability ability );
        //affectactor, ability
        
    }
}
