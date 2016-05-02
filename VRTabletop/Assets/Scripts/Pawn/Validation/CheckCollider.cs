using UnityEngine;
using System.Collections;


namespace VRTabletop.Pawns.Validation {
    public class CheckCollider : MonoBehaviour, IValidator<Transform> {
        [SerializeField] protected Collider coll;
        [SerializeField] protected Transform PrevTr;
        [SerializeField] protected Material ValidMat;
        [SerializeField] protected Material InvalidMat;
        [SerializeField] protected CharacterController CC;
        [SerializeField] protected GameObject VisualCheck;

        [SerializeField] protected float r;
        protected bool valid;
        protected bool FirstMove;



        void Start()
        {
            r = GetComponentInParent<PawnModel>().MoveRange;   
        }

        public Transform CheckValid(float range) {
            if(valid) {
                return transform;
            } else {
                return PrevTr;
            }
        }

        public void Setup()
        {
            FirstMove = true;
            valid = false;
            ChangeMat();
        }

        public bool RangeFind()
        {
            if(Vector3.Distance(transform.position, PrevTr.position) <= r)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void StartValidation() {
            VisualCheck.SetActive(true);

            ChangeMat();
        }

        public void StopValidation() {
            VisualCheck.SetActive(false);
            transform.position = PrevTr.position;
        }

        public IValidator<Transform> RetrieveValidator() {
            return this;
        }

        public Transform GrabTransform() {
            if(valid && FirstMove == false) {
                return transform;
            } else {
                return null;
            }
        }

        /* void OnTriggerStay(Collider Other) {

            BasePawn PawnCheck = Other.gameObject.GetComponent<BasePawn>();
            Obstacle OCheck = Other.gameObject.GetComponent<Obstacle>();

            if (PawnCheck != null || OCheck != null) {
                valid = false;
            }
        }

        void OnTriggerExit(Collider Other) {
            BasePawn PawnCheck = Other.gameObject.GetComponent<BasePawn>();
            Obstacle OCheck = Other.gameObject.GetComponent<Obstacle>();

            if (PawnCheck == null && OCheck == null) {
                valid = true;
            }
        } */
        public void ChangeMat() {
            if (valid) {
                VisualCheck.GetComponent<MeshRenderer>().material = ValidMat;
            } else{
                VisualCheck.GetComponent<MeshRenderer>().material = InvalidMat;
            }
        }

        public void MoveChecker(float x, float z) {
            CC.Move(new Vector3(x ,0f, z)*Time.deltaTime);
            if (x > 0f || z > 0f) FirstMove = false;
            valid = RangeFind();
            ChangeMat();
        }
    }
}


