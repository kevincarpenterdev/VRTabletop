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

        [SerializeField] protected CameraController MainCam;

        //Game Vars
        [SerializeField] protected VRState VR;

        [SerializeField] protected VRHUDController VRHC;

        public void SendMessageToHUD(string message){
            VRHC.SetMessaage(message);
        }
        public void ClearMessagesInHUD(){
            VRHC.HideMessage();
        }

        public void SetCamMode(WeaponModel WM) {
                VRHC.PopulateWeapon(WM);
                MainCam.GoToFPS();                
        }
        public void SetCamMode() {
            MainCam.GoToOverview();
            VRHC.DeactivateWeaponHUD();
        }


        public void SetFPSCam(Transform T) {
            MainCam.SetFPSPosition(T);
        }

        public VRState getVRState() {
            return VR;
        }

        public void setVRState(VRState V) {
            if(VR != VRState.Disconnected) {
                VR = V;
            }
        }

        // Use this for initialization
        void Start() {
            //Temp
            foreach (Player p in Players) {
                p.setGM(this);
            }
            int i = 0;
            //AutoSetup Pawns
            foreach(BasePawn P in Pawns_In_Play){
                P.ID = i;
                i++; 
            }
            PlayerTurn = 0;
            RF = new ResponseFactory();
            VRHC = MainCam.GetComponentInChildren<VRHUDController>();
        }


        // Update is called once per frame
        void Update() {
            Players[PlayerTurn].InputCheck();
        }

        public void passTurn() {
            Players[PlayerTurn].ForceOutOfFPS();
            if (PlayerTurn + 1 == Players.Count) {
                PlayerTurn = 0;
            } else {
                PlayerTurn++;
            }
            Players[PlayerTurn].StartTurn();
            SendMessageToHUD("Player " + PlayerTurn + "'s Turn");
        }

        public void AcquireOrder(Order O) {
            //Grab the order Send it to the Rulebook and let 'er rip!
            //Here is our very temporary version
            ApplyResponse(RF.GenerateNonValidResponse(O));
        }

        public void AcquireOrder(Order O, BasePawn attacker, int target)
        {
            DamageResponse R = (DamageResponse)RF.GenerateNonValidResponse(O, attacker, Pawns_In_Play[target].GetPawnModel());

            if(R.HPChange > R.DefenderHPChange)
            {
                R.SetAppliedID(attacker.ID);
                R.SetHPChange(R.DefenderHPChange);
            }

            ApplyResponse(R);
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
            VRHC.PopulateResults(R.Result);
        }

    }
}

