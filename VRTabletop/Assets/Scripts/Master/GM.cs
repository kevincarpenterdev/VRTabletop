using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;
using VRTabletop.Communications;

using VRTabletop.Clients;

using VRTabletop.Pawns.Validation;

namespace VRTabletop {
    //Due for a refactor?
    public class GM : MonoBehaviour {

        [SerializeField] public bool localGame { get; private set; }

        //"Global" Variables
        Dictionary<int , BasePawn> Pawns_In_Play;
        Dictionary<int , Player> Players;
        Dictionary<int , Human> Humans; //insert in a seperate controller that controls server?

        //Game Vars
        [SerializeField] public VRState VR { get; private set; }

        public void setVRState(VRState V) {
            VR = V;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (localGame) {
                //Wait for player
            } else {
                //listen for server
                //If got broadcast, move pawns/update states
            }
        }

        public void passTurn() {
            
        }

        //Grab the response from the server and apply it, though for now we are short cutting it....
        public void ApplyResponse(Response R) {
            BasePawn TargetPawn = Pawns_In_Play[R.AppliedID];
            if (TargetPawn != null) {
                TargetPawn.ExecuteCommand(R , R.CMD);
                if(TargetPawn == null) {
                    //Remove it
                }
            } else {
                throw new ArgumentNullException();
            }

        }

    }
}

