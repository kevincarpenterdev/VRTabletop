using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;

namespace VRTabletop.Communications {
    public class Order : MonoBehaviour {
        int TargetID;

        //Keeping them as floats for serialization reasons
        //We can't technically pass "objects" to servers

        //Movement
        float MovX;
        float MovY;
        float MovZ;

        float TarX;
        float TarY;
        float TarZ;

        public Vector3 GrabMovement() {
            return new Vector3(MovX, MovY , MovZ);
        }

        public Vector3 GrabTargetLocation() {
            return new Vector3(TarX , TarY , TarZ);
        }

    }
}

