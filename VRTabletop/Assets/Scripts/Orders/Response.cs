using UnityEngine;
using System.Collections;

namespace VRTabletop.Communications {
    public class Response  {

        public int AppliedID { get; private set; }

        protected float movX;
        protected float movY;
        protected float movz;

        public int HPChange { get; private set; }
        //Other Stats changed

        public Response(float x, float y, float z, int i, int h) {
            AppliedID = i;

            movX = x;
            movY = y;
            movz = z;

            HPChange = h;
        }


        public Vector3 getNewPosition() {
            return new Vector3(movX , movY , movz);
        }

    }
}

