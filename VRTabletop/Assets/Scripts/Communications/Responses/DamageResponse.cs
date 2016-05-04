using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VRTabletop.Communications {
    public class DamageResponse : Response {

        public int HPChange { get; private set; }
        public int DefenderHPChange { get; set; }

        public DamageResponse(int i, int h, int h2, CommandType CT) {
            AppliedID = i;
            HPChange = h;
            DefenderHPChange = h2;
            CMD = CT;
        }

        public override void Apply() {
            throw new NotImplementedException();
        }

        public int GetHPChange()
        {
            return HPChange;
        }

        public int GetDefenderHPChange()
        {
            return DefenderHPChange;
        }

        public void SetHPChange(int i)
        {
            HPChange = i;
        }

        public void SetAppliedID(int i)
        {
            AppliedID = i;
        }
    }
}
