using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedStarItem : Item
{
    [Header("Stat")]
    [SerializeField] float time;
    public override void ItemActivation()
    {
        GetPlayerObj().GetComponent<HealthComp>().canTakeDmg = true;
        GetPlayerObj().GetComponent<HealthComp>().Hit(2,false);
        StartCoroutine(PowerUp());
        StartCoroutine(PowerUpVisualEffect());
    }

    IEnumerator PowerUp()
    {
        GetPlayerObj().GetComponent<HealthComp>().canTakeDmg = false;
        yield return new WaitForSeconds(time);
        AddItemStack(-1);
        if(GetCurrentStack() == 0)
        {
            EndPowerUp();
        }
        else 
        {
            StartCoroutine(PowerUp());
        }
    }

    IEnumerator PowerUpVisualEffect()
    {
        GetPlayerObj().GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetPlayerObj().GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetPlayerObj().GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(PowerUpVisualEffect());
    }


    void EndPowerUp()
    {
        StopAllCoroutines();
        FindObjectOfType<ItemSystem>().RemoveItem(this.gameObject);
        GetPlayerObj().GetComponent<SpriteRenderer>().color = Color.white;
        GetPlayerObj().GetComponent<HealthComp>().canTakeDmg = true;
        GetPlayerObj().GetComponent<HealthComp>().Hit(0,true);
        Destroy(gameObject);
    }
}
