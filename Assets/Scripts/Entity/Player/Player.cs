using UnityEngine;

public class Player : BaseEntity
{
    [SerializeField]
    private PlayerController _playerController;

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
        Rigidbody2D.velocity = new Vector2(_directionMove.x * Speed, Rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConfig.GroundTag))
        {
            _isJump = false;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out _interactableObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactableObject = null;
    }*/

    private void OnSetDirection(Vector2 directionMove)
    {
        this._directionMove = directionMove;
    }

    private void OnJump()
    {
        if (!_isJump)
        {
            Rigidbody2D.AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
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
