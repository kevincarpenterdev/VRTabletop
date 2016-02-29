using UnityEngine;
using System.Collections;

/// <summary>
/// Humans are essentially "Client instances" controlled by humans (E.G Players, Judges, Spectators, Admins)
/// </summary>

namespace VRTabletop.Clients {
    public abstract class Human : MonoBehaviour {
        public string Handle { private set; get; }


    }
}

