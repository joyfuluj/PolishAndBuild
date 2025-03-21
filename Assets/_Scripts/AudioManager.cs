using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    
    public AudioSource sfxSource;
    public AudioSource AmbienceSource;

    [Header("Audio Clips")]

    public AudioClip ambientClip;
    public AudioClip hitClip;
    public AudioClip killClip;
    public AudioClip brickClip;
    public AudioClip gameStartClip; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        PlayAmbientSound();
    }
    
    public void PlayAmbientSound()
    {
        if(ambientClip != null && AmbienceSource != null)
        {
            AmbienceSource.clip = ambientClip;
            AmbienceSource.loop = true;
            AmbienceSource.Play();
        }
    }

    public void PlaySound (AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
