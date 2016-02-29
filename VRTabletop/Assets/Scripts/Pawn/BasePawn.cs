using UnityEngine;
using System.Collections;
using System;

namespace VRTabletop.Pawns {
    public class BasePawn : ICommandable {

        public bool ValidateCommand() {
            Debug.Log("validate!");
            //What kind of command are we passing through?
            bool cmd = grabCommand();
            if (cmd) {
                return true;
            } else {
                ResetPostion();
                return false;
            }

        }

        protected bool grabCommand() {
            return false;
        }

        private void ResetPostion() {
            Debug.Log("I'mma resettin mah position!");
            //Set old transform to the 
        }

        public void ExecuteCommand() {
            Debug.Log("I'mma followin your orders!");
            //DO IT!
        }

    }
}

