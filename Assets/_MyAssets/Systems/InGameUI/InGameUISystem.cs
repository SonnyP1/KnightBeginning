using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUISystem : MonoBehaviour
{
    [Header("HealthUI")]
    [SerializeField] GameObject heartUIObj;
    [SerializeField] GameObject healthGridUI;
    List<GameObject> heartList = new List<GameObject>();

    [Header("ItemsUI")]
    [SerializeField] GameObject _panel;
    List<GameObject> items = new List<GameObject>();
    
    [Header("ScoreUI")]
    [SerializeField] TextMeshProUGUI _scoreText;


    public void UpdateScoreText(float val)
    {
        _scoreText.text = val.ToString();
    }

    public void UpdateHealthUI(int desiredlength)
    {
        Debug.Log(desiredlength);

        int currentLength = heartList.Count;
        Debug.Log(currentLength);

        int difference = desiredlength - currentLength;
        Debug.Log(difference);

        if(difference > 0)
        {
            for(int i = 0; i < difference; i++)
            {
                GameObject newHealth = Instantiate(heartUIObj,healthGridUI.transform);
                heartList.Add(newHealth);
            }
        }
        else
        {
            difference = Mathf.Abs(difference);
            for(int i = 0; i < difference; i++)
            {
                GameObject objToRemove = heartList[0];
                heartList.Remove(objToRemove);
                Destroy(objToRemove);
            }
        }
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
