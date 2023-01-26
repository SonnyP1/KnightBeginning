using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsOfSpeedItem : Item
{
    [Header("Stats")]
    [SerializeField] float SpeedMultiplier;
    public override void ItemActivation()
    {
        base.ItemActivation();
        GetPlayerObj().GetComponent<MovementComponent>().ApplyMovementStat(SpeedMultiplier);
    }
}
