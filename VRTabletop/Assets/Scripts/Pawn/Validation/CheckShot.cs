using UnityEngine;
using System.Collections;
using VRTabletop.Pawns;

namespace VRTabletop.Pawns.Validation {
    public class CheckShot : MonoBehaviour {
        [SerializeField]
        protected bool Test;
        [SerializeField]
        protected LineRenderer Laser;

        // Use this for initialization
        void Start() {

        }

        public GameObject CastRay() {
            Vector3 Direction = transform.forward;
            Ray ray = new Ray(transform.position , Direction);
            RaycastHit h;

            if(Test) TurnOnLaser(ray);
            //Adjust "10" as nessecary
            if (Physics.Raycast(ray , out h , 10)) {
                BasePawn B = h.collider.gameObject.GetComponent<BasePawn>();
                if (B != null) {
                    return h.collider.gameObject;
                }
            }
            return null;
        }

        protected void TurnOnLaser(Ray r) {
            Laser.enabled = true;
            Laser.SetPosition(0 , r.origin);
        }
    }
}

