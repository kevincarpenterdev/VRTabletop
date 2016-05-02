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

        void Start() {
            m = Mode.MoveMode;
        }

        public void setGM(GM G) {
            GameMaster = G;
        }

        public void StartTurn(){
            foreach (BasePawn P in ThisPlayersPawns){
                P.RefreshOrders();
            }
        }

        public void InputCheck() {
            if(SelectedPawn != null) ChangeMode();
            if (InputHandler.Select() && (m == Mode.Select || m == Mode.MoveMode)) {
                if (GameMaster.getVRState() == VRState.Overview){
                    SelectPawn(PC.PointAtObject(1000f, Camera.main.transform, false));
                } else {
                    SelectPawn(PC.ClickOnGameObject(Input.mousePosition));
                }
            }
            if (PV.hasPawn() && m != Mode.Select) {
                if (SelectedPawn.GetPawnModel().OrderAmt > 0){
                    ControlPawn();
                } else {
                    PV.StopValidation();
                }
            }
            if(InputHandler.InputConfirm() && SelectedPawn != null) {
                if(SelectedPawn.GetPawnModel().OrderAmt > 0)
                {
                    SendOrder();
                } else
                {
                    GameMaster.SendMessageToHUD("Not enough Orders");
                }

            }
            if (InputHandler.InputPass()) {                
                if(SelectedPawn != null) {
                    PV.StopValidation();
                    PV.ClearValidators();
                    SelectedPawn = null;
                }
                GameMaster.passTurn();
            }
        }

        public void ForceOutOfFPS() {
            if(m == Mode.ShootMode) {
                m = Mode.MoveMode;
                GameMaster.SetCamMode();
                GameMaster.setVRState(VRState.Overview);
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
            if(InputHandler.InputPerspectiveChange()) {
                if(m == Mode.MoveMode && SelectedPawn != null) {
                    m = Mode.ShootMode;
                    GameMaster.SetCamMode(SelectedPawn.GetWeapon());
                    GameMaster.setVRState(VRState.InPawn);
                } else {
                    m = Mode.MoveMode;
                    GameMaster.SetCamMode();
                    GameMaster.setVRState(VRState.Overview);
                }
                PV.StopValidation();
                GameMaster.ClearMessagesInHUD();
            }
        }

        void ControlPawn() {
           if (m == Mode.ShootMode) {
                PV.RunValidation();
                if (GameMaster.getVRState() == VRState.InPawn) {
                    //Let VR Head Tracking handle it
                } else {
                    float[] rot = InputHandler.InputWorldSpaceMove(.5f);
                    SelectedPawn.RotateHead(rot[0],rot[1]);
                }
            } else if (m == Mode.MoveMode) {

                float[] mov = InputHandler.InputWorldSpaceMove(5f); 
                PV.RunValidation(mov[0] , mov[1]);
            }
        }

        void SelectPawn(GameObject Obj) {
            if(Obj != null) {
                BasePawn P = Obj.GetComponentInParent<BasePawn>();
                if (P != null) {
                    if (ValidateSelectedPawn(P)) {
                        if(SelectedPawn != null) PV.StopValidation();
                        SelectedPawn = P;
                        GameMaster.SetFPSCam(P.GetHead());
                        PV.BPValidatorSetup(P,PC);
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

        private bool ValidateSelectedPawnByID(int i)
        {
            foreach (BasePawn B in ThisPlayersPawns)
            {
                if (i == B.ID)
                {
                    return true;
                }
            }
            return false;
        }

        void SendOrder() {
            if (m != Mode.Select) {
                if (m == Mode.MoveMode) {
                    Order O = PV.SendOrder(CommandType.Movement);
                    if(O != null) {
                        GameMaster.AcquireOrder(O);
                        SelectedPawn.UseOrder();
                    } else {
                        GameMaster.SendMessageToHUD("Not a valid move");
                    }

                } else if (m == Mode.ShootMode) {
                    Order O = PV.SendOrder(CommandType.TargetAbility);
                    if (O != null && !ValidateSelectedPawnByID(O.TargetID)) {
                        GameMaster.AcquireOrder(O);
                        SelectedPawn.UseOrder();
                    } else {
                        GameMaster.SendMessageToHUD("Not a valid shot!");
                    }
                }
            } else {
                GameMaster.SendMessageToHUD("Can't send an order like that!");
            }
        }
    }

}
