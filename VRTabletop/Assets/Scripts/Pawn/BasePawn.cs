using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;

namespace VRTabletop.Pawns {
    public class BasePawn : MonoBehaviour, ICommandable {
        //Data Stuff
        public int ID { get;  private set; }
        public bool ValidateMode { get; private set; }
        CommandType C;

        //Game Stuff
        public int HP;
        //TODO Impliment other stats

        //Unity Stuff
        [SerializeField]
        protected CapsuleCollider CheckCollider;
        [SerializeField]
        protected CheckShot CS;
        // Update is called once per frame
        void Update() {
            if(ValidateMode) {
                bool isValid;
                isValid = ValidateCommand();
                if (!isValid && C == CommandType.Movement) {
                    ResetPostion();
                }
            }
        }


        public bool ValidateCommand() {
            bool valid = false;

            switch (C) {
                case CommandType.Movement:
                    valid = PawnValidator.ValidatePosition(this);
                    break;
                case CommandType.Shooting:
                    valid = PawnValidator.ValidateShot(CS);
                    break;
                case CommandType.NonTargetAbility:
                    valid = PawnValidator.ValidateAbility();
                    break;
                default:
                    Debug.Log("Nothin here!");
                    break;
            }
            return valid;
        }

        public void SetCommand(CommandType T) {
            C = T;
        }

        private void ResetPostion() {
            Debug.Log("I'mma resettin mah position!");
            throw new NotImplementedException();
            //Set old transform to the 
        }

        public void ExecuteCommand(Response R) {
            HP += R.HPChange;
            if (HP<=0) {
                Destroy(this);
                return;
            }
            transform.position = R.getNewPosition();


            Debug.Log("I'mma followin your orders!");
            throw new NotImplementedException();
            //DO IT!
        }

    }
}

