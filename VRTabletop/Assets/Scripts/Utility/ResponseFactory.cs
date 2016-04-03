using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VRTabletop.Communications;

namespace VRTabletop.Utils {
    public class ResponseFactory {
        //Server Version
        /*public Response GenerateResponse(string s) {
            throw new NotImplementedException();
        }*/

        //Client-only version
        public Response GenerateResponse(Order O, CommandType T) {

            switch(T) {
                case CommandType.Movement:
                    MoveOrder MO = (MoveOrder)O;
                    return new MoveResponse(MO.TargetID , new Vector3(MO.TarX , MO.TarY , MO.TarZ),T);
                case CommandType.TargetAbility:
                    AttackOrder AO = (AttackOrder)O;
                    return new DamageResponse(AO.TargetID , AO.HPChange , T);
                case CommandType.NonTargetAbility:
                    throw new NotImplementedException();
//                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
