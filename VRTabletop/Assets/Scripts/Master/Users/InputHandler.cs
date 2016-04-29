﻿using UnityEngine;
using System.Collections;


namespace VRTabletop.Utils {
    public static class InputHandler {

        public static bool Select() {
            if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("XOA")) {
                return true;
            }
            return false;
        }

        public static bool InputPerspectiveChange() {
            if (Input.GetMouseButtonDown(1) || Input.GetButton("XOX")) {
                return true;
            } 
            return false;
            
        } 

        public static float[] InputWorldSpaceMove(float scale) {
            float x = 0f;
            float z = 0f;

            //Xbox One logic
            if(Input.GetAxis("XOLHorz") != 0 || Input.GetAxis("XOLVert") != 0) {
                return new float[] { Input.GetAxis("XOLVert") * scale , Input.GetAxis("XOLHorz") * scale };
            } else {
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
            }
            return new float[] {x,z};
        }
        
    }

}

