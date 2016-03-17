using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Text;

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
                        
            int[,] attackRolls = new int[attacker.burst, 2];
            int[] defenseRolls = new int[2];
            int attackerDamage = 0;
            int defenderDamage = 0;

            // Roll for Attacker
            for (int x = 0; x < attackRolls.GetLength(0); x++)
            {
                int y = roll.Next(1, 21);
                attackRolls[x,0] = y;
                //Console.WriteLine(y);
                if (y <= attacker.BS)
                {
                    attackRolls[x,1] = 1;
                    defenderDamage++;
                }
                else
                {
                    attackRolls[x,1] = 0;
                }
            }

            // Roll for Defender
            defenseRolls[0] = roll.Next(1, 21);
            //Console.WriteLine(defenseRolls);
            if( defenseRolls[0] <= defender.BS)
            {
                defenseRolls[1] = 1;
                attackerDamage++;
            }

            else
            {
                defenseRolls[1] = 0;
            }

            // Negate Attack rolls
            for (int x = 0; x < attackRolls.GetLength(0); x++)
            {
                if (attackRolls[x,1] != 0)
                {
                    if (attackRolls[x,0] < defenseRolls[0] && defenseRolls[1] != 0)
                    {
                        attackRolls[x,1] = 2;
                        defenderDamage--;
                    }
                    else if (attackRolls[x,0] == defenseRolls[0] && defenseRolls[1] != 0)
                    {
                        attackRolls[x,1] = 2;
                        attackerDamage--;
                        defenderDamage--;
                    }
                    else if (defenseRolls[1] == 1)
                    {
                        defenseRolls[1] = 2;
                        attackerDamage--;
                    }
                }
            }

            // Print Results
            Console.WriteLine("Attacker Skill: {0}     Defender Skill: {1}", attacker.BS, defender.BS);
            Console.WriteLine("\nAttacker Rolls:");
            for( int x = 0; x < attackRolls.GetLength(0); x++)
            {
                Console.Write("{0}: ", attackRolls[x,0]);
                if(attackRolls[x,1] == 0)
                {
                    Console.Write("Miss\n");
                }
                else if(attackRolls[x,1] == 1)
                {
                    Console.Write("Hit\n");
                }
                else
                {
                    Console.Write("Negated\n");
                }
            }

            Console.WriteLine("\nDefender Roll:");
            Console.Write("{0}: ", defenseRolls[0]);
            if(defenseRolls[1] == 0)
            {
                Console.Write("Miss\n");
            }
            else if(defenseRolls[1] == 1)
            {
                Console.Write("Hit\n");
            }
            else
            {
                Console.Write("Negated\n");
            }

            ResultObject result = new ResultObject();
            result.Set(attacker, defender, attackRolls, defenseRolls, attackerDamage, defenderDamage);
            XmlSerializer serializer = new XmlSerializer(result.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, result);

            PrintResult(writer.ToString());


        }

        public void PrintResult(String printResult)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ResultObject));
            TextReader reader = new StringReader(printResult);
            ResultObject result = (ResultObject)serializer.Deserialize(reader);

            Console.WriteLine("Attacker Skill: {0}     Defender Skill: {1}", result.attacker.BS, result.defender.BS);
            Console.WriteLine("\nAttacker Rolls:");
            for (int x = 0; x < result.attackRolls.GetLength(0); x++)
            {
                Console.Write("{0}: ", result.attackRolls[x, 0]);
                if (result.attackRolls[x, 1] == 0)
                {
                    Console.Write("Miss\n");
                }
                else if (result.attackRolls[x, 1] == 1)
                {
                    Console.Write("Hit\n");
                }
                else
                {
                    Console.Write("Negated\n");
                }
            }

            Console.WriteLine("\nDefender Roll:");
            Console.Write("{0}: ", result.defenseRolls);
            if (result.defenseRolls[1] == 0)
            {
                Console.Write("Miss\n");
            }
            else if (result.defenseRolls[1] == 1)
            {
                Console.Write("Hit\n");
            }
            else
            {
                Console.Write("Negated\n");
            }

            Console.WriteLine("Attacker takes {0} damage.", result.attackerDamage);
            Console.WriteLine("Defender takes {0} damage", result.defenderDamage);
        }
    }
}
