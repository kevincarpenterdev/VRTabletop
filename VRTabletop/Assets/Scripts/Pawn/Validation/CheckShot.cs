using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;

namespace VRTabletop.Pawns.Validation {
    public class CheckShot : MonoBehaviour, IValidator<BasePawn> {
        [SerializeField] protected bool Test;
        [SerializeField] protected LineRenderer Laser;
        [SerializeField] BasePawn Target;
        [SerializeField] protected bool Validating;

        // Use this for initialization
        void Start() {
            Test = true;
            Validating = false;
        }

        void Update() {
            if (Validating) Target = CastRay();
        }

        public BasePawn CheckValid() {
            if (Target != null) {
                return Target;
            } else {
                return null;
            }
        }

        public void StartValidation() {
            Validating = true;
        }

        public void StopValidation() {
            Validating = false;
        }

        public BasePawn CastRay() {
            Ray ray = new Ray(transform.position , transform.forward);
            RaycastHit h;

            if(Test) TurnOnLaser(ray);
            //Adjust  as nessecary
            if (Physics.Raycast(ray , out h , 100)) {
                if (Test) Laser.SetPosition(1 , h.point);
                Target = h.collider.gameObject.GetComponentInParent<BasePawn>();
                if (Target != null) {
                    return Target;
                }
            }
            return null;
        }


        //Testing stuff
        protected void TurnOnLaser(Ray r) {
            Laser.enabled = true;
            Laser.SetPosition(0 , r.origin);
        }

        protected void TurnOffLaser() {
            Laser.enabled = false;
        }
    }
}

