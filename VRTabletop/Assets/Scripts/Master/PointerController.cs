using UnityEngine;
using System.Collections;

namespace VRTabletop.Utils {
    public class PointerController : MonoBehaviour {

        [SerializeField]
        protected LineRenderer DebugLine;

        public void setDebugLine(LineRenderer R) {
            DebugLine = R;
        }

        public GameObject PointAtObject(float range, Transform location, bool LasTest) {
            Ray ray = new Ray(location.position , location.transform.forward);
            RaycastHit H;

            if (LasTest) TurnDebugOn(ray);

            if(Physics.Raycast(ray,out H, range)) {
                if (LasTest) DebugLine.SetPosition(1 , H.point);
                return H.collider.gameObject;
            }
            return null;
        }

        protected void TurnDebugOn(Ray r) {
            DebugLine.enabled = true;
            DebugLine.SetPosition(0 , r.origin);
        }

        protected void TurnDebugOff() {
            DebugLine.enabled = false;
        }
        
    }
}

