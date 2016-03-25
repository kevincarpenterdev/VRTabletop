using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;
using VRTabletop.Pawns.Validation;

namespace VRTabletop.Pawns {
    public class BasePawn : MonoBehaviour, ICommandable {
        //Data Stuff
        [SerializeField] public int ID; //{ get;  private set; }
        public bool isValid { get; private set; }
        [SerializeField] CommandType C;

        [SerializeField]protected GameObject Head;
        
        //Game Stuff
        public int HP;
        //TODO Impliment other stats

        //Unity Stuff
        [SerializeField] protected CheckCollider CheckCollider;
        [SerializeField] protected CheckShot CS;

        void Start () {
            isValid = false;
        }

        public void RunValidation(float x , float z) {
            if (C == CommandType.Movement) {
                PawnValidator.ValidatePosition(CheckCollider);
                CheckCollider.MoveChecker(x , z);
            } else {
                Debug.LogWarning("RunValidation for movement isn't supposed to be called");
            }
        }


        //Run this in an update loop
        public void RunValidation() {
            switch (C) {
                case CommandType.Shooting:
                    isValid = PawnValidator.ValidateShot(CS);
                    break;
                case CommandType.NonTargetAbility:
                    isValid = PawnValidator.ValidateAbility();
                    break;
                default:
                    Debug.Log("Nothin here!");
                    break;
            }
        }

        //And Trash this method once we get VR running
        public void LookAtPawn(BasePawn P) {
            Head.transform.LookAt(P.transform);
        }


        //Called once
        public Order SendOrder() {
            if(isValid) {
                switch (C) {
                    case CommandType.Movement:
                        Order O = new Order(ID , CheckCollider.GrabTransform());
                        CheckCollider.StopValidation();
                        return O;
                    case CommandType.Shooting:
                        return new Order(CS.CheckValid().ID , -100 , CS.CheckValid().transform.position);
                    case CommandType.NonTargetAbility:
                        throw new NotImplementedException();
                    default:
                        Debug.Log("Nothin here!");
                        return null;
                }  
            } else {
                Debug.Log("This is not a valid command");
            }
            return null;
        }

        public void SetCommand(CommandType T) {
            C = T;
        }

        public void ExecuteCommand(Response R) {
            HP += R.HPChange;
            if (HP < 0) {
                Destroy(this.gameObject);
                return;
            }
            transform.position = R.getNewPosition();
            //And anything else we need to apply
        }

        public void StopValidation() {
            CheckCollider.StopValidation();
            CS.StopValidation();
        }

    }
}

