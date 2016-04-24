using UnityEngine;
using System.Collections;
using System;


public class CameraController : MonoBehaviour {

    [SerializeField]
    protected Transform OverviewPoint;

    [SerializeField]
    protected Transform FPSPoint;

    void Start() {
        GoToOverview();
    }


    public void SetFPSPosition(Transform T) {
        FPSPoint = T;
    }

    public void GoToOverview() {
        transform.position = OverviewPoint.position;
        transform.rotation = OverviewPoint.rotation;
        transform.parent = OverviewPoint;
    }

    public void GoToFPS() {
        transform.position = FPSPoint.position;
        transform.rotation = FPSPoint.rotation;
        transform.parent = FPSPoint;
    }

}
