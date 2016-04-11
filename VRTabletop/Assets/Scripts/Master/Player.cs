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
        protected GM GameMaster; //Global GM
        [SerializeField] protected List<BasePawn> ThisPlayersPawns;
        [SerializeField] protected BasePawn SelectedPawn;
        protected bool isTurn;
        [SerializeField] protected BasePawnValidator PV;
        [SerializeField] protected Mode m;
        [SerializeField] PointerController PC;

        void Start() {
            m = Mode.Select;
        }

        public void setGM(GM G) {
            GameMaster = G;
        }

        public void InputCheck() {
            ChangeMode();
            if (Input.GetMouseButtonDown(0) && m == Mode.Select) {
                SelectPawn(PC.ClickOnGameObject(Input.mousePosition));                
            }
            if (PV.hasPawn()) {
                ControlPawn();
            }
            if(Input.GetKeyDown(KeyCode.Space)) {
                SendOrder();
            }
        }

        void ChangeMode() {
            if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.F2)) {
                PV.StopValidation();
            }

            if (Input.GetKeyDown(KeyCode.F1) && SelectedPawn != null) {
                m = Mode.MoveMode;
                GameMaster.SetCamMode(false);
            } else if (Input.GetKeyDown(KeyCode.F2) && SelectedPawn != null) {
                m = Mode.ShootMode;
                GameMaster.SetCamMode(true);
            } else if (Input.GetKeyDown(KeyCode.F3)) {
                m = Mode.Select;
                if (SelectedPawn != null) GameMaster.SetCamMode(false);
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
                    //do somethin!
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

        void SelectPawn(GameObject Obj) {
            if(Obj != null) {
                BasePawn P = Obj.GetComponentInParent<BasePawn>();
                if (P != null) {
                    if (ValidateSelectedPawn(P)) {

                        SelectedPawn = P;
                        GameMaster.SetFPSCam(P.PawnCam);
                        PV.BPValidatorSetup(P);
                        //Graphicy stuff!
                    }
                }
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
            if (m != Mode.Select) {
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
