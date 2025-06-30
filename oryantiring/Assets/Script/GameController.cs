using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public TMP_Text sure, finishtext;
    private float gecens = 0;
    bool esc;
    bool mapk = false;
    public int top;
    public GameObject pause, finish, map, player, setting,loadscreen,tutorial,tutorial2;
    public Slider bar;
    public AudioSource audioSource;
    string duzensur;
    int dakika;
    int saniye;
    void Start()
    {
        Time.timeScale = 1;
        esc = false;
        player.GetComponent<PlayerController>().touchSensitivity = PlayerPrefs.GetFloat("sensitivity", 1.0f);
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Geçen Süre Hes.
        gecens += Time.deltaTime;
        dakika = Mathf.FloorToInt(gecens / 60F);
        saniye = Mathf.FloorToInt(gecens % 60F);

        duzensur = string.Format("{0:00}:{1:00}", dakika, saniye);

        sure.text = duzensur;
    }

    public void Pausebut()
    {
        //Pause
        if (esc == false)
        {
            esc = true;
            pause.active = true;
            Time.timeScale = 0;
        }
        else if (esc != false)
        {
            esc = false;
            pause.active = false;
            Time.timeScale = 1;
        }
    }

    public void Mapbut()
    {
        if (mapk == false)
        {
            map.active = true;
            mapk = true;

        }
        else if ( mapk == true)
        {
            map.active = false;
            mapk = false;
        }
    }

    public void devam()
    {
        esc = false;
        pause.active = false;
        Time.timeScale = 1;
        if (setting.active = true)
        {
            setting.active = false;
        }
    }

    public void topla()
    {
        top++;
    }

    public void tekrar()
    {
        Time.timeScale = 1;
        load("testmap");
    }

    public void anamenu()
    {
        load("SampleScene");
    }

    public void settin()
    {
        setting.active = true;
    }

    public void kazanma()
    {
        esc = true;
        finish.active = true;
        Time.timeScale = 0;
        duzensur = string.Format("{0:00}:{1:00}", dakika, saniye);
        finishtext.text = duzensur;
    }

    public void GetScene()
    {
        load("StoryScene");
    }

    public void load(string map)
    {
        loadscreen.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(startload(map));
    }

    IEnumerator startload(string map)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(map);
        while (!async.isDone)
        {
            bar.value = async.progress;
            yield return null;
        }

    }

    public void Tutkapat()
    {
        tutorial.SetActive(false);
        tutorial2.SetActive(true);
    }

    public void Tut2Kpat()
    {
        tutorial2.SetActive(false);
    }
}
