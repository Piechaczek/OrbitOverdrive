using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource bgMusic;
    public Transform enemyController;

    public void PlayBackground() {
        bgMusic.Play();
    }

    public void OnMusicToggle(bool musicOff) {
        if (musicOff) {
            bgMusic.volume = 0f;
        } else {
            bgMusic.volume = 0.2f;
        }
    }

    public void OnSoundToggle(bool soundsOff) {
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
