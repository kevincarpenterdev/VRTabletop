using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;

namespace VRTabletop.Pawns.Validation {
    public class BasePawnValidator : MonoBehaviour {

        //Unity Stuff
        [SerializeField]
        protected CheckCollider CC;
        [SerializeField]
        protected CheckShot CS;

        protected BasePawn Checked;

        public bool isValid { get; private set; }

        void Start() {
            isValid = false;
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
                case CommandType.Shooting:
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
                    Order MO = new Order(Checked.ID , CC.GrabTransform());
                    StopValidation();
                    return MO;
                case CommandType.Shooting:
                    Order SO = new Order(CS.CheckValid().ID , -100 , CS.CheckValid().transform.position);
                    StopValidation();
                    return SO;
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

