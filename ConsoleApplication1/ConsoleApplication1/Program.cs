using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleMethods battle = new BattleMethods();

            BattleObject attacker = new BattleObject();
            attacker.action = "shoot";
            attacker.BS = 14;
            attacker.burst = 3;
            attacker.CC = 12;
            attacker.PH = 12;

            BattleObject defender = new BattleObject();
            defender.action = "shoot";
            defender.BS = 13;
            defender.burst = 1;
            defender.CC = 12;
            defender.PH = 12;

            battle.ShootAndShoot(attacker, defender);

            Console.ReadLine();
        }
    }
}
