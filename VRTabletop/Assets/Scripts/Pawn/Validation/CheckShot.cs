using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;

namespace VRTabletop.Pawns.Validation {
    public class CheckShot : MonoBehaviour {
        [SerializeField] protected bool Test;
        [SerializeField] protected LineRenderer Laser;
        [SerializeField] BasePawn Target;

        // Use this for initialization
        void Start() {
            Test = true;
        }

        public BasePawn GrabTarget() {
            if (Target != null) {
                return Target;
            } else {
                return null;
            }
        }

        public int GrabTargetID() {
            return Target.ID;
        }

        public GameObject CastRay() {
            //Vector3 Direction = transform.position - transform.forward;
            Ray ray = new Ray(transform.position , transform.forward);
            RaycastHit h;

            if(Test) TurnOnLaser(ray);
            //Adjust "10" as nessecary
            if (Physics.Raycast(ray , out h , 100)) {
                if (Test) Laser.SetPosition(1 , h.point);
                Target = h.collider.gameObject.GetComponentInParent<BasePawn>();
                if (Target != null) {
                    return h.collider.gameObject;
                }
            }
            return null;
        }

        protected void TurnOnLaser(Ray r) {
            Laser.enabled = true;
            Laser.SetPosition(0 , r.origin);
        }

        protected void TurnOffLaser() {
            Laser.enabled = false;
        }
    }
}

