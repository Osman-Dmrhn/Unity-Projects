using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryGameController : MonoBehaviour
{
    bool esc;
    bool mapk = false;
    public int top;
    public TMP_Text sure, finishtext;
    public Texture[] Image;
    public GameObject pause, finish, map, player, setting, maptest, loadscreen;
    public Slider bar;
    public GameObject[] target;
    public AudioSource audioSource;
    int dakika;
    int saniye;
    string duzensur;
    private float gecens = 0;
    void Start()
    {
        Time.timeScale = 1;
        esc = false;
        top= 0;
        target[top].active=true;
        maptest.GetComponent<RawImage>().texture = Image[0];
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
        else if (mapk == true)
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
        target[top].active = true;
        maptest.GetComponent<RawImage>().texture = Image[top];
    }

    public void tekrar()
    {
        Time.timeScale = 1;
        load("StoryScene");
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
        finishtext.text = duzensur;
        finish.active = true;
        Time.timeScale = 0;
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
}
