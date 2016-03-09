using UnityEngine;
using System.Collections;
using System;

namespace VRTabletop.Pawns {
    public static class PawnValidator {

        public static bool ValidatePosition(BasePawn P) {
            /*TODO 
                Grab BasePawn's "Check Collider"
                See if it hit anything
            */  
            throw new NotImplementedException();
        }

        public static bool ValidateShot(BasePawn P) {
            /*TODO
                Grab the "Head" Game object
                See if it can draw a straight ray from the target to the Valid Target
            */
            throw new NotImplementedException();
        }

        public static bool ValidateAbility() {
            /*TODO
                Just like Validate Position, only Validating Ability
            */
            throw new NotImplementedException();
        }
    
    }
}

