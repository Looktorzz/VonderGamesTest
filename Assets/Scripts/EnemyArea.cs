using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConfig.PlayerTag))
        {
            CoreGame.Instance.TimeHopController.OnTimeChanged();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.SetUp();
        }
    }
}