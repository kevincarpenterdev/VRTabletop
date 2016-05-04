using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VRTabletop.Communications;
using VRTabletop.Pawns;
using VRTabletop.Rules;

namespace VRTabletop.Utils {
    public class ResponseFactory {

        //Client-only version
        public Response GenerateNonValidResponse(Order O) {

            if(O is MoveOrder) {
                MoveOrder MO = (MoveOrder)O;
                return new MoveResponse(O.TargetID , new Vector3(MO.TarX,MO.TarY,MO.TarZ), CommandType.Movement);
            } else if(O is AttackOrder) {
                AttackOrder AO = (AttackOrder)O;
                return new DamageResponse(O.TargetID , AO.HPChange , CommandType.TargetAbility);
            }
            throw new InvalidOperationException();
        }

        //Get Attack order
        public Response GenerateNonValidResponse(Order O, PawnModel PM, PawnModel target)
        {
            GeneralRules rules = new GeneralRules();
            Roll roll = new Roll();
            AttackOrder AO = rules.GenerateAttackOrder(O, PM, target);
            return roll.Rolls(AO);
            //return new DamageResponse(O.TargetID, AO.HPChange, CommandType.TargetAbility);
        }
    }
}
