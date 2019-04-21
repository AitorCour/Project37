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
    private float lightInt;
    public Slider master;
    public Slider effects;
    public Slider music;
    public Slider lightSl;

    public RectTransform selectedRes;
    public RectTransform selectedQual;
    private float resPos;
    private float qualPos;

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

        lightInt = PlayerPrefs.GetFloat("Light_Intensity");
        lightSl.value = lightInt;
        SetLight(lightInt);

        resPos = PlayerPrefs.GetFloat("Pos_Res");
        selectedRes.localPosition = new Vector3(-450, resPos, 0);

        qualPos = PlayerPrefs.GetFloat("Pos_Qual");
        selectedQual.localPosition = new Vector3(-205, qualPos, 0);
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
    public void SetLight(float light)
    {
        lightInt = light;
        RenderSettings.ambientIntensity = light;
        SaveLight();
    }
    public void SetResolution720()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
        selectedRes.localPosition = new Vector3(-450, 85, 0);
        resPos = 85f;
        SavePositionRes();
        //Debug.Log("resolution 720");
    }
    public void SetResolution1200()
    {
        Screen.SetResolution(1600, 1200, Screen.fullScreen);
        selectedRes.localPosition = new Vector3(-450, 30, 0);
        resPos = 30f;
        SavePositionRes();
        //Debug.Log("resolution 1200");
    }
    public void SetResolution1080()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        selectedRes.localPosition = new Vector3(-450, -25, 0);
        resPos = -25f;
        SavePositionRes();
        //Debug.Log("resolution 1080");
    }
	public void SetQualityUltra ()
	{
		QualitySettings.SetQualityLevel(0);
        selectedQual.localPosition = new Vector3(-205, 85, 0);
        qualPos = 85;
        SavePositionQual();
    }
	public void SetQualityHigh ()
	{
		QualitySettings.SetQualityLevel(1);
        selectedQual.localPosition = new Vector3(-205, 25, 0);
        qualPos = 25;
        SavePositionQual();
    }
	public void SetQualityMedium ()
	{
		QualitySettings.SetQualityLevel(2);
        selectedQual.localPosition = new Vector3(-205, -25, 0);
        qualPos = -25;
        SavePositionQual();
    }
	public void SetQualityLow ()
	{
		QualitySettings.SetQualityLevel(3);
        selectedQual.localPosition = new Vector3(-205, -85, 0);
        qualPos = -85;
        SavePositionQual();
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
    void SaveLight()
    {
        PlayerPrefs.SetFloat("Light_Intensity", lightInt);
    }
    void SavePositionRes()
    {
        PlayerPrefs.SetFloat("Pos_Res", resPos);
    }
    void SavePositionQual()
    {
        PlayerPrefs.SetFloat("Pos_Qual", qualPos);
    }
}
