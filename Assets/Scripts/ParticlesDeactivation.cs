using UnityEngine;

public class ParticlesDeactivation : MonoBehaviour
{
    private void OnDisable()
    {
        PoolManager.instance.DespawnObject(gameObject);
    }
}
