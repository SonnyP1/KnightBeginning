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
    [SerializeField] TextMeshProUGUI StackUI;
    [SerializeField] TextMeshProUGUI NameTxt;
    [SerializeField] TextMeshProUGUI ItemDesTxt;

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

    internal void UpdateStackUI(int val)
    {
        currentStack += val;
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

            ItemActivation();
            FindObjectOfType<GameMangerSystem>().ContinueGame();
            if(isStackable)
            {
                FindObjectOfType<ItemSystem>().AddItem(gameObject);
                inInventory = true;
            }



            Destroy(parentObject);
        }
    }
}
