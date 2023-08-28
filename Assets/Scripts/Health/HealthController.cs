using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Animator animator;
    private bool dead;
    [SerializeField]private float startingHealth;
    [SerializeField] private float TimeDelay = 2f;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private Image totalhealthBar;
    public float currentHealth {get; private set; } 
    public GameOverController gameOverController;
    public HealthController playerHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        Invoke(nameof(Cur_Health),TimeDelay);
        //currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
        
        if (currentHealth > 0)
        {
            Debug.Log(" Player Hurt By Enemy ");
            animator.SetTrigger("Hurt");
        }
        else
        {
            if(!dead)
            {
                Debug.Log(" Player Killed By Enemy ");
                animator.SetTrigger("Death");
                GetComponent<PlayerController>().enabled = false;
                dead = true;
                Invoke(nameof(Load_Scene) , TimeDelay);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Cur_Health()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Load_Scene()
    {
        Debug.Log(" Reloading Current Active Scene ");
        gameOverController.PlayerDied();
        GetComponent<PlayerController>().enabled = false;
    }


}
