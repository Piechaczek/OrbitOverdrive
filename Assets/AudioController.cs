using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource bgMusic;

    public void PlayBackground() {
        bgMusic.Play();
    }

    public void OnMusicToggle(bool musicOff) {
        if (musicOff) {
            bgMusic.volume = 0f;
        } else {
            bgMusic.volume = 1f;
        }
    }

}
