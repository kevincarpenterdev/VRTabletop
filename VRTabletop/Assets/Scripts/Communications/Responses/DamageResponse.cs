using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VRTabletop.Communications {
    public class DamageResponse : Response {

        public int HPChange { get; private set; }

        public DamageResponse(int i, int h, CommandType CT) {
            AppliedID = i;
            HPChange = h;
            CMD = CT;
        }

        public override void Apply() {
            throw new NotImplementedException();
        }
    }
}
