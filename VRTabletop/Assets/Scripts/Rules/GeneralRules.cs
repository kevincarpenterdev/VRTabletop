using UnityEngine;
using System.Collections;
using VRTabletop.Communications;
using VRTabletop.Pawns;
public class GeneralRules {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public AttackOrder GenerateAttackOrder(Order O, PawnModel attacker, PawnModel defender)
    {
        AttackOrder AO = (AttackOrder)O;
        AO.attackNum = attacker.Weapon.burst;
        AO.attackPower = attacker.Weapon.damage;
        AO.attackSkill = attacker.BallisticSkill;
        AO.defendSkill = defender.BallisticSkill;
        AO.defendPower = defender.Weapon.damage;
        return AO;
    }
}
