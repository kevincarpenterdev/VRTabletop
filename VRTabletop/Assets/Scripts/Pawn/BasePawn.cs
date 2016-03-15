using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;
using VRTabletop.Pawns.Validation;

namespace VRTabletop.Pawns {
    public class BasePawn : MonoBehaviour, ICommandable {
        //Data Stuff
        public int ID { get;  private set; }
        public bool ValidateMode { get; private set; }
        public bool isValid { get; private set; }
        CommandType C;

        //Game Stuff
        public int HP;
        //TODO Impliment other stats

        //Unity Stuff
        [SerializeField]
        protected CapsuleCollider CheckCollider;
        [SerializeField]
        protected CheckShot CS;
        void Start () {
            isValid = false;
        }
        // Update is called once per frame
        void Update() {
            if(ValidateMode) {

            }
        }


        public void ValidateCommand() {

            switch (C) {
                case CommandType.Movement:
                    isValid = PawnValidator.ValidatePosition(this);
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


            Debug.Log("I'mma followin your orders!");
            throw new NotImplementedException();
            //DO IT!
        }

    }
}

