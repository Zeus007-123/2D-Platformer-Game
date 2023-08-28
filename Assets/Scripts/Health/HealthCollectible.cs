using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        HealthController healthController = playerController.GetComponent<HealthController>();

        if (playerController != null && healthController != null)
        {
            healthController.AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
