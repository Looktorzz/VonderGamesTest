using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float _speed = 5;

    private const string _groundTag = "Ground";

    private Vector2 _directionMove;
    private bool _isJump;

    private void OnEnable()
    {
        _playerController.SetUp();
        _playerController.WhenDirectionMoveChanged += OnSetDirection;
        _playerController.WhenJumped += OnJump;
    }

    private void OnDisable()
    {
        _playerController.UnsubscribeInput();
        _playerController.WhenDirectionMoveChanged -= OnSetDirection;
        _playerController.WhenJumped -= OnJump;
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_directionMove.x * _speed, _rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isJump = false;
        }
    }

    private void OnSetDirection(Vector2 directionMove)
    {
        this._directionMove = directionMove;
    }

    private void OnJump()
    {
        if (!_isJump)
        {
            _rigidbody2D.AddForce(Vector2.up * _speed, ForceMode2D.Impulse);
            _isJump = true;
        }
    }
}
