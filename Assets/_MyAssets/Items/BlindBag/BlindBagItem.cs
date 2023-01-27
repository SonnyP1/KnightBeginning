using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static UnityEditor.Progress;

public class BlindBagItem : Item
{
    [Header("Stats")]
    [SerializeField] int maxItems = 5;
    [SerializeField] AllItems _allItemList;


    private PostProcessVolume _postProcessingVolume;
    private Vignette _vignette;

    //ITEM BUG : Stacking just break somehow
    public override void ItemActivation()
    {
        base.ItemActivation();
        GameObject randomItem = ChooseRandomItem();

        for(int i = 0; i < maxItems; i++)
        {
            GameObject spawnedItem = Instantiate(randomItem, gameObject.transform);
            spawnedItem.GetComponent<Item>().ItemSelected();
        }

        _postProcessingVolume = FindObjectOfType<PostProcessVolume>();

        IncreaseBlindness(0.2f);
    }

    private GameObject ChooseRandomItem()
    {
        GameObject[] allItems = _allItemList.GetAllItems().ToArray();
        GameObject randItem = allItems[Random.Range(0, allItems.Length)];
        string chosenItemID = randItem.GetComponent<Item>().GetItemID();

        if(chosenItemID == GetItemID())
        {
            return ChooseRandomItem();
        }

        return randItem;
    }
    void IncreaseBlindness(float val)
    {
        _postProcessingVolume.profile.TryGetSettings(out _vignette);
        _vignette.intensity.value += val;
    }
}
