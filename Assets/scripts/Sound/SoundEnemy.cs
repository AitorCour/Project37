using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip[] clipCarpets;
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

        //Destroy(obj.gameObject, clips[numClip].length);
    }


    public void PlayF()
    {
        int numClip = 0;

        GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        //obj.name = "AUDIO_" + clips[numClip].name;

        numClip = Random.Range(0, clipCarpets.Length);
        source.clip = clipCarpets[numClip];

        //AudioSource source = obj.AddComponent<AudioSource>();
        source.volume = 1;
        //vol
        source.spatialBlend = 1; // 1 --> 3D  0--> 2D
        source.Play();

        Destroy(obj.gameObject);
    }

    public void Stop()
    {
        source.Stop();
    }
}
