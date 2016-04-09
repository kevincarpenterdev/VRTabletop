using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;
using VRTabletop.Pawns.Validation;
using VRTabletop.Communications;

namespace VRTabletop.Clients {
    public class Player : MonoBehaviour {
        //Organize this better
        public static GM GameMaster; //Global GM
        protected List<BasePawn> ThisPlayersPawns;
        protected BasePawn SelectedPawn;
        protected bool isTurn;
        public BasePawnValidator PV;
        [SerializeField] protected Mode m;

        void Start() {
            m = Mode.None;
        }


        public void InputCheck() {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Click!");
            }
            if (PV.hasPawn()) {
                ControlPawn();
            }
        }

        void ControlPawn() {
           if (m == Mode.ShootMode) {                 
                //We'll need a VR Controller, We'll cross this bridge once we get to the rift
                if (GameMaster.VR == VRState.Overview) {
                             //Set VR Camera Active
                             //Set Player Cam inactive
                             GameMaster.setVRState(VRState.InPawn);
                         } else {
                             /*if (Input.GetKey(KeyCode.Q)) {
                                 //ControlledPawn.LookAtPawn(TargetPawn);
                             } */
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
            //Order O = PV.SendOrder();
            //See Prototype Controller
        }
    }

}
