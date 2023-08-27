using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particleSys;

    public void PlayOnLevelFail()
    {
        gameObject.SetActive(true);
    }
}
