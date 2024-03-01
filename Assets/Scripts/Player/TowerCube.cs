using UnityEngine;

public class TowerCube : MonoBehaviour
{
    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") && _isActive)
        {
            _isActive = false;
            Transform parent = transform.parent;
            parent.SetParent(other.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && _isActive)
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnDisable()
    {   
        Transform parent = transform.parent;
        Destroy(parent.gameObject);

        Vector3 basicPosition = new Vector3(0, 0, 0);
        parent.position = basicPosition;
        transform.position = basicPosition;

        _isActive = true;
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

}
