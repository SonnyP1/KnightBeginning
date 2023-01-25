using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsOfFlightItem : Item
{
    [Header("Stats")]
    [SerializeField] int _jumpCountAddition;
    public override void ItemActivation()
    {
        base.ItemActivation();
        GetPlayerObj().GetComponent<MovementComponent>().AddToJumpCount(_jumpCountAddition);
    }
}
