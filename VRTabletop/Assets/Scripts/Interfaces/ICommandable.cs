using UnityEngine;
using System.Collections;
using VRTabletop.Communications;

namespace VRTabletop {
    public interface ICommandable {

        void RunValidation();

        Order SendOrder(); //Client side validation

        void ExecuteCommand(Response R); //Updates the state and informs the server
    }
}


