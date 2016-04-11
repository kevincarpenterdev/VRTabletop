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

        public BasePawn Checked { get; private set; }
        public Camera PawnHeadCam { get; private set; }
        protected OrderFactory OF;

        public bool isValid { get; private set; }

        void Start() {
            isValid = false;
            OF = new OrderFactory();
        }

        public bool hasPawn() {
            if(Checked != null) {
                return true;
            } else {
                return false;
            }
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

        public void RunValidation(float x , float z) {
                    CC.StartValidation();
                    CC.MoveChecker(x , z);
        }


        //Run this in an update loop
        public void RunValidation() {
                    CS.StartValidation();
                    isValid = ValidateShot();
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
                        Order SO = OF.createOrder(C , V, -10);
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

        public bool ValidateShot() {
            if(CS.CheckValid() != null) {
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

