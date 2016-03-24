using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;

namespace VRTabletop.Pawns.Validation {
    public static class PawnValidator {

        //Replace?
        public static bool ValidatePosition(CheckCollider C) {
            C.StartValidation();
            return C.CheckValid();
        }

        public static bool ValidateShot(CheckShot C) {
            if(C.CastRay() != null) {
                return true;
            } else {
                return false;
            }

        }

        public static bool ValidateAbility() {
            /*TODO
                Just like Validate Position, only Validating Ability
            */
            throw new NotImplementedException();
        }
    }
}

