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
    public AudioClip[] clipHall;
    public enum Footsteps{Carpet,Dirt,Wood,Hall}
    public Footsteps footSteps = new Footsteps();

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

    
    public void PlayF()
    {
		int numClip = 0;

        GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        //obj.name = "AUDIO_" + clips[numClip].name;

        switch(footSteps){
            case Footsteps.Carpet:
                numClip = Random.Range(0, clipCarpets.Length);
                source.clip = clipCarpets[numClip];
                break;
            case Footsteps.Wood:
                numClip = Random.Range(0, clipWoods.Length);
                source.clip = clipWoods[numClip];
                break;
            case Footsteps.Dirt:
                numClip = Random.Range(0, clipDirts.Length);
                source.clip = clipDirts[numClip];
                break;
            case Footsteps.Hall:
                numClip = Random.Range(0, clipHall.Length);
                source.clip = clipHall[numClip];
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
	}
    public void PlayWood()
	{
        footSteps = Footsteps.Wood;
	}
    public void PlayDirt()
	{
        footSteps = Footsteps.Dirt;
	}
    public void PlayHall()
	{
        footSteps = Footsteps.Hall;
	}
}
