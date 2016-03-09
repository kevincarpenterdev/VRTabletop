using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;
using VRTabletop.Clients;

namespace VRTabletop {
    //Due for a refactor?
    public class GM : MonoBehaviour {

        //"Global" Variables
        Dictionary<int , BasePawn> Pawns_In_Play;
        Dictionary<int , Player> Players;

        Dictionary<int , Human> Humans; //insert in a seperate controller that controls server?

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            //listen for server
            //If got broadcast, move pawns/update states
        }

        //Grab the response from the server and apply it
        void ApplyResponse() {

        }

        void SendCommand() {
            //Server Send ze command!
        }
    }
}

