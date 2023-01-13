using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Item : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] string itemId;
    [SerializeField] string itemDes;

    [Header("Stats")]
    [SerializeField] float movementMultiplier;

    [Header("UI")]
    int currentStack = 1;
    [SerializeField] TextMeshProUGUI StackUI;

    public float GetMovementMultiplier()
    {
        return movementMultiplier;
    }

    public virtual void ItemActivation()
    {

    }

    internal void UpdateStackUI(int val)
    {
        currentStack += val;
        StackUI.text = currentStack.ToString() + "x";
    }
}
