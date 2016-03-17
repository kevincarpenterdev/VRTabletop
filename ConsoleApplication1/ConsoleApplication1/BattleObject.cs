using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class BattleObject
    {
        public int BS { get; set; }
        public int CC { get; set; }
        public int PH { get; set; }
        public int burst { get; set; }
        public string action { get; set; }
    }
}
