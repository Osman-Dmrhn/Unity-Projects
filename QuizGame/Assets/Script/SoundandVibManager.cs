using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundandVibManager : MonoBehaviour
{
    private static AndroidJavaObject vibrator;
    public Sprite soundOn, soundOff, vibOn, vibOff;
    public Image soundBut, vibBut;
    private static bool isMuted,isVib;
    private AudioSource[] allSources;
    // Start is called before the first frame update
    void Start()
    {
        allSources=FindObjectsOfType<AudioSource>();
        isMuted = Convert.ToBoolean(PlayerPrefs.GetInt("isMuted"));
        isVib = Convert.ToBoolean(PlayerPrefs.GetInt("isVib"));
        soundBut.sprite = isMuted ? soundOn : soundOff;
        vibBut.sprite = isVib ? vibOn :  vibOff;
        SoundVolume();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        SoundVolume();
        soundBut.sprite = isMuted ? soundOn :  soundOff;
        PlayerPrefs.SetInt("isMuted", Convert.ToInt32(isMuted));
    }

    private void SoundVolume()
    {
        foreach (AudioSource source in allSources)
        {
            if (isMuted)
            {
                source.volume = 100f;
            }
            else
            {
                source.volume = 0f;
            }

        }
    }

    public void ToggleVib()
    {
        isVib = !isVib;
        vibBut.sprite = isVib ? vibOn : vibOff;
        PlayerPrefs.SetInt("isVib", Convert.ToInt32(isVib));
    }


    private void Awake()
    {
        // Ayarý yükle
        isVib = Convert.ToBoolean(PlayerPrefs.GetInt("isVib"));

    #if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            vibrator = activity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        }
    #endif
    }

    public static void Vibrate(long milliseconds = 50)
    {
        if (!isVib) return;

    #if UNITY_ANDROID && !UNITY_EDITOR
        if (vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
    #else
        Handheld.Vibrate(); 
    #endif
    }
}
