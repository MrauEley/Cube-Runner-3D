using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    [SerializeField] private GameObject _pickupPrefab;

    [Header("Pickups")]
    private List<GameObject> _pickupsOnRoad = new List<GameObject>();
    [SerializeField][Range(1, 10)] private int _cubesPerRoadMin = 3, _cubesPerRoadMax = 4, _cubesPerRoad = 1;
    private int _pickupSpawnOffsetZ = 11;
    private int _pickupSpawnRange = 16;
    private float _minPickupX = 0f, _maxPickupX = 4f;


    private void OnEnable()
    {
        SpawnObjects();
    }//Randomise amount of pickups


    private void SpawnObjects()
    {
        _cubesPerRoad = Random.Range(_cubesPerRoadMin, _cubesPerRoadMax + 1);
        if (_pickupsOnRoad.Count < _cubesPerRoad)
        {
            float _distanceBetweenPickups = _pickupSpawnRange / (_cubesPerRoad - 1);
            for (int i = 0; i < _cubesPerRoad; i++)
            {
                float randomizePositionX = Random.Range(_minPickupX, _maxPickupX);
                float randomizePositionZ = Random.Range(-1, 1);

                float positionX = -transform.position.x + randomizePositionX;
                float positionZ = _pickupSpawnOffsetZ + _distanceBetweenPickups * i + randomizePositionZ;
                Vector3 pickupOffset = new Vector3(positionX, 0, positionZ);

                GameObject newPickup = PoolManager.instance.SpawnObject(_pickupPrefab, transform.position + pickupOffset, transform.rotation);
                newPickup.transform.SetParent(transform);
                _pickupsOnRoad.Add(newPickup);
            }
        }
    }

    private void OnDisable()
    {
        while (_pickupsOnRoad.Count > 0)
        {
            PoolManager.instance.DespawnObject(_pickupsOnRoad[0]);
            _pickupsOnRoad.RemoveAt(0);
        }
    }

}
