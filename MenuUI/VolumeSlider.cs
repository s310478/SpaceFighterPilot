using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("volumeSetting"))
        {
            PlayerPrefs.SetFloat("volumeSetting", 1);
            Load();
        }
        else
        {
            Load();
        }

        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumeSetting");
    }

    void Save()
    {
        PlayerPrefs.SetFloat("volumeSetting", volumeSlider.value);
    }
}
