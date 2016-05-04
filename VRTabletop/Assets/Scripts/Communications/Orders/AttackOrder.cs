using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace VRTabletop.Communications {
    public class AttackOrder: Order {

        public int HPChange;
        public int attackSkill;
        public int attackNum;
        public int defendSkill;
        public int attackPower;
        public int defendPower;


        //shooting contstructor
        public AttackOrder(int i , int h) {
            TargetID = i;
            HPChange = h;
        }

        //shooting constructor for rules
        public AttackOrder(int i, int a, int n, int d, int ap, int dp)
        {
            TargetID = i;
            attackNum = n;
            attackSkill = a;
            defendSkill = d;
            attackPower = ap;
            defendPower = dp;
        }
    }
}
