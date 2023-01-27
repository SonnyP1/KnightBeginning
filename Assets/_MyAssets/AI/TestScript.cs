using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [Header("Sample Text")]
    [SerializeField] string _sampleTxt;
    void Start()
    {
        Debug.Log(_sampleTxt);
    }
}
