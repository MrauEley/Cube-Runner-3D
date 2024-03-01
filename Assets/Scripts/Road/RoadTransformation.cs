using System.Collections.Generic;
using UnityEngine;

public class RoadTransformation : MonoBehaviour
{
    [Header("Road transfom")]
    private float _roadBasicY = 0f;
    [SerializeField][Range(80, 150)] private float _roadSpeedAfterSpawn = 100;
    private Vector3 _roadVerticalMovement;
    private bool _canMove = true;

    //[Header("Prefabs")]
    //[SerializeField] private GameObject _wallPrefab;
    //[SerializeField] private GameObject _pickupPrefab;

    /*
    [Header("Pickups")]
    private List<GameObject> _pickupsOnRoad = new List<GameObject>();
    [SerializeField][Range(1, 10)] private int _cubesPerRoadMin = 3, _cubesPerRoadMax = 4, _cubesPerRoad;
    private int _pickupSpawnOffsetZ = 8;
    private int _pickupSpawnRange = 16;
    private float _minPickupX = 0f, _maxPickupX = 4f;
    */


    private void Start()
    {
        _roadVerticalMovement = new Vector3(0, _roadSpeedAfterSpawn, 0);
            
        //Vector3 wallOffset = new Vector3(-transform.position.x, 0, transform.localScale.z);
        //GameObject wall = Instantiate(_wallPrefab, transform.position + wallOffset, transform.rotation);
        //wall.transform.SetParent(transform);
    }

    /*
    private void OnEnable()
    {
        SpawnPickups();
    }//Randomise amount of pickups
    */

    void FixedUpdate()
    {
        if (_canMove)
        {
            MovementAfterSpawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
                RoadGenerator.instance.RemoveRoad();
                RoadGenerator.instance.AddNextRoad();

            //Destroy(gameObject);
        }
    } //killbox

    private void MovementAfterSpawn()
    {
        if (transform.position.y < _roadBasicY)
        {
            transform.position += _roadVerticalMovement * Time.deltaTime;
        }
        else if (transform.position.y > _roadBasicY)
        {
            Vector3 targetPos = transform.position;
            targetPos.y = _roadBasicY;
            transform.position = targetPos;
            _canMove = false;
        }
    }

    /*
    private void SpawnPickups()
    {
        if(_pickupsOnRoad.Count < _cubesPerRoad)
        {
            _cubesPerRoad = Random.Range(_cubesPerRoadMin, _cubesPerRoadMax + 1);
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
    */

    /*
    private void OnDisable()
    {
        while(_pickupsOnRoad.Count > 0)
        {
            PoolManager.instance.DespawnObject(_pickupsOnRoad[0]);
            _pickupsOnRoad.RemoveAt(0);
        }
    }
    */

}
