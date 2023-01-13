using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUISystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] GameObject _panel;
    List<GameObject> items = new List<GameObject>();


    public void UpdateScoreText(float val)
    {
        _scoreText.text = val.ToString();
    }

    public void UpdateItemList(GameObject newItem, bool isDuplicated)
    {
        if(isDuplicated)
        {
            foreach(GameObject item in items)
            {
                if(item.GetComponent<Item>().name == newItem.GetComponent<Item>().name)
                {
                    item.GetComponent<Item>().UpdateStackUI(1);
                }
            }
        }
        else
        {
            newItem.transform.parent = _panel.transform;
            items.Add(newItem);
        }
    }
}
