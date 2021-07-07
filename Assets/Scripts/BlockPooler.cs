using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPooler : MonoBehaviour
{
    public static BlockPooler Instance { get; private set; }
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            PoolBlock();
        }
    }

    private void PoolBlock()
    {
        GameObject tmp = Instantiate(objectToPool);
        tmp.SetActive(false);
        tmp.transform.SetParent(gameObject.transform);
        pooledObjects.Add(tmp);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // If the pool is too small, increase the pool
        PoolBlock();
        amountToPool = pooledObjects.Count;
        return pooledObjects[amountToPool - 1];
    }
}
