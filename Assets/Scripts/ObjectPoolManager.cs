using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();

    public static GameObject SpawnObject(GameObject objToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Find pool based on the object name
        PoolObjectInfo pool = ObjectPools.Find(p => p.LookupString == objToSpawn.name);

        // If pool does not exist, create a new pool for this object
        if (pool == null)
        {
            pool = new PoolObjectInfo { LookupString = objToSpawn.name };
            ObjectPools.Add(pool);
            pool.InactiveObjcets = new List<GameObject>();
        }

        // Try to get an inactive object from the pool
        GameObject spawnableObj = pool.InactiveObjcets.FirstOrDefault();

        if (spawnableObj == null)
        {
            // Instantiate a new object if no inactive object is available
            spawnableObj = Instantiate(objToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            // Use the inactive object from the pool
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            spawnableObj.SetActive(true);
            pool.InactiveObjcets.Remove(spawnableObj);
        }

        return spawnableObj;
    }

    public static void ReturnObjectPool(GameObject obj)
    {
        string goName = obj.name.Replace("(Clone)", "");
        PoolObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjcets.Add(obj);
        }
    }
}

public class PoolObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjcets = new List<GameObject>();
}