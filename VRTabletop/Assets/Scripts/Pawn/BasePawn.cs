using UnityEngine;
using System.Collections;
using System;

namespace VRTabletop.Pawns {
    public class BasePawn : ICommandable {
        public bool ValidateMode { get; private set; }
        CommandType C;

        // Update is called once per frame
        void Update() {
            if(ValidateMode) {
                ValidateCommand();
            }

        }


        public bool ValidateCommand() {

            switch (C) {
                case CommandType.Movement:
                    PawnValidator.ValidatePosition(this);
                    break;
                case CommandType.Shooting:
                    PawnValidator.ValidateShot(this);
                    break;
                case CommandType.NonTargetAbility:
                    PawnValidator.ValidateAbility();
                    break;
                default:
                    Debug.Log("Nothin here!");
                    break;
            }

            throw new NotImplementedException();

            /*if (cmd is valid) {
                return true;
            } else {
                ResetPostion();
                return false;
            } */

        }

        private void ResetPostion() {
            Debug.Log("I'mma resettin mah position!");
            throw new NotImplementedException();
            //Set old transform to the 
        }

        public void ExecuteCommand() {
            Debug.Log("I'mma followin your orders!");
            throw new NotImplementedException();
            //DO IT!
        }

    }
}

