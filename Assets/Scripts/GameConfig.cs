using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [SerializeField]
    private Color _openInventoryColor;
    public Color OpenInventoryColor => _openInventoryColor;

    [SerializeField]
    private Color _closeInventoryColor;
    public Color CloseInventoryColor => _closeInventoryColor;

    [SerializeField]
    private Color _normalSlotColor;
    public Color NormalSlotColor => _normalSlotColor;

    [SerializeField]
    private Color _selectedSlotColor;
    public Color SelectedSlotColor => _selectedSlotColor;

    [Header("Spawn Player")]
    [SerializeField]
    private Vector3 _playerSpawnPosition;
    public Vector3 PlayerSpawnPosition => _playerSpawnPosition;

    [Header("Spawn Enemy")]
    [SerializeField]
    private GameObject _enemy;
    public GameObject Enemy => _enemy;

    [SerializeField]
    private Vector3 _enemySpawnPosition;
    public Vector3 EnemySpawnPosition => _enemySpawnPosition;

    public const string GroundTag = "Ground";
    public const string PlayerTag = "Player";
}
