using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;
using System.ComponentModel;
using Unity.VisualScripting;
using System;


public class MainSceneScript : MonoBehaviour
{
    public GameObject konuPan,settingsPan;
    public TextMeshProUGUI coinText;
    public RawImage coinImage;
    public AudioSource mainAudio;
    public AudioClip slideClip, buttonClip;
    private int startCoin,currentCoin;
    private RectTransform konuPanRect, settingsPanRect;
    private Vector2 originalPositionKonu, targetPositionKonu,targetPositionSetting,originalPositionSettings;
    // Start is called before the first frame update
    public void Start()
    {
        GetPlayerSave();
        currentCoin = PlayerPrefs.GetInt("Gold");
        GetPositionPan();
        CoinTextAnim();      
    }
    public void Basla()
    {
        mainAudio.PlayOneShot(buttonClip);
        SceneManager.LoadScene("SampleScene");
    }

    public void Kapat()
    {
        mainAudio.PlayOneShot(buttonClip);
        Application.Quit();
    }

    public void KonuPanOp()
    {
        StartCoroutine("PlayOneShotSequential");
        konuPan.gameObject.active = true;
        konuPanRect.DOAnchorPos(originalPositionKonu, 2f).SetEase(Ease.OutBack);
    }

    public void KonuPanEx()
    {
        StartCoroutine("PlayOneShotSequential"); ;
        konuPanRect.DOAnchorPos(targetPositionKonu, 2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            konuPan.gameObject.active = false;
        });
    }

    public void SettingsPanOp()
    {
        StartCoroutine("PlayOneShotSequential");
        settingsPan.gameObject.active = true;
        settingsPanRect.DOAnchorPos(originalPositionSettings, 2f).SetEase(Ease.OutBack);   
    }

    public void SettingsPanEx()
    {
        StartCoroutine("PlayOneShotSequential");
        settingsPanRect.DOAnchorPos(targetPositionSetting, 2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            settingsPan.gameObject.active = false;
        }); 
    }

    public void CoinTextAnim()
    {
        DOTween.To(() => startCoin, x =>
        {
            startCoin = x;
            coinText.text = x.ToString();
        }, currentCoin,3).SetEase(Ease.OutCubic);
        coinImage.rectTransform.DOPunchScale(Vector3.one * 0.2f, 1.5f, 5, 0.5f);
    }
    
    private void GetPositionPan()
    {
        //KONU
        konuPanRect = konuPan.GetComponent<RectTransform>();
        originalPositionKonu = konuPanRect.localPosition;
        targetPositionKonu = new Vector2(originalPositionKonu.x, -Screen.height - 1200);
        konuPanRect.anchoredPosition = targetPositionKonu;
        //AYARLAR
        settingsPanRect = settingsPan.GetComponent<RectTransform>();
        originalPositionSettings = settingsPanRect.localPosition;
        targetPositionSetting = new Vector2(originalPositionSettings.x, -Screen.height-600);
        settingsPanRect.anchoredPosition = targetPositionSetting;
    }

    private void GetPlayerSave()
    {
        if (!PlayerPrefs.HasKey("created"))
        {
            PlayerPrefs.SetInt("Gold", 0);
            PlayerPrefs.SetInt("isMuted", 1);
            PlayerPrefs.SetInt("isVib", 1);
            PlayerPrefs.SetString("created",DateTime.Now.ToString());
            Debug.Log(PlayerPrefs.GetString("created"));
        }
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://osman-dmrhn.github.io/privacy-policy/");
    }

    IEnumerator PlayOneShotSequential()
    {
        mainAudio.PlayOneShot(buttonClip);
        yield return new WaitForSeconds(buttonClip.length); // clip1 tamamlanana kadar bekle

        mainAudio.PlayOneShot(slideClip); // ardýndan clip2 çal
    }
}
