using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Range(0, 3)]
    [SerializeField] private float minBlockSize;
    [Range(0, 3)]
    [SerializeField] private float maxBlockSize;

    [SerializeField] private float timeBetweenSpawns;
    private float nextSpawnTime;

    private Vector2 screenHalfSizeWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        nextSpawnTime = Time.time + timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBlock();
            nextSpawnTime = Time.time + timeBetweenSpawns;
        }
    }

    private void SpawnBlock()
    {
        GameObject newBlock = BlockPooler.Instance.GetPooledObject();
        if (newBlock != null)
        {
            // Random block size
            newBlock.transform.localScale = Vector3.one * Random.Range(minBlockSize, maxBlockSize);
            
            // Random block spawn position
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x + (newBlock.transform.localScale.x / 2), screenHalfSizeWorldUnits.x - (newBlock.transform.localScale.x / 2)), screenHalfSizeWorldUnits.y + (newBlock.transform.localScale.y / 2));
            newBlock.transform.position = spawnPosition;
            
            // Random cube rotation keeping cube in the bounds of the screen on journey
            float maxAngle = Mathf.Atan2(screenHalfSizeWorldUnits.x - newBlock.transform.localScale.x / 2 - newBlock.transform.position.x, 2 * (screenHalfSizeWorldUnits.y + newBlock.transform.localScale.y / 2));
            float minAngle = -Mathf.Atan2(newBlock.transform.position.x + (screenHalfSizeWorldUnits.x - newBlock.transform.localScale.x / 2), 2 * (screenHalfSizeWorldUnits.y + newBlock.transform.localScale.y / 2));
            float rotAngle = Random.Range(minAngle, maxAngle) * 180 / Mathf.PI;
            newBlock.transform.Rotate(0, 0, rotAngle);

            newBlock.SetActive(true);
        }
    }
}
