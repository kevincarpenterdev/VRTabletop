using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRTabletop.Pawns;
using VRTabletop.Communications;
using UnityEngine;
using VRTabletop.Rules;

namespace VRTabletop.Rules
{
    public class Roll
    {

        public Response Rolls(AttackOrder AO)
        {
            int roll;
            System.Random Dice = new System.Random();
            int[,] attackRolls = new int[AO.attackNum, 3];
            int[] defenseRolls = new int[3];
            int attackDamage = 0;
            int defenseDamage = 0;

            //Roll for Attacker
            for (int x = 0; x < attackRolls.GetLength(0); x++)
            {
                roll = Dice.Next(1, 21);
                attackRolls[x, 0] = roll;
                if (roll <= AO.attackSkill)
                {
                    attackRolls[x, 1] = 1;
                    attackRolls[x, 2] = 1;

                }
                else
                {
                    attackRolls[x, 1] = 0;
                    attackRolls[x, 2] = 0;
                }
            }

            //Roll for Defender
            roll = Dice.Next(1, 21);
            defenseRolls[0] = roll;
            if (defenseRolls[0] <= AO.defendSkill)
            {
                defenseRolls[1] = 1;
                defenseRolls[2] = 1;

            }
            else
            {
                defenseRolls[1] = 0;
                defenseRolls[2] = 0;
            }

            //Negate Attack Rolls
            for (int x = 0; x < attackRolls.GetLength(0); x++)
            {
                if (attackRolls[x, 1] != 0)
                {
                    if (attackRolls[x, 0] < defenseRolls[0] && defenseRolls[1] != 0)
                    {
                        attackRolls[x, 2] = 0;

                    }
                    else if (attackRolls[x, 0] == defenseRolls[0] && defenseRolls[1] != 0)
                    {
                        attackRolls[x, 2] = 0;
                        defenseRolls[2] = 0;
                    }
                    else if (attackRolls[x, 0] > defenseRolls[0] && defenseRolls[1] != 0)
                    {
                        defenseRolls[2] = 0;
                    }
                }
            }

            //Calculate Damage
            for (int x = 0; x < attackRolls.GetLength(0); x++)
            {
                attackDamage += attackRolls[x, 2];
            }
            defenseDamage = defenseRolls[2];

            //Populate Result
            DamageResponse damage = new DamageResponse(AO.TargetID, 0 - attackDamage, 0 - defenseDamage, CommandType.TargetAbility);
            damage.Result += "Attacker Shots:\n";
            for (int x = 0; x < AO.attackNum; x++)
            {
                damage.Result += (attackRolls[x, 0] + ":  ");
                if(attackRolls[x, 1] == 1 && attackRolls[x, 2] != 0)
                {
                    damage.Result += "Hit!";
                }
                else if( attackRolls[x, 1] == 0)
                {
                    damage.Result += "Miss";
                }
                else
                {
                    damage.Result += "Negated";
                }
            }
            damage.Result += "\nDefender Shot:\n" + defenseRolls[0] + ": ";
            if(defenseRolls[1] == 1 && defenseRolls[2] != 0)
            {
                damage.Result += "Hit!";
            }
            else if (defenseRolls[1] == 0)
            {
                damage.Result += "Miss";

            } else
            {
                damage.Result += "Negated";
            }
            //Report Response         
            return damage;
        }
    }
}
