using UnityEngine;
using System.Collections;
using VRTabletop.Clients;
using VRTabletop.Communications;
using VRTabletop.Pawns;
using VRTabletop.Pawns.Validation;
using VRTabletop.Utils;

public class PrototypeController : MonoBehaviour {

    public BasePawn ControlledPawn;
    public BasePawn TargetPawn;

    public Camera OverviewCam;
    public GameObject Sight;

    public BasePawnValidator PV;

    public CommandType Curr;

    [SerializeField] float speed;

    ResponseFactory RF;

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
        Sight.SetActive(false);
        PV.BPValidatorSetup(ControlledPawn);
        RF = new ResponseFactory();
	}

    // Update is called once per frame
    void Update() {
        InputCheck();
        ModeCheck();
        ExecutionCheck();
    }
    
    void InputCheck() {

        if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.F2)) {
            PV.StopValidation();
        }

        if (Input.GetKeyDown(KeyCode.F1)) {
            m = Mode.MoveMode;
            SetCommand(CommandType.Movement);
            OverviewCam.gameObject.SetActive(true);
            //Sight.SetActive(false);
            ControlledPawn.PawnCam.gameObject.SetActive(false);
        } else if (Input.GetKeyDown(KeyCode.F2)) {
            m = Mode.ShootMode;
            SetCommand(CommandType.TargetAbility);
            OverviewCam.gameObject.SetActive(false);
            //Sight.SetActive(true);
            ControlledPawn.PawnCam.gameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.F3)) {
            m = Mode.None;
            OverviewCam.gameObject.SetActive(true);
            //Sight.SetActive(false);
            ControlledPawn.PawnCam.gameObject.SetActive(false);
        }
    }
    void ModeCheck() {
        if (m != Mode.None) {
            PV.RunValidation(Curr);
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
                if(Input.GetKey(KeyCode.Q)) {
                    ControlledPawn.LookAtPawn(TargetPawn);
                }
            }
        } else if (m== Mode.MoveMode) {
            float x = 0f;
            float z = 0f;
            if(Input.GetKey(KeyCode.W)) {
                x -= speed;
            }
            if(Input.GetKey(KeyCode.A)) {
                z -= speed;
            }
            if(Input.GetKey(KeyCode.S)) {
                x += speed;
            }
            if(Input.GetKey(KeyCode.D)) {
                z += speed;
            }
            PV.RunValidation(x , z, Curr);
        }
    }

    void ExecutionCheck() {
        if(m != Mode.None && Input.GetKeyDown(KeyCode.Space)) {
            Order O = PV.SendOrder(Curr);
            if(O != null) {
                //Serialize/Deserialize Server simulation
                /*string OrderString = OrderFormatter.serialize(O);
                Response R = OrderFormatter.deserialize(OrderString); */
                Response R = RF.GenerateResponse(O , Curr);
                ExecuteOrder(R);
            }
        }
    }
    void ExecuteOrder(Response R) {
        if(R.AppliedID == 0) {
            ControlledPawn.ExecuteCommand(R, Curr);
        } else if (R.AppliedID == 1) {
            TargetPawn.ExecuteCommand(R, Curr);
        }
    }
    void SetCommand(CommandType C) {
        Curr = C;
    }
}
