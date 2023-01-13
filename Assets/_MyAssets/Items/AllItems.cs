using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AllItemsList", order = 1)]
public class AllItems : ScriptableObject
{
    [SerializeField] List<GameObject> allItems = new List<GameObject>();
    public List<GameObject> GetAllItems()
    { return allItems; }    
}
