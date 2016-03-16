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

        public Order SendOrder() {
            if(isValid) {
                switch (C) {
                    case CommandType.Movement:
                        return new Order(this.ID , CheckCollider.GrabTransform());
                    case CommandType.Shooting:
                        return new Order(CS.GrabTargetID() , 100);
                    case CommandType.NonTargetAbility:
                        throw new NotImplementedException();
                    default:
                        Debug.Log("Nothin here!");
                        return null;
                }
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

