using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VRTabletop.Communications {
    class MoveOrder : Order {

        //XYZ Target
        public float TarX { get; private set; }
        public float TarY { get; private set; }
        public float TarZ { get; private set; }
        //Movement Constructor

        public MoveOrder(int i , Transform target) {
            TargetID = i;
            TarX = target.position.x;
            TarY = target.position.y;
            TarZ = target.position.z;
        }
    }
}
