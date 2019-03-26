using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour 
{
    public AudioMixer masterVol;
    private float vol_1;
    private float vol_2;
    private float vol_3;
    public Slider master;
    private void Start()
    {
        vol_1 = PlayerPrefs.GetFloat("Master_Vol");
        //volume_2 = PlayerPrefs.GetFloat("p_z");
        //vol_1 = -40.0f;
        master.value = vol_1;
        masterVol.SetFloat("Master", vol_1);   
    }

    public void SetVolumeMaster(float volume_1)
    {
        vol_1 = volume_1;
        masterVol.SetFloat("Master", volume_1);
    }
    public void SetVolumeEfect (float volume_2)
	{
        volume_2 = vol_2;
        masterVol.SetFloat("Effects", volume_2);
	}
	public void SetVolumeMusic (float volume_3)
	{
        volume_3 = vol_3;
        masterVol.SetFloat("Music", volume_3);
	}
    public void SetResolution720()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
        //Debug.Log("resolution 720");
    }
    public void SetResolution1200()
    {
        Screen.SetResolution(1600, 1200, Screen.fullScreen);
        //Debug.Log("resolution 1200");
    }
    public void SetResolution1080()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        //Debug.Log("resolution 1080");
    }
	public void SetQualityUltra ()
	{
		QualitySettings.SetQualityLevel(0);
	}
	public void SetQualityHigh ()
	{
		QualitySettings.SetQualityLevel(1);
	}
	public void SetQualityMedium ()
	{
		QualitySettings.SetQualityLevel(2);
	}
	public void SetQualityLow ()
	{
		QualitySettings.SetQualityLevel(3);
	}
	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Master_vol", vol_1);
        Debug.Log("MusicSaved");
    }
}
