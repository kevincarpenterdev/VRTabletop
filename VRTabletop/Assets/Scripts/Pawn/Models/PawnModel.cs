using UnityEngine;
using System.Collections;

namespace VRTabletop.Pawns {
    public class PawnModel : MonoBehaviour {

        //I gave em the full name mainly for readability

        public int HP;
        public int BallisticSkill;
        public int CloseCombat;
        public int Physique;
        public int Willpower;
        public float MoveRange;

        public string PawnName;
        public int OrderAmt;
        public int OrderRefreshAmt;


        //weapon statistics
        public WeaponModel Weapon;

    }
}

