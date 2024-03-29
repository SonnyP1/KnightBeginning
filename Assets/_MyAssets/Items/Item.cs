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

    public string GetItemID() { return itemId; }
    public bool GetIsInventory() { return inInventory; }
    private bool inInventory = false;

    public void SetParentObj(GameObject newParentObj) { parentObj = newParentObj; }
    private GameObject parentObj;
    public GameObject GetPlayerObj() 
    {
        if(_player == null)
        {
            _player = FindObjectOfType<PlayerController>().gameObject;
        }
        return _player; 
    }
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
        if(_player != null) 
        {
            _player = FindObjectOfType<PlayerController>().gameObject;
        }
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
            transform.SetParent(null);
            //check if already in inventory then activate
            if(isStackable)
            {
                FindObjectOfType<ItemSystem>().AddItem(gameObject);
                inInventory = true;
            }
            else
            {
                ItemActivation();
            }

            if(parentObj != null)
            {
                FindObjectOfType<GameMangerSystem>().ContinueGame();
                Destroy(parentObj);
            }
        }
    }
}
