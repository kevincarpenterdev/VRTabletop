using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;
using VRTabletop.Pawns.Validation;
using VRTabletop.Communications;
using VRTabletop.Utils;

namespace VRTabletop.Clients {
    public class Player : MonoBehaviour {
        //Organize this better
        public static GM GameMaster; //Global GM
        protected List<BasePawn> ThisPlayersPawns;
        [SerializeField] protected BasePawn SelectedPawn;
        protected bool isTurn;
        [SerializeField] protected BasePawnValidator PV;
        [SerializeField] protected Mode m;
        [SerializeField] PointerController PC;

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
            if(Input.GetKeyDown(KeyCode.Space)) {
                SendOrder();
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

                }
            } else if (m == Mode.MoveMode) {
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

        //For making sure the pawn is that player's pawn
        private bool ValidateSelectedPawn(BasePawn P) {
            foreach(BasePawn B in ThisPlayersPawns) {
                if(P.ID == B.ID) {
                    return true;
                }
            }
            return false;
        }

        void SendOrder() {
            if (m != Mode.None) {
                if (m == Mode.MoveMode) {
                    Order O = PV.SendOrder(CommandType.Movement);
                    GameMaster.AcquireOrder(O);
                } else if (m == Mode.ShootMode) {
                    Order O = PV.SendOrder(CommandType.TargetAbility);
                    GameMaster.AcquireOrder(O);
                }
            } else {
                Debug.Log("Can't send an order like that!");
            }
        }
    }

}
