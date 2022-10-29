using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource effects;
    private void Start()
    {
        effects = FindObjectOfType<AudioSource>();
        GameManager.OnPhotoStartMaking += () =>
        {
            if(effects.isPlaying)
            {
                effects.Stop();
            }

            effects.Play();
        };
    }
}
