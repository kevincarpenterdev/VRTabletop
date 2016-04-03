using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRTabletop.Communications {
    public class AbilityResponse : Response {

        public AbilityResponse(int i) {
            AppliedID = i;
        }

        public override void Apply() {
            throw new NotImplementedException();
        }
    }
}
