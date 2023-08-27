using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button buttonRestart;
    public Button buttonHome;

    //public ParticleController particleController;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(ReloadLevel);
        buttonHome.onClick.AddListener(GoHome);
    }
    public void PlayerDied()
    {
        SoundManager.Instance.Play(Sounds.PlayerDeath);
        SoundManager.Instance.Play(Sounds.DeathMusic);
        gameObject.SetActive(true);
        //particleController.PlayOnLevelFail();
    }

    private void ReloadLevel()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log(" Reloading Active Scene ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoHome()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log(" Going to Home Lobby Screen/Scene ");
        SceneManager.LoadScene(0);
    }
}
 