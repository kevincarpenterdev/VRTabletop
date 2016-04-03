using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRTabletop.Pawns;
using VRTabletop.Communications;
using UnityEngine;

namespace VRTabletop.Utils {
    public class OrderFactory {

        //Generally for Shooting or for Ability casts
        public Order createOrder(CommandType Command , BasePawn P , int Value) {
            switch(Command) {
                case CommandType.NonTargetAbility:
                    throw new NotImplementedException();
                case CommandType.TargetAbility:
                    return new AttackOrder(P.ID , Value);
                default:
                    throw new InvalidOperationException();
            }
        }
        //Movement
        public Order createOrder(BasePawn P, Transform Loc) {
            return new MoveOrder(P.ID , Loc);
        }
    }
}
