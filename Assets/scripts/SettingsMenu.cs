﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour 
{
	public TMP_Dropdown resolutionDropdown;
	Resolution[] resolutions;
	void Start ()
	{
		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions(); //limpia las resoluciones en el dropdown

		List<string> options = new List<string>(); //Lista de strings, que serán nuestras opciones

		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);

			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)//Probar a comparar con Screen.height/.width, por el tema de comparar solo la ventana
			{
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}
}
