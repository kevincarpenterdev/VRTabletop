using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VRTabletop.Communications;

namespace VRTabletop.Utils {
    public class ResponseFactory {

        //Client-only version
        public Response GenerateNonValidResponse(Order O) {
            //Temp
            if(O is MoveOrder) {
                MoveOrder MO = (MoveOrder)O;
                return new MoveResponse(O.TargetID , new Vector3(MO.TarX,MO.TarY,MO.TarZ), CommandType.Movement);
            } else if(O is AttackOrder) {
                AttackOrder AO = (AttackOrder)O;
                return new DamageResponse(O.TargetID , AO.HPChange , CommandType.TargetAbility);
            }
            throw new InvalidOperationException();
        }
    }
}
