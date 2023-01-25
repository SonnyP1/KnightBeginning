using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultyShieldItem : Item
{
    [Header("Stats")]
    [SerializeField] float percentageChance;
    public override void ItemActivation()
    {
        base.ItemActivation();
        GetPlayerObj().GetComponent<HealthComp>().onDmgTaken += ChanceToNegateDmg;
    }

    private void ChanceToNegateDmg(int val)
    {
        float value = GetCurrentStack() * percentageChance;
        if (UnityEngine.Random.value < value)
        {
            GetPlayerObj().GetComponent<HealthComp>().SetDmgMultiplier(0.0f);
        }
    }

    private void OnDestroy()
    {
        if(this.gameObject != null && GetPlayerObj() != null)
        {
            GetPlayerObj().GetComponent<HealthComp>().onDmgTaken -= ChanceToNegateDmg;
        }
    }
}
