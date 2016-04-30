using UnityEngine;
using System.Collections;

namespace VRTabletop.Utils {
    public class PointerController : MonoBehaviour {

        [SerializeField]
        protected LineRenderer DebugLine;

        public void setDebugLine(LineRenderer R) {
            DebugLine = R;
        }

        //ONLY USE THIS WITH THE "OVERVIEW" CAM! NOT THE FPS CAM!
        public GameObject ClickOnGameObject(Vector3 MousePos) {
            Ray r = Camera.main.ScreenPointToRay(MousePos);
            RaycastHit RH = new RaycastHit();
            if(Physics.Raycast(r, out RH, 1000f)) {
                return RH.transform.gameObject;
            }
            return null;
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

        public GameObject PointAtObject(float range, bool LasTest)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit H;

            if (LasTest) TurnDebugOn(ray);

            if (Physics.Raycast(ray, out H, range))
            {
                if (LasTest) DebugLine.SetPosition(1, H.point);
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

