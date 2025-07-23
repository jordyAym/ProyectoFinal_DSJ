using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonido_boton : MonoBehaviour
{
    private AudioSource music;
    public AudioClip ClickAudio;

    void Start()
    {
        music = GetComponent<AudioSource>();
    }
    public void ClickAudioOn()
    {
        music.PlayOneShot(ClickAudio);
    }
}
