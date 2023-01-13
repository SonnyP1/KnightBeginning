using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] AllItems items;
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

        Debug.Log(item1.GetComponent<Item>().name);
        Debug.Log(item2.GetComponent<Item>().name);
        Debug.Log(item3.GetComponent<Item>().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER BY " + other.gameObject.layer);
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _chestAnimator.SetTrigger("OpenChest");
            _gameManagerSystem.StopGame();
        }
    }
}
