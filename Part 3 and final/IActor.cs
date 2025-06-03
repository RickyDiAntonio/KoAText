using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoAText.Monsters;

namespace KoAText
{
    public interface IActor
    {
        void DisplayHP();
        void SetHP(int value);
        void TakeEffectType(EffectTypes effectType,int amount);
        void TakeDamage(int amount);
        void TurnStart();
        void TakeTurn(Player player,List<Monster> monsters);
        void TakeTurn(Player player, List<Monster> monsters, Action refreshBattleUI);

        Vitals GetVitals();
        bool isAlive();
        string GetName();


    }
}
