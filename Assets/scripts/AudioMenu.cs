using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public AudioClip[] clips;
    //public AudioMixerGroup output;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void Play(int numClip/*, float vol*/)
    {
        GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        //obj.name = "AUDIO_" + clips[numClip].name;

        //AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clips[numClip];
        source.volume = 1;
        //vol
        source.spatialBlend = 1; // 1 --> 3D  0--> 2D
        source.Play();
        //Debug.Log(obj);
        Destroy(obj.gameObject, clips[numClip].length);
    }
}

