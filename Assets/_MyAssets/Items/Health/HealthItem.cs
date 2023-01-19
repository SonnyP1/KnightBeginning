using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    [Header("Stats")]
    [SerializeField] int healthToHeal;

    public override void ItemActivation()
    {
        GameObject playerObj = FindObjectOfType<PlayerController>().gameObject;
        playerObj.GetComponent<HealthComp>().Heal(healthToHeal);
        Destroy(gameObject);
    }
}
