using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    //public AudioMixerGroup output;
    public AudioSource source;

    public AudioClip[] clipCarpets;
    public AudioClip[] clipWoods;
    public AudioClip[] clipDirts;
    public enum Footsteps
    {
        Carpet,
        Dirt,
        Wood
    }
    Footsteps footSteps = new Footsteps();

    void Start()
    {
        footSteps = Footsteps.Dirt;
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

        Destroy(obj.gameObject, clips[numClip].length); 
    }

    
    private void PlayF()
    {
		int numClip = Random.Range(0, clipCarpets.Length);

        GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        //obj.name = "AUDIO_" + clips[numClip].name;

        switch(footSteps){
            case Footsteps.Carpet:
                source.clip = clipCarpets[numClip];
                break;
            case Footsteps.Wood:
                source.clip = clipWoods[numClip];
                break;
            case Footsteps.Dirt:
                source.clip = clipDirts[numClip];
                break;

        }
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
    public void PlayCarpet()
	{
        footSteps = Footsteps.Carpet;
		PlayF();
	}
}
