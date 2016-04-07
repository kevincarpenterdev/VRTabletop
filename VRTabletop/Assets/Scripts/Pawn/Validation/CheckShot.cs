using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;
using VRTabletop.Utils;

namespace VRTabletop.Pawns.Validation {
    public class CheckShot : MonoBehaviour, IValidator<BasePawn> {
        [SerializeField] protected bool Test;
        [SerializeField] protected LineRenderer Laser;
        [SerializeField] BasePawn Target;
        [SerializeField] protected bool Validating;
        [SerializeField] protected PointerController PC;


        // Use this for initialization
        void Start() {
            Test = true;
            Validating = false;
            PC.setDebugLine(Laser);
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

        public IValidator<BasePawn> RetrieveValidator() {
            return this;
        }

        public BasePawn CastRay() {
            GameObject GO = PC.PointAtObject(100 , transform , Test);
            if (GO != null) {
                BasePawn P = GO.GetComponent<BasePawn>();
                if(P != null) {
                    return P;
                } else {
                    return null;
                }
            } else {
                return null;
            }
                 
        }
    }
}

