using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaItem : Item
{
    [Header("Stats")]
    [SerializeField] float AttackMultiplierAddition;

    public override void ItemActivation()
    {
        GetPlayerObj().GetComponent<AttackComponent>().AddAttackMultiplier(AttackMultiplierAddition);
    }
}
