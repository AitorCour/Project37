using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour 
{
    public AudioMixer masterVol;
    public float vol_1;
    private float vol_2;
    private float vol_3;
    public Slider master;
    public Slider effects;
    public Slider music;
    void Start()
    {
        vol_1 = PlayerPrefs.GetFloat("Master_Vol");
        master.value = vol_1;
        //Debug.Log(master.value);
        masterVol.SetFloat("Master", vol_1);

        vol_2 = PlayerPrefs.GetFloat("Effects_Vol");
        effects.value = vol_2;
        masterVol.SetFloat("Effects", vol_2);

        vol_3 = PlayerPrefs.GetFloat("Music_Vol");
        music.value = vol_3;
        masterVol.SetFloat("Music", vol_3);
    }
    public void SetVolumeMaster(float volume_1)
    {
        vol_1 = volume_1;
        masterVol.SetFloat("Master", volume_1);
        SaveVolume_Master();
    }
    public void SetVolumeEfect (float volume_2)
	{
        vol_2 = volume_2;
        masterVol.SetFloat("Effects", volume_2);
        SaveVolume_Effect();
	}
	public void SetVolumeMusic (float volume_3)
	{
        vol_3 = volume_3;
        masterVol.SetFloat("Music", volume_3);
        SaveVolume_Music();
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
    void SaveVolume_Master()
    {
        PlayerPrefs.SetFloat("Master_Vol", vol_1);
        //Debug.Log(vol_1);
        //PlayerPrefs.Save();
    }
    void SaveVolume_Effect()
    {
        PlayerPrefs.SetFloat("Effects_Vol", vol_2);
    }
    void SaveVolume_Music()
    {
        PlayerPrefs.SetFloat("Music_Vol", vol_3);
    }
}
