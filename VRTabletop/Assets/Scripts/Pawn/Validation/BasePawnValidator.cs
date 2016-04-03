using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;
using VRTabletop.Utils;

namespace VRTabletop.Pawns.Validation {
    public class BasePawnValidator : MonoBehaviour {

        //Unity Stuff
        [SerializeField]
        protected CheckCollider CC;
        [SerializeField]
        protected CheckShot CS;

        protected BasePawn Checked;
    
        protected OrderFactory OF;

        public bool isValid { get; private set; }

        void Start() {
            isValid = false;
            OF = new OrderFactory();
        }

        public void BPValidatorSetup(BasePawn P) {
            Checked = P;
            if(Checked != null) {
                CC = Checked.GetComponentInChildren<CheckCollider>();
                CS = Checked.GetCS();
            } else {
                throw new InvalidOperationException();
            }
        }

        public void RunValidation(float x , float z , CommandType C) {
            switch (C) {
                case CommandType.Movement:
                    CC.StartValidation();
                    CC.MoveChecker(x , z);
                    break;
                case CommandType.NonTargetAbility:
                    throw new NotImplementedException();
                default:
                    RunValidation(C);
                    break;
            }
        }


        //Run this in an update loop
        public void RunValidation(CommandType C) {
            switch (C) {
                case CommandType.TargetAbility:
                    isValid = ValidateShot(CS);
                    break;
                default:
                    Debug.Log("Nothin here!");
                    isValid = false;
                    break;
            }
        }

        //Called once
        public Order SendOrder(CommandType C) {
            switch (C) {
                case CommandType.Movement:
                    Order MO = OF.createOrder(Checked , CC.GrabTransform());
                    StopValidation();
                    return MO;
                case CommandType.TargetAbility:
                    BasePawn V = CS.CheckValid();
                    if(V != null) {
                        Order SO = OF.createOrder(C , V, 10);
                        StopValidation();
                        return SO;
                    } else {
                        return null;
                    }
                case CommandType.NonTargetAbility:
                    throw new NotImplementedException();
                default:
                    Debug.Log("Nothin here!");
                    return null;
            }
        }

        public bool ValidateShot(CheckShot C) {
            if (C.CastRay() != null) {
                return true;
            } else {
                return false;
            }
        }

        public void StopValidation() {
            //PawnValidator
            CC.StopValidation();
            CS.StopValidation();
        }
    }
}

