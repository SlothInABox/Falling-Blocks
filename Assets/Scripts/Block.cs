using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float yBound;

    // Update is called once per frame
    void Update()
    {
        // Move block downwards
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Block cleanup
        if (transform.position.y < yBound)
        {
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
            gameObject.SetActive(false); // Return block to pool
        }
    }
}
