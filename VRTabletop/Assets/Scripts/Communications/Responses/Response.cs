using UnityEngine;
using System.Collections;

namespace VRTabletop.Communications {
    public abstract class Response  {

        public int AppliedID { get; protected set; }
        public CommandType CMD { get; protected set; }

        public abstract void Apply();

    }
}

