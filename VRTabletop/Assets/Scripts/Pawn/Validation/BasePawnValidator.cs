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
            OF = new OrderFactory();
        }

        public bool hasPawn() {
            if(Checked != null) {
                return true;
            } else {
                return false;
            }
        }

        public void BPValidatorSetup(BasePawn P,PointerController PC) {
            Checked = P;
            if(Checked != null) {
                CC = Checked.GetComponentInChildren<CheckCollider>();
                CS = Checked.GetComponentInChildren<CheckShot>();
                CS.SetPointer(PC);
                CC.Setup();
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
        }

        //Called once
        public Order SendOrder(CommandType C) {
            switch (C) {
                case CommandType.Movement:
                    Transform t = CC.GrabTransform();
                    if(t != null){
                        Order MO = OF.createOrder(Checked, t);
                        StopValidation();
                        return MO;
                    } else{
                        return null;
                    }
                case CommandType.TargetAbility:
                    BasePawn V = CS.CheckValid(Checked.GetWeapon().WeaponRange);
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

        public void StopValidation() {
            //PawnValidator
            CC.StopValidation();
            CS.StopValidation();
        }
    }
}

