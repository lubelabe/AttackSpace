using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoinsController : MonoBehaviour
{
    private float _randomTime = 10;
    
    private void Start()
    {
        InvokeRepeating("SpawnCoinsInRandomTime", 20, _randomTime);
    }

    private void SpawnCoinsInRandomTime()
    {
        _randomTime = Random.Range(15, 70);
        if (transform.childCount <= 0)
        {
            return;
        }
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.transform.GetChild(i).gameObject.activeSelf)
            {
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(true);
            break;
        }
    }
}
