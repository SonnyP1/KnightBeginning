using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireItem : Item
{
    [Header("Stats")]
    [SerializeField] float PercentChance;
    public override void ItemActivation()
    {
        GetPlayerObj().GetComponent<AttackComponent>().onKillCountChange += ChanceToHeal;
    }


    private void OnDestroy()
    {
        GetPlayerObj().GetComponent<AttackComponent>().onKillCountChange -= ChanceToHeal;
    }
    private void ChanceToHeal()
    {
        float value = GetCurrentStack() * PercentChance;
        Debug.Log(value);
        if (UnityEngine.Random.value < value)
        {
            GetPlayerObj().GetComponent<HealthComp>().Heal(1);
        }
    }
}
