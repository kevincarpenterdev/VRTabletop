using UnityEngine;
using System.Collections;

namespace VRTabletop.Clients {
    public class Player : Human {
        public static GM GameMaster; //Global GM

        void SendOrder() {
            bool valid = true;
            //Bool Valid = ICommandable.ValidateCommand(order);
            if (valid) {
                //GM.SendCommand
            }
        }

    }

}
