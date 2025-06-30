using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sesSlider;
    public Slider hassasSlider;
    public AudioSource audioSource;
    public GameObject player,settings;

    void Start()
    {
        sesSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        hassasSlider.value = PlayerPrefs.GetFloat("sensitivity", 1.0f);

        ApplyVolume(sesSlider.value);
        ApplySensitivity(hassasSlider.value);
        
        sesSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        hassasSlider.onValueChanged.AddListener(delegate { OnSensitivityChange(); });
    }

    private void ApplyVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void ApplySensitivity(float sensitivity)
    {
        player.GetComponent<PlayerController>().touchSensitivity = sensitivity;
    }

    public void OnVolumeChange()
    {
        float volume = sesSlider.value;
        ApplyVolume(volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void OnSensitivityChange()
    {
        float sensitivity = hassasSlider.value;
        ApplySensitivity(sensitivity);
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
    }

    public void kapat()
    {
        settings.active=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
