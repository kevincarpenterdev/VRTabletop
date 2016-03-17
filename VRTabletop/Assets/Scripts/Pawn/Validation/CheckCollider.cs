using UnityEngine;
using System.Collections;


namespace VRTabletop.Pawns.Validation {
    public class CheckCollider : MonoBehaviour {
        [SerializeField] protected Collider coll;
        [SerializeField] protected Transform PrevTr;
        [SerializeField] protected Material ValidMat;
        [SerializeField] protected Material InvalidMat;
        [SerializeField] protected CharacterController CC;
        [SerializeField] protected GameObject VisualCheck;



        bool valid = true;

        public bool Validate() {
            if(valid) {
                return true;
            } else {
                return false;
            }
        }

        public void Cancel() {
            transform.position = PrevTr.position;
        }

        public void startValidation() {
            VisualCheck.SetActive(true);
        }

        public void StopValidation() {
            VisualCheck.SetActive(false);
        }

        public Transform GrabTransform() {
            if(valid) {
                return transform;
            } else {
                return null;
            }
        }

        void OnTriggerStay(Collider Other) {

            BasePawn PawnCheck = Other.gameObject.GetComponent<BasePawn>();
            Obstacle OCheck = Other.gameObject.GetComponent<Obstacle>();

            if (PawnCheck != null || OCheck != null) {
                valid = false;
                VisualCheck.GetComponent<MeshRenderer>().material = InvalidMat;
            }
        }

        void OnTriggerExit(Collider Other) {
            BasePawn PawnCheck = Other.gameObject.GetComponent<BasePawn>();
            Obstacle OCheck = Other.gameObject.GetComponent<Obstacle>();

            if (PawnCheck == null && OCheck == null) {
                valid = true;
                VisualCheck.GetComponent<MeshRenderer>().material = ValidMat;
            }
        }
        public void MoveChecker(float x, float z) {
            CC.Move(new Vector3(x ,0f, z));
        }
    }
}


