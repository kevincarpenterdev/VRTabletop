using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;

namespace VRTabletop.Clients {
    public class Player : Human {
        public static GM GameMaster; //Global GM
        protected List<BasePawn> ThisPlayersPawns;

        void SendOrder() {
            bool valid = true;
            //Bool Valid = ICommandable.ValidateCommand(order);
            if (valid) {
                //GM.SendCommand
            }
        }

    }

}
