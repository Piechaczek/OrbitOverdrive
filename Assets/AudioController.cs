using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController INSTANCE;

    public AudioSource bgMusic;
    public Transform enemyController;

    public bool musicOff;
    public bool soundsOff;

    public void Start() {
        INSTANCE = this;
    }

    public void PlayBackground() {
        bgMusic.Play();
    }

    public void OnMusicToggle(bool musicOff) {
        this.musicOff = musicOff;
        if (musicOff) {
            bgMusic.volume = 0f;
        } else {
            bgMusic.volume = 0.2f;
        }
    }

    public void OnSoundToggle(bool soundsOff) {
        this.soundsOff = soundsOff;
        AudioSource[] audioSources = enemyController.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource audioSource in audioSources){
            if (soundsOff) {
                audioSource.volume = 0f;
            } else {
                audioSource.volume = 1f;
            }
        }
    }

}
