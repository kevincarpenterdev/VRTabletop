using UnityEngine;
using System.Collections;
using VRTabletop.Clients;
using VRTabletop.Communications;
using VRTabletop.Pawns;

public class PrototypeController : MonoBehaviour {

    public BasePawn ControlledPawn;
    public BasePawn TargetPawn;

    enum Mode {
        None,
        MoveMode,
        ShootMode
    }

    [SerializeField] Mode m;

	// Use this for initialization
	void Start () {
        m = Mode.None;
	}

    // Update is called once per frame
    void Update() {
        KeyCheck();
        ModeCheck();

        ExecutionCheck();
    }
    
    void KeyCheck() {
        if(Input.GetKeyDown(KeyCode.F1)) {
            m = Mode.MoveMode;
            ControlledPawn.SetCommand(CommandType.Movement);
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            m = Mode.ShootMode;
            ControlledPawn.SetCommand(CommandType.Shooting);
        }
    }
    void ModeCheck() {
        if (m != Mode.None) {
            ControlledPawn.RunValidation();
        }
    }
    void ExecutionCheck() {
        if(m != Mode.None && Input.GetKeyDown(KeyCode.Space)) {
            string OrderString = "";
            Response R = OrderFormatter.deserialize(OrderString);
            ExecuteOrder(R);
        }
    }
    void ExecuteOrder(Response R) {

    }
}
