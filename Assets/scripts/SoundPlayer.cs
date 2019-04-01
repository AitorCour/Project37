using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    //public AudioMixerGroup output;
    public AudioSource source;
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

        Destroy(obj.gameObject, clips[numClip].length); 
    }
    public void Stop()
    {
        source.Stop();
    }
}
