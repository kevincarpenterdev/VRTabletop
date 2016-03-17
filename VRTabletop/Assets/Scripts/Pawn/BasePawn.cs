using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;
using VRTabletop.Pawns.Validation;

namespace VRTabletop.Pawns {
    public class BasePawn : MonoBehaviour, ICommandable {
        //Data Stuff
        [SerializeField] public int ID { get;  private set; }
        public bool isValid { get; private set; }
        CommandType C;

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

        // Update is called once per frame
        /*void Update() {
          
        }*/

        /*ValidationStartups
        public void startValidation() {
            switch (C) {
                case CommandType.Movement:
                    isValid = PawnValidator.ValidatePosition(CheckCollider);
                    break;
                case CommandType.Shooting:
                    Debug.Log("No Validation needed!");
                    break;
                case CommandType.NonTargetAbility:
                    throw new NotImplementedException();
                    break;
                default:
                    Debug.Log("Nothin here!");
                    break;
            }
        }*/



        //Run this in an update loop
        public void RunValidation() {

            switch (C) {
                case CommandType.Movement:
                    isValid = PawnValidator.ValidatePosition(CheckCollider);
                    break;
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

        //Trash this method
        public void moveChecker(float x, float z) {
            CheckCollider.MoveChecker(x,z);
        }

        //And Trash this method
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
                        CheckCollider.Cancel();
                        return O;
                    case CommandType.Shooting:
                        return new Order(CS.GrabTargetID() , 100 , transform.position);
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
            if (HP<=0) {
                Destroy(this);
                return;
            }
            transform.position = R.getNewPosition();
            //And anything else we need to apply
        }

    }
}

