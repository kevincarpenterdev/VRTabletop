using UnityEngine;
using System.Collections;

namespace VRTabletop.Communications {
    public class Response : MonoBehaviour {

        public int AppliedID { get; private set; }

        protected float movX;
        protected float movY;
        protected float movz;

        public int HPChange { get; private set; }
        //Other Stats changed

        public Vector3 getNewPosition() {
            return new Vector3(movX , movY , movz);
        }

    }
}

