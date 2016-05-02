using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VRTabletop.Pawns;


public class PawnUIController : MonoBehaviour {

    [SerializeField]
    protected Text Name;

    [SerializeField]
    protected Text Stats;

    [SerializeField]
    protected Text Orders;

    protected PawnModel model;

	// Use this for initialization
	void Start () {
        model = GetComponentInParent<PawnModel>();
        Name.text = model.PawnName;
        UpdateStats();
        UpdateOrders();
	}
	

    public void UpdateStats()
    {
        Stats.text = "HP: " + model.HP + "\n" +
            "BS: " + model.BallisticSkill + "\n" +
            "CC: " + model.CloseCombat + "\n" +
            "PH: " + model.Physique + "\n" +
            "WP: " +model.Willpower +"\n" +
            "MVMT: " +model.MoveRange + "\n";
    }

    public void UpdateOrders()
    {
        Orders.text = "Orders: " + model.OrderAmt;
    }
}
