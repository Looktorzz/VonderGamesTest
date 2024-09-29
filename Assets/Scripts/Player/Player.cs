using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float _speed = 5;

    // TODO: Move to core game or game config
    private const string _groundTag = "Ground";

    private InteractableObject _interactableObject;
    private Vector2 _directionMove;
    private bool _isJump;

    private void OnEnable()
    {
        _playerController.SetUp();
        _playerController.WhenDirectionMoveChanged += OnSetDirection;
        _playerController.WhenJumped += OnJump;
        _playerController.WhenInteracted += OnInteract;
    }

    private void OnDisable()
    {
        _playerController.UnsubscribeInput();
        _playerController.WhenDirectionMoveChanged -= OnSetDirection;
        _playerController.WhenInteracted -= OnInteract;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out _interactableObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactableObject = null;
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

    private void OnInteract()
    {
        if (_interactableObject != null)
        {
            _interactableObject.Interact();
        }
    }
}
