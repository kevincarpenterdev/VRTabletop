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

        //"Global" Variables
        [SerializeField] protected List<BasePawn> Pawns_In_Play;
        [SerializeField] protected List<Player> Players;
        protected int PlayerTurn;

        //Game Vars
        [SerializeField] public VRState VR { get; protected set; }

        public void setVRState(VRState V) {
            VR = V;
        }

        // Use this for initialization
        void Start() {
            //Temp
            setVRState(VRState.Disconnected);
            PlayerTurn = 0;
        }


        // Update is called once per frame
        void Update() {
            Players[PlayerTurn].InputCheck();
        }

        public void passTurn() {

        }

        public void AcquireOrder(Order O) {
            //Grab the order Send it to the Rulebook and let 'er rip!
        }

        //Grab the response from the Rulebook and apply it, though for now we are short cutting it....
        public void ApplyResponse(Response R) {
            BasePawn TargetPawn = Pawns_In_Play[R.AppliedID];
            if (TargetPawn != null) {
                TargetPawn.ExecuteCommand(R , R.CMD);
                if(TargetPawn == null) {
                    //Remove it from the list
                }
            } else {
                throw new ArgumentNullException();
            }

        }

    }
}

