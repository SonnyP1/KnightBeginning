using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField] GameObject itemToTest;
    private InGameUISystem _gameUISystem;
    private MovementComponent _playerMovementComp;
    List<GameObject> itemsEquip = new List<GameObject>();

    private void Start()
    {
        _gameUISystem = FindObjectOfType<InGameUISystem>();
        _playerMovementComp = FindObjectOfType<MovementComponent>();
        StartCoroutine(AddItemToTest());
    }
    public void AddItem(GameObject newItem)
    {

        bool isDuplicatedItem = false;
        foreach (GameObject itemEquip in itemsEquip)
        {
            if(itemEquip.GetComponent<Item>().name == newItem.GetComponent<Item>().name)
            {
                isDuplicatedItem = true;
            }
        }
        _gameUISystem.UpdateItemList(newItem, isDuplicatedItem);
        ApplyStats(newItem.GetComponent<Item>());

        if(!isDuplicatedItem)
        {
            itemsEquip.Add(newItem);
        }
        else
        {
            Destroy(newItem);
        }

    }

    public void ApplyStats(Item itemToApply)
    {
        _playerMovementComp.ApplyMovementStat(itemToApply.GetMovementMultiplier());
    }

    IEnumerator AddItemToTest()
    {
        yield return new WaitForSecondsRealtime(5f);
        if(itemToTest != null)
        {
            GameObject newItem = Instantiate(itemToTest);
            AddItem(newItem);
            Debug.Log("Added Test Item");
        }
        StartCoroutine(AddItemToTest());
    }
}
