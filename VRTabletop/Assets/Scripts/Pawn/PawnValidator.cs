using UnityEngine;
using System.Collections;
using System;
using VRTabletop.Communications;

namespace VRTabletop.Pawns {
    public static class PawnValidator {

        
        public static bool ValidatePosition(BasePawn P) {
            /*TODO 
                Grab BasePawn's "Check Collider"
                See if it hit anything
            */  
            throw new NotImplementedException();
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

