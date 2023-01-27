using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] AllItems items;
    [SerializeField] GameObject[] itemHolders;

    Animator _chestAnimator;
    GameMangerSystem _gameManagerSystem;
    private GameObject item1;
    private GameObject item2;
    private GameObject item3;
    private void Start()
    {
        _chestAnimator = GetComponent<Animator>();
        _gameManagerSystem = FindObjectOfType<GameMangerSystem>();

        GameObject[] allItems = items.GetAllItems().ToArray();
        item1 = allItems[Random.Range(0, allItems.Length)];
        item2 = allItems[Random.Range(0, allItems.Length)];
        item3 = allItems[Random.Range(0, allItems.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _chestAnimator.SetTrigger("OpenChest");
            GetComponent<SimpleMove>().StopMovement();
            _gameManagerSystem.StopGame();
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    public void StartShowingItems()
    {
        StartCoroutine(ShowThreeItems());
    }
    IEnumerator ShowThreeItems()
    {
        itemHolders[0].SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject spawnedItem1 = Instantiate(item1,itemHolders[0].transform);
        spawnedItem1.GetComponent<Item>().SetParentObj(gameObject);

        itemHolders[1].SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject spawnedItem2 = Instantiate(item2, itemHolders[1].transform);
        spawnedItem2.GetComponent<Item>().SetParentObj(gameObject);

        itemHolders[2].SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject spawnedItem3 = Instantiate(item3, itemHolders[2].transform);
        spawnedItem3.GetComponent<Item>().SetParentObj(gameObject);
    }
}
