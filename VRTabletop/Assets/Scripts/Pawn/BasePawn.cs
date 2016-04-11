using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;
using VRTabletop.Pawns.Validation;

namespace VRTabletop.Pawns {
    public class BasePawn : MonoBehaviour, ICommandable {
        //Data Stuff
        [SerializeField] public int ID; //{ get;  private set; }

        //Unity Stuff
        [SerializeField] public Camera PawnCam;
        [SerializeField] protected CheckShot CS;

        //Game Stuff
        public int HP;
        //TODO Impliment other stats
        
        public CheckShot GetCS() {
            return CS;
        }

        //And Trash this method once we get VR running
        [SerializeField]
        protected GameObject Head;
        public void LookAtPawn(BasePawn P) {
            Head.transform.LookAt(P.transform);
        }

        public void RotateHead(float x, float y) {
            //May need to set up a Quaternion for this but this is good for now
            
            Head.transform.Rotate(x , y , 0f);
        }

        public void ExecuteCommand(Response R) {
            switch(R.CMD) {
                case CommandType.Movement:
                    MoveResponse MovRes = (MoveResponse)R;
                    transform.position = MovRes.getNewPosition();
                    break;
                case CommandType.TargetAbility:
                    DamageResponse DamRes = (DamageResponse)R;
                    HP += DamRes.HPChange;
                    if (HP < 0) {
                        Destroy(this.gameObject);
                        return;
                    }
                    break;
            }

            //And anything else we need to apply
        }
    }
}

