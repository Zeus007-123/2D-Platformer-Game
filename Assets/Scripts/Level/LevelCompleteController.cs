using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleteController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float TimeDelay = 2f;

    public Button buttonHome;
    public GameObject LevelCompleteScreen;

    public GameOverController gameOverController;

     private void Awake()
    {
        buttonHome.onClick.AddListener(GoHome);
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Debug.Log(" Level Finished by the Player ");
            LevelManager.Instance.MarkCurrentLevelComplete();
            Invoke(nameof(Load_Scene), TimeDelay);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Deathpit"))
        {
            Debug.Log(" Player Died ");
            animator.SetTrigger("Death");
            Invoke(nameof(Restart_Scene), TimeDelay);
        }
        
    }

    private void Load_Scene()
    {
        Debug.Log(" Level Completed ");
        LevelCompleteScreen.SetActive(true);
    }

    public void Restart_Scene()
    {
        Debug.Log(" Reloading Current Active Scene ");
        gameOverController.PlayerDied();
        GetComponent<PlayerController>().enabled = false;
    }

    private void GoHome()
    {
        Debug.Log(" Going to Home Lobby Screen/Scene ");
        SceneManager.LoadScene(0);
    }
}
