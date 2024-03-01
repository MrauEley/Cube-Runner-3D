using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] float _minScale, _maxScale, _scaleDirection=1, _scaleSpeed;
    [SerializeField] float _leftPosition = -200, _rightPosition = 200, _movementDirection, _movementSpeed;

    void Update()
    {
        if (transform.localPosition.x < _rightPosition && _movementDirection == 1)
        {
            transform.localPosition += new Vector3(_movementSpeed, 0, 0) * Time.deltaTime * _movementDirection;
            if (transform.localPosition.x >= _rightPosition)
            {
                transform.localPosition = new Vector3(_rightPosition-1, transform.localPosition.y, 0);
                _movementDirection *= -1;
            }
        }
        else
        {
            transform.localPosition += new Vector3(_movementSpeed, 0, 0) * Time.deltaTime * _movementDirection;
            if (transform.localPosition.x <= _leftPosition)
            {
                transform.localPosition = new Vector3(_leftPosition + 1, transform.localPosition.y, 0);
                _movementDirection *= -1;
            }
        }

        if (transform.localScale.x < _maxScale && _scaleDirection == 1)
        {
            transform.localScale += new Vector3(_scaleSpeed, _scaleSpeed, _scaleSpeed) * Time.deltaTime * _scaleDirection;
            if(transform.localScale.x >= _maxScale) { _scaleDirection *= -1; }
        }
        else
        {
            transform.localScale += new Vector3(_scaleSpeed, _scaleSpeed, _scaleSpeed) * Time.deltaTime * _scaleDirection;
            if (transform.localScale.x <= _minScale) { _scaleDirection *= -1; }
        }
    }
}
