using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PoolObjectController : MonoBehaviour
{
    
    
    [SerializeField] private List<Pools> _poolObjects = new List<Pools>();
    
    void Start()
    {
        foreach (var currentPool in _poolObjects)
        {
            SpawnObject(currentPool);
        }
    }

    private void SpawnObject(Pools currentPool)
    {
        for (var i = 0; i < currentPool.SizePool; i++)
        {
            var currentObject = Instantiate(currentPool.Prefab, currentPool.ContainerObjects);
            currentObject.SetActive(false);
        }
    }
}

[Serializable]
public class Pools
{
    public int SizePool;
    public GameObject Prefab;
    public Transform ContainerObjects;
}