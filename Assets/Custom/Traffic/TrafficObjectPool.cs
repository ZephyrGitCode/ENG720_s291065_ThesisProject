﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficObjectPool : MonoBehaviour
{

    [SerializeField] GameObject trafficPrefab;
    [SerializeField] [Range(1, 30)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 20f)] float spawnTimer = 1f;

    GameObject[] pool;

    void Awake(){
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnTraffic());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(trafficPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnTraffic()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableObjectInPool()
    {
        
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

}
