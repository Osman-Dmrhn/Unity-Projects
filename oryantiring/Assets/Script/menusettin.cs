using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menusettings : MonoBehaviour
{
    public Slider sesSlider;
    public Slider hassasSlider;
    // Start is called before the first frame update
    void Start()
    {
        sesSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        hassasSlider.value = PlayerPrefs.GetFloat("sensitivity", 1.0f);

        

        sesSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        hassasSlider.onValueChanged.AddListener(delegate { OnSensitivityChange(); });
    }

    public void OnVolumeChange()
    {
        float volume = sesSlider.value;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void OnSensitivityChange()
    {
        float sensitivity = hassasSlider.value;
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
