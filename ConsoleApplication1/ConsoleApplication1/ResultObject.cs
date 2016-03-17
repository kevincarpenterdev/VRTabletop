using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class ResultObject
    {
        public int[,] attackRolls { get; set; }
        public int[] defenseRolls { get; set; }
        public int attackerDamage { get; set; }
        public int defenderDamage { get; set; }
        public BattleObject attacker { get; set; }
        public BattleObject defender { get; set; }

        /*
        public ResultObject(int[,] attackRolls, int[] defenseRolls, int attackerDamage, int defenderDamage)
        {
            this.attackRolls = attackRolls;
            this.defenseRolls = defenseRolls;
            this.attackerDamage = attackerDamage;
            this.defenderDamage = defenderDamage;
        }
        */

        public ResultObject()
        {

        }

        public void Set(BattleObject attacker, BattleObject defender, int[,] attackRolls, int[] defenseRolls, int attackerDamage, int defenderDamage)
        {
            this.attacker = attacker;
            this.defender = defender;
            this.attackRolls = attackRolls;
            this.defenseRolls = defenseRolls;
            this.attackerDamage = this.attackerDamage;
            this.defenderDamage = defenderDamage;
        }
    }
}
