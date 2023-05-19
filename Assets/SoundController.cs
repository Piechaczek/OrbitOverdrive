using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public static SoundController INSTANCE;

    public GameObject hitClip;

    void Start()
    {
        INSTANCE = this;
    }

    public void PlayHitClip(float volume){
        if (!AudioController.INSTANCE.soundsOff){
            AudioSource audioSource = Instantiate(hitClip, transform).GetComponent<AudioSource>();
            audioSource.volume = volume;
        }
    }


}
