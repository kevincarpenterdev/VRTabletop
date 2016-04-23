using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

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
        protected InputHandler IH;

        void Start() {
            IH = new InputHandler();
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
            if (PV.hasPawn() && m != Mode.Select) {         
                ControlPawn();
            }
            if(Input.GetKeyDown(KeyCode.Space)) {
                SendOrder();
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                GameMaster.passTurn();
            }
        }

        CommandType GetCT() {
            if(m == Mode.ShootMode) {
               return CommandType.TargetAbility;
            }
            if(m == Mode.MoveMode) {
               return CommandType.Movement;
            } else {
                throw new InvalidOperationException();
            }
        }

        void ChangeMode() {
            if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.F2)) {
                PV.StopValidation();
            }

            if (Input.GetKeyDown(KeyCode.F1) && SelectedPawn != null) {
                m = Mode.MoveMode;
                GameMaster.SetCamMode(false);
                GameMaster.setVRState(VRState.Overview);
            } else if (Input.GetKeyDown(KeyCode.F2) && SelectedPawn != null) {
                m = Mode.ShootMode;
                GameMaster.SetCamMode(true);
                GameMaster.setVRState(VRState.InPawn);
            } else if (Input.GetKeyDown(KeyCode.F3)) {
                m = Mode.Select;
                if (SelectedPawn != null) GameMaster.SetCamMode(false);
                GameMaster.setVRState(VRState.Overview);
            }
        }

        void ControlPawn() {
           if (m == Mode.ShootMode) {
                PV.RunValidation();                 
                //We'll need a VR Controller, We'll cross this bridge once we get to the rift
                if (GameMaster.getVRState() == VRState.InPawn) {
                    //Set VR Camera Active
                    //Set Player Cam inactive
                } else {
                    float rx = 0f;
                    float ry = 0f;
                    if (Input.GetKey(KeyCode.W)) {
                        rx -= .5f;
                    }
                    if (Input.GetKey(KeyCode.A)) {
                        ry -= .5f;
                    }
                    if (Input.GetKey(KeyCode.S)) {
                        rx += .5f;
                    }
                    if (Input.GetKey(KeyCode.D)) {
                        ry += .5f;
                    }
                    SelectedPawn.RotateHead(rx,ry);
                }
            } else if (m == Mode.MoveMode) {
                float x = 0f;
                float z = 0f;
                //We need better solution than hardcoded #
                if (Input.GetKey(KeyCode.W)) {
                    x -= 5;
                }
                if (Input.GetKey(KeyCode.A)) {
                    z -= 5;
                }
                if (Input.GetKey(KeyCode.S)) {
                    x += 5;
                }
                if (Input.GetKey(KeyCode.D)) {
                    z += 5;
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
                        GameMaster.SetFPSCam(P.GetHead());
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
                    if (O != null) {
                        GameMaster.AcquireOrder(O);
                    } else {
                        Debug.Log("Not a valid shot!");
                    }
                }
            } else {
                Debug.Log("Can't send an order like that!");
            }
        }
    }

}
