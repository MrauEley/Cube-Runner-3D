using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private float _playerSlideSpeed;
    [SerializeField][Range(0, 0.2f)] private float _playersMovementTrashold; //if distance is < than trashold, player will not move to prevent jittering

    private float _minPlayerX = 0f, _maxPlayerX = 4f;
    [SerializeField][Range(3, 6)] private int _screenToWorldPrecision = 5; //amount of signs after dot
    private double _conversionScreenToWorld;
    private float _roadWidth = 4;

    private bool _canMove = true;
    private Vector3 _startPosition;
    private Vector3 _playersMovement;

    void Start()
    {
        SetPlayerStartPosition();
        int screenWidth = Screen.width;
        _playersMovement = new Vector3(_playerSlideSpeed, 0, 0);

        _conversionScreenToWorld = _roadWidth  / screenWidth;
        _conversionScreenToWorld = Math.Round(_conversionScreenToWorld, _screenToWorldPrecision);
    }

    void FixedUpdate()   
    {
        if (Input.touchCount > 0 && _canMove)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            CheckPlayersPosition(touchPosition);
        }
    }

    public void SetPlayerStartPosition()
    {
        _startPosition = transform.position;
        _startPosition.x = (_minPlayerX + _maxPlayerX) / 2;
        transform.position = _startPosition;
    }

    private void CheckPlayersPosition(Vector2 touchPosition)
    {
        float convertedTouchPosition = (float)(touchPosition.x * _conversionScreenToWorld);
        float playersPosition = transform.position.x;

        if (MathF.Abs(convertedTouchPosition - playersPosition) < _playersMovementTrashold)
        {//if distance is < than trashold, player will not move to prevent jittering
            return;
        }

        Vector3 targetPosition = new Vector3(convertedTouchPosition, 0, 0);
        if (convertedTouchPosition < playersPosition && playersPosition > _minPlayerX)
        {//MoveToLeft
            MovePlayer(-1, targetPosition); 
        }
        else if (convertedTouchPosition > playersPosition && playersPosition < _maxPlayerX)
        {//MoveToRight
            MovePlayer(1, targetPosition); 
        }
    }

    private void MovePlayer(int direction, Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _playerSlideSpeed * Time.deltaTime);
    } 

    public void SetCanMove(bool state)
    {
        _canMove = state;
    }

}
