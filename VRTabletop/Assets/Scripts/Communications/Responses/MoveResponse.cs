using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VRTabletop.Communications {
    public class MoveResponse : Response {
        protected float movX;
        protected float movY;
        protected float movZ;


        public MoveResponse(int i, Vector3 Loc ,CommandType CT) {
            movX = Loc.x;
            movY = Loc.y;
            movZ = Loc.z;
            CMD = CT;
        }

        public override void Apply() {
            throw new NotImplementedException();
        }

        public Vector3 getNewPosition() {
            return new Vector3(movX , movY , movZ);
        }
    }
}
