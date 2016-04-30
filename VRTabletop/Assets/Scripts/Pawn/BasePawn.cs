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
        [SerializeField] protected CheckShot CS;
        [SerializeField] protected PawnModel PM;
        
        [SerializeField]
        protected GameObject Head;

        public PawnModel GetPawnModel() {
            return PM;
        } 

        public WeaponModel GetWeapon() {
            return PM.Weapon;
        }

        //For M+K Testing
        public void RotateHead(float x, float y) {
            //May need to set up a Quaternion for this but this is good for now
            
            Head.transform.Rotate(x , y , 0f);
        }

        public Transform GetHead() {
            return Head.transform;
        }


        public void ExecuteCommand(Response R) {
            switch(R.CMD) {
                case CommandType.Movement:
                    MoveResponse MovRes = (MoveResponse)R;
                    transform.position = MovRes.getNewPosition();
                    break;
                case CommandType.TargetAbility:
                    DamageResponse DamRes = (DamageResponse)R;
                    PM.HP += DamRes.HPChange;
                    if (PM.HP < 0) {
                        Destroy(this.gameObject);
                        return;
                    }
                    break;
            }

            //And anything else we need to apply
        }
    }
}

