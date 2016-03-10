using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class BattleMethods
    {
        public BattleMethods()
        {

        }

        public void ShootAndShoot(BattleObject attacker, BattleObject defender)
        {
            Random roll = new Random();
                        
            int[] attackRolls = new int[attacker.burst];
            int defenseRolls;
            int[] attackHits = new int[attacker.burst];
            int defenseHits = 0;

            // Roll for Attacker
            for (int x = 0; x < attackRolls.Length; x++)
            {
                int y = roll.Next(1, 21);
                attackRolls[x] = y;
                //Console.WriteLine(y);
                if (y <= attacker.BS)
                {
                    attackHits[x] = y;
                }
                else
                {
                    attackHits[x] = 0;
                }
            }

            // Roll for Defender
            defenseRolls = roll.Next(1, 21);
            //Console.WriteLine(defenseRolls);
            if( defenseRolls <= defender.BS)
            {
                defenseHits = defenseRolls;
            }

            else
            {
                defenseHits = 0;
            }

            // Negate Attack rolls
            for (int x = 0; x < attackRolls.Length; x++)
            {
                if (attackHits[x] != 0)
                {
                    if (attackRolls[x] < defenseRolls && defenseRolls <= defender.BS)
                    {
                        attackHits[x] = 0;
                    }
                    else if (attackRolls[x] == defenseRolls && defenseRolls <= defender.BS)
                    {
                        attackHits[x] = 0;
                    }
                    else
                    {
                        defenseHits = 0;
                    }
                }
            }

            // Print Results
            Console.WriteLine("Attacker Skill: {0}     Defender Skill: {1}", attacker.BS, defender.BS);
            Console.WriteLine("\nAttacker Rolls:");
            for( int x = 0; x < attackRolls.Length; x++)
            {
                Console.Write("{0}: ", attackRolls[x]);
                if(attackRolls[x] > attacker.BS)
                {
                    Console.Write("Miss\n");
                }
                else if(attackHits[x] != 0)
                {
                    Console.Write("Hit\n");
                }
                else
                {
                    Console.Write("Negated\n");
                }
            }

            Console.WriteLine("\nDefender Roll:");
            Console.Write("{0}: ", defenseRolls);
            if(defenseRolls > defender.BS)
            {
                Console.Write("Miss\n");
            }
            else if(defenseHits != 0)
            {
                Console.Write("Hit\n");
            }
            else
            {
                Console.Write("Negated\n");
            }

        }
    }
}
