using UnityEngine;
using System.Collections;


    public enum CommandType {
        Movement,
        TargetAbility,
        NonTargetAbility
    }
    public enum VRState {
         Disconnected,
         Overview,
         InPawn
    }

    public enum Mode {
        Select,
        MoveMode,
        ShootMode
    }