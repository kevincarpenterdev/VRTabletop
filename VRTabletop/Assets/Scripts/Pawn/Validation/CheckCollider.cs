﻿using UnityEngine;
using System.Collections;


namespace VRTabletop.Pawns.Validation {
    public class CheckCollider : MonoBehaviour {
        [SerializeField] Collider coll;
        [SerializeField] Transform PrevTr;
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
            }
        }

        void OnTriggerExit(Collider Other) {
            BasePawn PawnCheck = Other.gameObject.GetComponent<BasePawn>();
            Obstacle OCheck = Other.gameObject.GetComponent<Obstacle>();

            if (PawnCheck == null && OCheck == null) {
                valid = true;
            }
        }
    }
}

