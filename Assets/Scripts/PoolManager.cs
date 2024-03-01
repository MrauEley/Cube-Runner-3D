using System.Collections.Generic;
using UnityEngine;

public class PoolManager: MonoBehaviour
{
    public static PoolManager instance;
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = new PoolManager();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    class Pool
    {
        private List<GameObject> _inactiveObjects = new List<GameObject>();
        private GameObject _prefab;

        public Pool(GameObject prefab) { this._prefab = prefab; }

        public GameObject SpawnObject(Vector3 position, Quaternion rotation)
        {
            GameObject obj;
            if(_inactiveObjects.Count == 0)
            {
                obj = Instantiate(_prefab, position, rotation);
                obj.name = _prefab.name;
            }
            else
            {
                obj = _inactiveObjects[_inactiveObjects.Count - 1];
                _inactiveObjects.RemoveAt(_inactiveObjects.Count - 1);
            }
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        public void DespawnObject(GameObject obj)
        {
            obj.SetActive(false);
            _inactiveObjects.Add(obj);
        }
    }

    public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        InitializeNewPool(prefab);
        return _pools[prefab.name].SpawnObject(position, rotation);
    }

    private void InitializeNewPool(GameObject prefab)
    {
        if (prefab != null && _pools.ContainsKey(prefab.name) == false)
        {
            _pools[prefab.name] = new Pool(prefab);
        }
    }

    public void DespawnObject(GameObject obj)
    {
        if (_pools.ContainsKey(obj.name))
        {
            _pools[obj.name].DespawnObject(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}
