using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private int dil;
    public GameObject setting;
    public Slider bar;
    public GameObject loadscreen;
    void Start()
    {
        dil= 0;
        if (PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);     
        }
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("sensitivity", 150f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void basla()
    {
        SceneManager.LoadScene("testmap");
        Time.timeScale = 1;
    }

    public void cikis()
    {
        Application.Quit();
    }

    public void settings()
    {
        setting.active = true;
    }
    public void kapat()
    {
        setting.active=false;
    }

    public void load()
    {
        loadscreen.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(startload());
    }

    public void Language()
    {
        if(dil==0)
        {
            dil= 1;
            SetLang("English");
        }
        else
        {
            dil = 0;
            SetLang("Turkish");
        }
        
    }

    public void SetLang(string dil)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.LocaleName.Equals(dil))
            {
                LocalizationSettings.SelectedLocale=locale;
                return;
            }
        }
    }

    IEnumerator startload()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("testmap");
        while(!async.isDone) {
            bar.value = async.progress;
            yield return null;
        }

    }
}
