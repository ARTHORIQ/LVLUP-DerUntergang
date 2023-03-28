using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // The prefab of the object to pool.
    public int initialSize = 10; // The initial number of objects to create in the pool.

    private Queue<GameObject> objectPool = new Queue<GameObject>(); // The queue that holds the objects in the pool.

    void Start()
    {
        // Create the initial pool of objects.
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count == 0)
        {
            // If the pool is empty, create a new object and add it to the pool.
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        // Dequeue an object from the pool and return it.
        GameObject pooledObject = objectPool.Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReturnObject(GameObject obj)
    {
        // Set the object to inactive and add it back to the pool.
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}