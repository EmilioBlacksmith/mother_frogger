using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _waterObjectEmpty;
    private static GameObject _carObjectEmpty;
    private static GameObject _particleObjectEmpty;

    private Transform _startingTransform;

    public enum PoolType
    {
        WaterObject,
        CarObject,
        ParticleObject,
        None
    }

    public static PoolType PoolingType;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _waterObjectEmpty = new GameObject("Water Objects");
        _waterObjectEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _carObjectEmpty = new GameObject("Car Objects");
        _carObjectEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _particleObjectEmpty = new GameObject("Particle Objects");
        _particleObjectEmpty.transform.SetParent (_objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        // If the pool doesn't exist, create it
        if(pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Check if there are any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if(spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.WaterObject:
                return _waterObjectEmpty;
            case PoolType.CarObject:
                return _carObjectEmpty;
            case PoolType.ParticleObject: 
                return _particleObjectEmpty;
            case PoolType.None:
                return null;
            default: 
                return null;
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject> ();
}
