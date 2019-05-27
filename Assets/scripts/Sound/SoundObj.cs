using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    public AudioClip[] clips;
    //public AudioMixerGroup output;
    //private AudioSource source;
    public AudioMixerGroup fxGroup;
    public void Play(GameObject go,int numClip/*, float vol*/)
    {
        /*GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        //obj.name = "AUDIO_" + clips[numClip].name;

        //AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clips[numClip];
        source.volume = 1;
        //vol
        source.spatialBlend = 1; // 1 --> 3D  0--> 2D
        source.Play();
        //Debug.Log(obj);
        Destroy(obj.gameObject, clips[numClip].length);*/

        AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = fxGroup;

        audioSource.clip = clips[numClip];
        audioSource.volume = 1;
        audioSource.pitch = 1;
        //audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.reverbZoneMix = 1;

        audioSource.Play();

        Destroy(audioSource, clips[numClip].length);
    }
}
