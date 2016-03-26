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

        public void ExecuteCommand(Response R) {
            HP += R.HPChange;
            if (HP < 0) {
                Destroy(this.gameObject);
                return;
            }
            transform.position = R.getNewPosition();
            //And anything else we need to apply
        }



    }
}

