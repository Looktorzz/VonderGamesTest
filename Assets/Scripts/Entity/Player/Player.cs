using UnityEngine;

public class Player : BaseEntity
{
    [SerializeField]
    private PlayerController _playerController;
    
    // TODO: Collect item object instead wand
    private Wand _wand;
    private Vector2 _directionMove;
    private bool _isJump;
    private bool _isLeftDirection;

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

    // TODO: Use other way instead destroy and Instantiate everytime.
    public void SetCurrentHoldItem(ItemData item)
    {
        // Reset current hold item (wand).
        if (_wand != null)
        {
            Destroy(_wand.gameObject);
        }

        if (item == null || item.ItemPrefab == null)
        {
            return;
        }

        if (item.ItemPrefab.TryGetComponent(out Wand wand))
        {
            _wand = Instantiate(wand, transform);
        }
    }

    protected override void Dead()
    {
        Debug.Log($"{this.name} dead!");

        transform.position = CoreGame.Instance.GameConfig.PlayerSpawnPosition;
        CoreGame.Instance.TimeHopController.OnTimeChanged();
        SetUp();
    }

    private void OnSetDirection(Vector2 directionMove)
    {
        this._directionMove = directionMove;

        if (directionMove.x == 0)
        {
            return;
        }

        _isLeftDirection = directionMove.x < 0;
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
        if (_wand != null)
        {
            _wand.OnInteract(_isLeftDirection);
        }
    }
}
