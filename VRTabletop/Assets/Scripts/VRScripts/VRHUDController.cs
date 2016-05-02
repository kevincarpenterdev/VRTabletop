﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VRHUDController : MonoBehaviour {

    [SerializeField]
    protected Text Message;

    //WeaponInfo
    [SerializeField]
    protected GameObject FPSZone;
    [SerializeField]
    protected Text WeaponName;
    [SerializeField]
    protected Text WeaponDamage;
    [SerializeField]
    protected Text WeaponBurst;
    [SerializeField]
    protected Text WeaponRange;

    [SerializeField]
    protected GameObject Results;
    [SerializeField]
    protected Text ResultText;

    protected bool firstResult;

	// Use this for initialization
	void Start () {
        firstResult = true;
        Results.SetActive(false);
        Message.text = "";
        DeactivateWeaponHUD();
	}

    public void PopulateWeapon(WeaponModel WM){
        FPSZone.SetActive(true);
        WeaponName.text = WM.WeaponName;
        WeaponDamage.text = "DMG: " + WM.damage;
        WeaponBurst.text = "Burst: " + WM.burst;
        WeaponRange.text = "Range: " + WM.WeaponRange + " Meters";
    }

    public void DeactivateWeaponHUD(){
        FPSZone.SetActive(false);
    }

    /* We'll need to await some stuff from brett on this one...
    public void Populate(whatever the result will be passed in as) {
         if(firstResult) {
            Results.SetActive(true);
            firstResult=false;
         }
    }
    */

}
