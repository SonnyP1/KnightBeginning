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
        base.ItemActivation();
        GetPlayerObj().GetComponent<AttackComponent>().onKillCountChange += ChanceToHeal;
    }
    private void OnDestroy()
    {
        if(gameObject != null)
        {
            GameObject playerObj = GetPlayerObj();
            if(playerObj != null || playerObj.GetComponent<AttackComponent>() != null)
            {
                return;
            }
            playerObj.GetComponent<AttackComponent>().onKillCountChange -= ChanceToHeal;
        }
    }
    private void ChanceToHeal()
    {
        float value = GetCurrentStack() * PercentChance;
        if (UnityEngine.Random.value < value)
        {
            GetPlayerObj().GetComponent<HealthComp>().Heal(1);
        }
    }
}
