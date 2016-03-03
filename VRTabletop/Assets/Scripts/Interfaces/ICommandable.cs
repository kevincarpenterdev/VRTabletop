using UnityEngine;
using System.Collections;


namespace VRTabletop {
    public interface ICommandable {

        bool ValidateCommand(); //Client side validation

        void ExecuteCommand(); //Updates the state and informs the server
    }
}


