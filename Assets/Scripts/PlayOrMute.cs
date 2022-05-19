using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOrMute : MonoBehaviour
{
    private bool playing = true;

    public void userToggle(bool tog) {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        if (playing) {
            audios[0].Pause();
        }
        else {
            audios[0].Play();
        }
        playing = !playing;
    }
}
