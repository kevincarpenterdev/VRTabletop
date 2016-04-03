using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace VRTabletop.Communications {
    class AttackOrder: Order {

        public int HPChange;

        //shooting contstructor
        public AttackOrder(int i , int h) {
            TargetID = i;
            HPChange = h;
        }
    }
}
