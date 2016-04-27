using UnityEngine;
using System.Collections;


namespace VRTabletop.Utils {
    public static class InputHandler {

        /*public static bool Select() {
            if()
        }

        public static bool InputPerspectiveChange() {

        } */

        public static float[] InputWorldSpaceMove(float scale) {
            float x = 0f;
            float z = 0f;

            if (Input.GetKey(KeyCode.W)) {
                x -= scale;
            }
            if (Input.GetKey(KeyCode.A)) {
                z -= scale;
            }
            if (Input.GetKey(KeyCode.S)) {
                x += scale;
            }
            if (Input.GetKey(KeyCode.D)) {
                z += scale;
            }
            //Xbox One logic
            return new float[] {x,z};
        }
        
    }

}

