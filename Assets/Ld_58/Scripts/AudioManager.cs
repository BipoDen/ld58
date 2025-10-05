using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip background;
    public AudioClip click;
    public AudioClip typing;
    public AudioClip Hit;
    public AudioClip Notification;
    private void Awake()
    {
        G.audioManager = this;
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void Mute()
    {
        musicSource.volume = 0;
    }
}
