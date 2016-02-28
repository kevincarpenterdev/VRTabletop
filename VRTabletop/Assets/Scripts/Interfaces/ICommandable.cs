using UnityEngine;
using System.Collections;


namespace VRTabletop.Interfaces {
    public interface ICommandable {
        bool ValidateCommand(); //Client side validation

        void ExecuteCommand(); //Updates the state and informs the server
    }
}


