using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoodAndEvilItem : Item
{
    [Header("Stats")]
    [SerializeField] float alternatatingTime;
    [SerializeField] float multiplierStat;
    bool inGoodStat = false;
    public override void ItemActivation()
    {
        base.ItemActivation();
        Debug.Log("Start Alternating");
        StartCoroutine(GoodStatBoost());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    IEnumerator GoodStatBoost()
    {
        inGoodStat = true;
        GetPlayerObj().GetComponent<MovementComponent>().ApplyMovementStat(multiplierStat * GetCurrentStack());

        yield return new WaitForSeconds(alternatatingTime);

        GetPlayerObj().GetComponent<MovementComponent>().ApplyMovementStat(-multiplierStat * GetCurrentStack());
        inGoodStat = false;

        StartCoroutine(BadStatBoost());
    }

    IEnumerator BadStatBoost()
    {
        GetPlayerObj().GetComponent<MovementComponent>().ApplyMovementStat(-multiplierStat * GetCurrentStack());

        yield return new WaitForSeconds(alternatatingTime);

        GetPlayerObj().GetComponent<MovementComponent>().ApplyMovementStat(multiplierStat * GetCurrentStack());

        StartCoroutine(GoodStatBoost());
    }

}
