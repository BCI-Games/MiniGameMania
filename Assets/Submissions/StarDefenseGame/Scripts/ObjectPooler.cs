using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static Dictionary<string, ObjectPooler> SharedInstances = new Dictionary<string, ObjectPooler>();
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    

    void Awake()
    {
        SharedInstances.Add(objectToPool.name, this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager

        }

    }

    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    public static ObjectPooler GetInstance(string str)
    {
        return SharedInstances[str];
    }
    public static GameObject GetInstanceObject(string str)
    {
        return SharedInstances[str].GetPooledObject();
    }
}