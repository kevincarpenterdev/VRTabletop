using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;

namespace VRTabletop.Communications {
    public class Order : MonoBehaviour {

        public int TargetID { get; private set; }

        //XYZ Target
        public float TarX { get; private set; }
        public float TarY { get; private set; }
        public float TarZ { get; private set; }


        public int HPChange;
        //Flags here

        //Movement Constructor
        public Order(int i, Transform target) {
            TargetID = i;
            TarX = target.position.x;
            TarY = target.position.y;
            TarZ = target.position.z;
        }

        //shooting contstructor
        public Order(int i, int h) {
            TargetID = i;
            HPChange = h;
        }
    }
}

