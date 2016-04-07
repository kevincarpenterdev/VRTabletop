using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;
using VRTabletop.Pawns.Validation;

namespace VRTabletop.Clients {
    public class Player : Human {
        //Organize this better
        public static GM GameMaster; //Global GM
        protected List<BasePawn> ThisPlayersPawns;
        protected bool isTurn;
        public BasePawnValidator PV;
        [SerializeField] protected Mode m;

        void Start() {
            m = Mode.None;
        }


        public void Control() {
            //Select pawn validation, see ChronoCommand
            if(PV.hasPawn()) {

            }
        }

        public void InputCheck() {

        }

        void ControlPawn() {
           if (m == Mode.ShootMode) {                 
                //We'll need a VR Controller, We'll cross this bridge once we get to the rift
                if (GameMaster.VR == VRState.Overview) {
                             //Set VR Camera Active
                             //Set Player Cam inactive
                             GameMaster.setVRState(VRState.InPawn);
                         } else {
                             if (Input.GetKey(KeyCode.Q)) {
                                 //ControlledPawn.LookAtPawn(TargetPawn);
                             }
                         }
                     } else 
                if (m == Mode.MoveMode) {
                float x = 0f;
                float z = 0f;
                if (Input.GetKey(KeyCode.W)) {
                    x -= PV.pawnValidatorSpeed;
                }
                if (Input.GetKey(KeyCode.A)) {
                    z -= PV.pawnValidatorSpeed;
                }
                if (Input.GetKey(KeyCode.S)) {
                        x += PV.pawnValidatorSpeed;
                }
                if (Input.GetKey(KeyCode.D)) {
                        z += PV.pawnValidatorSpeed;
                }
                PV.RunValidation(x , z);
            }
        }

        void SelectPawn(BasePawn P) {
            if(ValidateSelectedPawn(P)) {
                PV.BPValidatorSetup(P);
                //Graphicy stuff!
            }
        }
        
        private bool ValidateSelectedPawn(BasePawn P) {
            return false;
        }

        void SendOrder() {
            
            bool valid = true;
            //Bool Valid = ICommandable.ValidateCommand(order);
            if (valid) {
                if(GameMaster.localGame) {
                    SendOrderLocal();
                } else {
                    SendOrderServer();
                }
            }
        }

        void SendOrderLocal() {
            //Prototype Controller

        }

        void SendOrderServer() {
            //Server Send ze command to ze Server!!
            //Serialize the order
            //Send the Command
        }

    }

}
