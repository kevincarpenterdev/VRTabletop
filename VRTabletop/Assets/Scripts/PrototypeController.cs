using UnityEngine;
using System.Collections;
using VRTabletop.Clients;
using VRTabletop.Communications;
using VRTabletop.Pawns;

public class PrototypeController : MonoBehaviour {

    public BasePawn ControlledPawn;
    public BasePawn TargetPawn;
   

    enum VRState {
        Disconnected,
        Overview,
        InPawn
    }

    enum Mode {
        None,
        MoveMode,
        ShootMode
    }

    [SerializeField] VRState VR;
    [SerializeField] Mode m;

	// Use this for initialization
	void Start () {
        m = Mode.None;
	}

    // Update is called once per frame
    void Update() {
        InputCheck();
        ModeCheck();
        //ExecutionCheck();
    }
    
    void InputCheck() {
        if(Input.GetKeyDown(KeyCode.F1)) {
            m = Mode.MoveMode;
            ControlledPawn.SetCommand(CommandType.Movement);
        } else if (Input.GetKeyDown(KeyCode.F2)) {
            m = Mode.ShootMode;
            ControlledPawn.SetCommand(CommandType.Shooting);
        }
    }
    void ModeCheck() {
        if (m != Mode.None) {
            ControlledPawn.RunValidation();
            if (!Input.GetKeyDown(KeyCode.Space) || !Input.GetKeyDown(KeyCode.F1) || !Input.GetKeyDown(KeyCode.F2)) {
                ControlPawn();
            }
        }
    }
    void ControlPawn() {
        if(m == Mode.ShootMode) {
            if(VR == VRState.Overview) {
                //Set VR Camera Active
                //Set Player Cam inactive
                VR = VRState.InPawn;
            } else {
                if (Input.GetKeyDown(KeyCode.Q)) {
                    //Rotate Left
                }
                if (Input.GetKeyDown(KeyCode.E)) {
                    //Rotate Right
                }
            }
        } else if (m== Mode.MoveMode) {
            if(Input.GetKeyDown(KeyCode.W)) {
                //Move Collider Up
            }
            if(Input.GetKeyDown(KeyCode.A)) {
                    //Move Collider left
            }
            if(Input.GetKeyDown(KeyCode.S)) {
                //Move Collider down
            }
            if(Input.GetKeyDown(KeyCode.D)) {
                //Move collider right
            }
        }
    }

    void ExecutionCheck() {
        if(m != Mode.None && Input.GetKeyDown(KeyCode.Space)) {
            Order O = ControlledPawn.SendOrder();
            if(O != null) {
                //Serialize/Deserialize Server simulation
                string OrderString = OrderFormatter.serialize(O);
                Response R = OrderFormatter.deserialize(OrderString);
                ExecuteOrder(R);
            }
        }
    }
    void ExecuteOrder(Response R) {
        if(R.AppliedID == 0) {
            ControlledPawn.ExecuteCommand(R);
        } else if (R.AppliedID == 1) {
            TargetPawn.ExecuteCommand(R);
        }
    }

    void CamState() {
        //Camera Swapping/VR State Swapping
        //See: http://docs.unity3d.com/ScriptReference/Camera.html 
    }
}
