using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] clips;

    public void Play(int numClip, float vol)
    {
        GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        obj.name = "AUDIO_" + clips[numClip].name;

        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clips[numClip];
        source.volume = vol;
        source.spatialBlend = 1; // 1 --> 3D  0--> 2D
        source.Play();

        Destroy(obj.gameObject, clips[numClip].length); 
    }

}
