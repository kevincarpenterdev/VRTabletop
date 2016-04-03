using UnityEngine;
using System.Collections;

namespace VRTabletop.Communications {
    public abstract class Order  {

        public int TargetID { get; protected set; }


        //Perhaps some utils?
        public string serialize() {
            return "";
        }
    }
}

