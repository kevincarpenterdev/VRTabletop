using UnityEngine;
using System.Collections;
using VRTabletop.Communications;
using VRTabletop.Pawns.Validation;

namespace VRTabletop {
    public interface ICommandable {

        void ExecuteCommand(Response R); //Updates the state of the commanded thing

        }
 }


