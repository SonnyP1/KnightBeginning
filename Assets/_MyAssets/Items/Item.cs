using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class Item : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] string itemId;
    [SerializeField] string itemDes;
    [SerializeField] bool isStackable;


    [Header("UI")]
    int currentStack = 1;
    public int GetCurrentStack() { return currentStack; }

    [SerializeField] TextMeshProUGUI StackUI;
    [SerializeField] TextMeshProUGUI NameTxt;
    [SerializeField] TextMeshProUGUI ItemDesTxt;

    public bool GetIsInventory() { return inInventory; }
    private bool inInventory = false;
    public GameObject GetPlayerObj() { return _player; }
    private GameObject _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;


        NameTxt.enabled = false;
        ItemDesTxt.enabled = false;

        NameTxt.text = itemId;
        ItemDesTxt.text = itemDes;
    }

    public virtual void ItemActivation()
    {

    }

    internal void AddItemStack(int val)
    {
        currentStack += val;
        UpdateStackUI();
    }
    internal void UpdateStackUI()
    {
        StackUI.text = currentStack.ToString() + "x";
    }

    public void ShowNameAndDescription()
    {
        NameTxt.enabled = true;
        ItemDesTxt.enabled = true;
    }

    public void HideNameAndSescription()
    {
        NameTxt.enabled = false;
        ItemDesTxt.enabled = false;
    }

    public void ItemSelected()
    {
        if(!inInventory)
        {
            //Gets the ChestGameObject
            GameObject parentObject = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.gameObject.transform.parent.gameObject;
            transform.parent = null;
            //check if already in inventory then activate
            if(isStackable)
            {
                FindObjectOfType<ItemSystem>().AddItem(gameObject);
                inInventory = true;
            }

            ItemActivation();
            FindObjectOfType<GameMangerSystem>().ContinueGame();



            Destroy(parentObject);
        }
    }
}
