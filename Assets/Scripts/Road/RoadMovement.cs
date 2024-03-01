using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private float _roadSpeed = 10;
    private Vector3 _roadMovement = new Vector3(0, 0, -1);
    private bool _canMove = false;

    void FixedUpdate()
    {
        if (_canMove)
        {
            transform.position += _roadMovement * _roadSpeed * Time.deltaTime;
        }
    }

    public void SetCanMove(bool state)
    {
        _canMove = state;
    }
}
