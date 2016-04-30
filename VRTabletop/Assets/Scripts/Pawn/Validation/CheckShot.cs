using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;
using VRTabletop.Utils;

namespace VRTabletop.Pawns.Validation {
    public class CheckShot : MonoBehaviour, IValidator<BasePawn> {
        [SerializeField] BasePawn Target;
        [SerializeField] protected bool Validating;
        [SerializeField] protected PointerController PC;


        // Use this for initialization
        void Start() {
            Validating = false;
        }

        void Update() {
            //if (Validating) Target = CastRay();
        }

        public BasePawn CheckValid() {
            Target = CastRay();
            if (Target != null) {
                return Target;
            } else {
                return null;
            }
        }

        public void StartValidation() {
            Validating = true;
        }

        public void SetPointer(PointerController P){
            PC = P;
        }

        public void StopValidation() {
            Validating = false;
        }

        public IValidator<BasePawn> RetrieveValidator() {
            return this;
        }

        public BasePawn CastRay() {
            GameObject GO = PC.PointAtObject(100 , false);
            if (GO != null) {
                BasePawn P = GO.GetComponentInParent<BasePawn>();
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

