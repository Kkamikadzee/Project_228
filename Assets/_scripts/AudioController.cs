using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    public Initialization initialization;

    public AudioSource musicSource;
    public AudioSource soundSource;

    public Scrollbar musicScrollbar;
    public Scrollbar soundScrollbar;

    public void LoadScrollbars()
    {
        musicScrollbar.value = musicSource.volume;
        soundScrollbar.value = soundSource.volume;
    }

    private void Start()
    {
        musicSource.Play();
    }

    public void SaveAudioSettings()
    {
        initialization.SaveSettings(soundSource.volume, musicSource.volume);
    }

    public void PlaySound()
    {
        soundSource.Play();
    }
}
