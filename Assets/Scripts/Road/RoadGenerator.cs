using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("RoadPrefabs")]
    [SerializeField] private GameObject _defaultRoad;
    [SerializeField] private List<GameObject> _roadPrefabs = new List<GameObject>();
    private List<GameObject> _activeRoad = new List<GameObject>();
    [SerializeField] private Transform _roadParent;
    [SerializeField] [Range(3,10)] private int _roadsMaxCount = 6;

    [Header("RoadPosition")]
    [SerializeField] private float _roadBasicY = 0;
    [SerializeField] private float _roadBasicX = 2f;
    [SerializeField] private float _newRoadOffsetY = -10;
    [SerializeField] private float _firstRoadOffsetZ = -10;
    [SerializeField] private float _roadLenght = 30;

    public static RoadGenerator instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RestartRoad();
    }

    public void RestartRoad()
    {
        while (_activeRoad.Count > 0)
        {
            PoolManager.instance.DespawnObject(_activeRoad[0].gameObject);
            _activeRoad.RemoveAt(0);
        }

        InstantiateRoad(_defaultRoad, new Vector3(_roadBasicX, 0, _firstRoadOffsetZ));

        while (_activeRoad.Count < _roadsMaxCount)
        {
            CreateRoad(_roadBasicX, _roadBasicY, _roadLenght);
        }
    }

    private void CreateRoad(float spawnOffsetX, float spawnOffsetY, float spawnOffsetZ)
    {
        int newRoadIndex = Random.Range(0, _roadPrefabs.Count);
        Vector3 roadSpawnPosition = _activeRoad[0].transform.position + new Vector3(0, 0, spawnOffsetZ);
        roadSpawnPosition.y = spawnOffsetY;

        InstantiateRoad(_roadPrefabs[newRoadIndex], roadSpawnPosition);
    }

    private void InstantiateRoad(GameObject roadPrefab, Vector3 spawnPositon)
    {
        GameObject newRoad = PoolManager.instance.SpawnObject(roadPrefab, spawnPositon, _roadParent.transform.rotation);
        newRoad.transform.SetParent(_roadParent);
        _activeRoad.Insert(0, newRoad);
    }

    public void AddNextRoad()
    {
        CreateRoad(_roadBasicX, _newRoadOffsetY, _roadLenght);
    }

    public void RemoveRoad()
    {
        PoolManager.instance.DespawnObject(_activeRoad[_activeRoad.Count - 1].gameObject);
        _activeRoad.RemoveAt(_activeRoad.Count - 1);
    }
}
