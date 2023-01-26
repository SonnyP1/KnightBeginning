using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeItem : Item
{
    [Header("Prefabs")]
    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spawnXOffset;

    [Header("Stats")]
    [SerializeField] float percentageChance;

    public override void ItemActivation()
    {
        base.ItemActivation();
        GetPlayerObj().GetComponent<AttackComponent>().onAttack += SpawnKnife;
    }

    private void OnDestroy()
    {
        if(gameObject != null && GetPlayerObj() != null)
        {
            GetPlayerObj().GetComponent<AttackComponent>().onAttack -= SpawnKnife;
        }
    }

    private void SpawnKnife()
    {
        if(UnityEngine.Random.value < percentageChance * GetCurrentStack())
        {
            GameObject newKnifePrefab = Instantiate(knifePrefab,GetPlayerObj().transform,false);
            newKnifePrefab.transform.Translate(Vector3.right * spawnXOffset);
            newKnifePrefab.transform.SetParent(null);
        }
    }
}
