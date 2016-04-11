using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VRTabletop.Pawns;
using VRTabletop.Communications;

using VRTabletop.Clients;

using VRTabletop.Pawns.Validation;
using VRTabletop.Utils;

namespace VRTabletop {
    //Due for a refactor?
    public class GM : MonoBehaviour {

        //"Global" Variables
        [SerializeField] protected List<BasePawn> Pawns_In_Play;
        [SerializeField] protected List<Player> Players;
        protected int PlayerTurn;
        protected ResponseFactory RF;

        [SerializeField] protected Camera MainCam;
        [SerializeField] protected Camera FPSCam;

        //Game Vars
        [SerializeField] public VRState VR { get; protected set; }

        public void SetCamMode(bool FPSMode) {
            if(FPSMode == true ) {
                MainCam.gameObject.SetActive(false);
                FPSCam.gameObject.SetActive(true);
            } else {
                MainCam.gameObject.SetActive(true);
                FPSCam.gameObject.SetActive(false);
            }
        }

        public void SetFPSCam(Camera C) {
            FPSCam = C;
        }

        public void setVRState(VRState V) {
            VR = V;
        }

        // Use this for initialization
        void Start() {
            //Temp
            foreach(Player p in Players) {
                p.setGM(this);
            }
            setVRState(VRState.Disconnected);
            PlayerTurn = 0;
            RF = new ResponseFactory();
        }


        // Update is called once per frame
        void Update() {
            Players[PlayerTurn].InputCheck();
        }

        public void passTurn() {
            throw new NotImplementedException();
        }

        public void AcquireOrder(Order O) {
            //Grab the order Send it to the Rulebook and let 'er rip!
            //Here is our very temporary version
            ApplyResponse(RF.GenerateNonValidResponse(O));
        }

        //Grab the response from the Rulebook and apply it, though for now we are short cutting it....
        public void ApplyResponse(Response R) {
            BasePawn TargetPawn = Pawns_In_Play[R.AppliedID];
            if (TargetPawn != null) {
                TargetPawn.ExecuteCommand(R);
                if(TargetPawn == null) {
                    //Remove it from the list
                }
            } else {
                throw new ArgumentNullException();
            }

        }

    }
}

