using System.Collections.Generic;
using UnityEngine;

public class CubeHarvester : MonoBehaviour
{
    [SerializeField] private GameObject _heightRegulator;
    [SerializeField] private GameObject _towerCubePrefab;
    [SerializeField] private int _basicPoolSize = 20;

    [SerializeField] private GameObject _particlesBurst;
    [SerializeField] private GameObject _particlesNumbers;

    private float _heightOffset = 1.05f;
    [SerializeField] GameObject _character;
    public static CubeHarvester instance;

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
        CollectCube();
    }

    private void CollectCube()
    {
        _heightRegulator.transform.position += new Vector3(0, _heightOffset, 0);

        GameObject newTowerBlock = Instantiate(_towerCubePrefab, transform.position, transform.rotation);
        newTowerBlock.transform.SetParent(_heightRegulator.transform);
    }

    private void CreateParticles(GameObject prefab)
    {
        GameObject particleSystem = PoolManager.instance.SpawnObject(prefab, transform.position, transform.rotation);
        particleSystem.transform.SetParent(_character.transform);
        particleSystem.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.transform.parent.gameObject.SetActive(false);

            CollectCube();
            CreateParticles(_particlesBurst);
            CreateParticles(_particlesNumbers);
        }
    }


    public void RestartGame()
    {
        int childrenCount = CountImmediateChildren(_heightRegulator.transform);

        if (childrenCount == 1)
        {
            CollectCube();
        }
        else
        {
            for (int i = 1; i < childrenCount - 1; i++)
            {
                GameObject cubeToDespawn = _heightRegulator.transform.GetChild(i).gameObject;
                if (cubeToDespawn != null)
                {
                    Destroy(_heightRegulator.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    private int CountImmediateChildren(Transform transform)
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            count++;
        }
        return count;
    }
}
