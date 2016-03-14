using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;

namespace VRTabletop.Communications {
    public class Order : MonoBehaviour {

        int TargetID;

        //XYZ Target
        float TarX;
        float TarY;
        float TarZ;

        //Flags here

        public Vector3 GrabLocation() {
            return new Vector3(TarX , TarY , TarZ);
        }

    }
}

