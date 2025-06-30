using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using System;

public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    //PUANLAMA SÝSTEMÝ
    private int score;
    //SORULAR
    public QuestionDataList questionDataList1,questionDataList2,questionDataList3,questionDataList4;
    private List<QuestionData> questionDatas1, questionDatas2, questionDatas3, questionDatas4;
    //SES OBJELERÝ
    public AudioSource questionAudioSource, timersound,mainAudio;
    public AudioClip correctSound, wrongSound, timesound,buttonClip,slideClip;
    //TEXTLER
    public TextMeshProUGUI a, b, c, d, questtext, questcounttext, AfillText, BfillText, CfillText, DfillText,callTrueText,score_text,coin_text,retry_text;
    //BUTTONLAR
    public List<UnityEngine.UI.Button> buttons;
    public UnityEngine.UI.Button jok1, jok2, jok3;
    public List<UnityEngine.UI.Button> callJokBut;
    //FÝLL ÝMAGE
    public UnityEngine.UI.Image timeImage, Afill, Bfill, Cfill, Dfill;
    public RawImage peopRaw, retryPan,callJokPan,coinImage,scoreImage;
    //PANEL
    public GameObject blockPan,rawBlockPan, settingsPan,peopPan,callPan;
    private List<int> trueSignalBut = new List<int>();
    private int rank;
    private int index;
    private int questCount;
    private int callButCount;
    private bool time_continue;
    private float currTime = 15.0f;
    private float time = 15.0f;
    private int questTrueAnswer;
    private bool timesoundactive,isVib;
    //
    private RectTransform callPanRect,peopPanRect, settingsPanRect;
    private Vector2 originalPositionCall, targetPositionCall, targetPositionSetting, originalPositionSettings, originalPositionPeop, targetPositionPeop;


    void Start()
    {
        score = 0;
        questionDatas1 = questionDataList1.data;
        questionDatas2 = questionDataList2.data;
        questionDatas3 = questionDataList3.data;
        questionDatas4= questionDataList4.data;
        time_continue = true;
        questCount = 1;
        rank = 1;
        callButCount = 0;
        isVib = Convert.ToBoolean(PlayerPrefs.GetInt("isVib"));
        getQuest(rank);
        GetPositionPan();
    }

    // Update is called once per frame
    void Update()
    {
        if (time_continue)
        {
            currTime -= Time.deltaTime;
            timeImage.fillAmount = currTime / time;

            if (currTime < 7 && timesoundactive == false)
            {
                timesoundactive = true;
                timersound.Play();
            }
            else if (currTime <= 0)
            {
                TimeEnd();
            }
        }      
        
    }


    public void RankCont()
    {
        if (questCount % 4 == 0 && rank < 4)
        {
            rank++;
            time= +10;
        }
    }
    public void buttonVis()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].interactable = true;
            buttons[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }


    //JOKER ÝÞLEMLERÝ
    //1/2 JOKERÝ ÝÞLEMLERÝ
    public void jok1_2()
    {
        score_dec();
        switch (rank)
        {
            case 1:
                jok1_2if(questionDatas1);
                break;
            case 2:
                jok1_2if(questionDatas2);
                break;
            case 3:
                jok1_2if(questionDatas3);
                break;
            case 4:
                jok1_2if(questionDatas4);
                break;
        }
    }
    private void jok1_2if(List<QuestionData> input)
    {
        for (int i = 0; i < 2;)
        {
            int rnd = UnityEngine.Random.Range(0, 3);
            if (rnd != input[index].trueAnswers)
            {
                if (buttons[rnd].interactable == false)
                {
                    continue;
                }
                buttons[rnd].interactable = false;
                i++;
                jok3.interactable = false;
            }
        }
    }
    //SEYÝRCÝ JOKERÝ ÝÞLEMLERÝ
    public void PeopJok()
    {
        score_dec();
        StartCoroutine("PlayOneShotSequential");
        peopPan.gameObject.active = true;
        peopPanRect.DOAnchorPos(originalPositionPeop, 2f).SetEase(Ease.OutBack);
        rawBlockPan.active = true;
        Invoke("PeopJok2", 0.51f);

    }

    private void PeopJok2()
    {
        switch (rank)
        {
            case 1:
                peopJokÝf(questionDatas1);
                break;
            case 2:
                peopJokÝf(questionDatas2);
                break;
            case 3:
                peopJokÝf(questionDatas3);
                break;
            case 4:
                peopJokÝf(questionDatas4);
                break;
        }

        jok2.interactable = false;
    }
    private void peopJokÝf(List<QuestionData> input)
    {
        int[] answer = new int[4] { 0, 0, 0, 0 };
        int cevap = input[index].trueAnswers;
        int top = 100;
        System.Random r = new System.Random();
        answer[cevap] = r.Next(40, 60);
        top -= answer[cevap];
        for (int i = 0; i < 4; i++)
        {
            if (answer[i] == 0)
            {
                answer[i] = r.Next(0, top);
                top -= answer[i];
            }
        }
        answer[cevap] += top;
        AfillText.text = "%" + answer[0];
        BfillText.text = "%" + answer[1];
        CfillText.text = "%" + answer[2];
        DfillText.text = "%" + answer[3];
        peopRaw.GetComponent<Animator>().enabled = false;
        Afill.fillAmount = (answer[0] / 100.0f);
        Bfill.fillAmount = (answer[1] / 100.0f);
        Cfill.fillAmount = (answer[2] / 100.0f);
        Dfill.fillAmount = (answer[3] / 100.0f);
    }
    //ARAMA JOKERÝ ÝÞLEMLERÝ
    public void callJok()
    {
        score_dec();
        rawBlockPan.active = true;
        time_continue = false;
        jok1.interactable = false;
        callPan.gameObject.active = true;
        StartCoroutine("PlayOneShotSequential");
        callPanRect.DOAnchorPos(originalPositionCall, 2f).SetEase(Ease.OutBack);

        List<int> signalBut = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
       
        int k = 0;

        StartCoroutine(ChangeSignalColors(signalBut, k, trueSignalBut));
    }

    private IEnumerator ChangeSignalColors(List<int> signalBut, int k, List<int> trueSignalBut)
    {
        blockPan.active = true; 
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 7; i++)
        {
            int j = UnityEngine.Random.Range(0, signalBut.Count);
            int rand = UnityEngine.Random.Range(0, 2);

            if (rand == 1||k>=2)
            {
                // Yeþil Yakma
                callJokBut[signalBut[j]].GetComponent<UnityEngine.UI.Image>().color = Color.green;
                trueSignalBut.Add(signalBut[j]);
                yield return new WaitForSeconds(0.5f); // Rengi gösterme süresi
                callJokBut[signalBut[j]].GetComponent<UnityEngine.UI.Image>().color = Color.white;
                signalBut.RemoveAt(j);
            }
            else if (rand == 0 && k <= 2)
            {
                k++;
                // Kýrmýzý Yakma
                callJokBut[signalBut[j]].GetComponent<UnityEngine.UI.Image>().color = Color.red;
                yield return new WaitForSeconds(0.5f); // Rengi gösterme süresi
                callJokBut[signalBut[j]].GetComponent<UnityEngine.UI.Image>().color = Color.white;
                signalBut.RemoveAt(j);
            }
            
            yield return new WaitForSeconds(0.5f); // Butonlar arasýndaki geçiþ süresi
        }
        blockPan.active = false;

    }

    public void callButCont(int count)
    {     
        if(count == trueSignalBut[callButCount])
        {
            callButCount++;
            callJokBut[count].GetComponent<UnityEngine.UI.Image>().color = Color.green;
            if (callButCount == trueSignalBut.Count)
            {
                callTrueText.text = buttons[questTrueAnswer].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text;
            }
        }
        else
        {
            callJokBut[count].GetComponent<UnityEngine.UI.Image>().color = Color.red;
            callTrueText.text = "Baðlantý Koptu";
        }
    }

    //SORU ÝÞLEMLERÝ
    private void getQuestforDatas(List<QuestionData> inputData)
    {
        index = UnityEngine.Random.Range(0, inputData.Count);
        if (inputData[index] != null)
        {
            a.text = inputData[index].answers[0];
            b.text = inputData[index].answers[1];
            c.text = inputData[index].answers[2];
            d.text = inputData[index].answers[3];
            questtext.text = inputData[index].questionText;
            questTrueAnswer = inputData[index].trueAnswers;
            blockPan.gameObject.SetActive(false);
            time_continue = true;
        }
        else
        {
            getQuestforDatas(inputData);
        }
    }

    private void nextQuestion()
    {
        if (questCount != 15)
        {
            if (rank == 1)
            {
                questionDatas1.RemoveAt(index);
            }
            else if (rank == 2)
            {
                questionDatas2.RemoveAt(index);
            }
            else if (rank == 3)
            {
                questionDatas3.RemoveAt(index);
            }
            else if (rank == 4)
            {
                questionDatas4.RemoveAt(index);
            }
            RankCont();
            questCount++;
            questcounttext.text = "15/" + questCount;
            currTime = time;
            getQuest(rank);
        }
        else
        {
            RetryPanOp();
        }
    }

    private void getQuest(int level)
    {
        switch (level)
        {
            case 1:
                getQuestforDatas(questionDatas1);
                break;
            case 2:
                getQuestforDatas(questionDatas2);
                break;
            case 3:
                getQuestforDatas(questionDatas3);
                break;
            case 4:
                getQuestforDatas(questionDatas4);
                break;
        }
        buttonVis();

    }
    public void AnswerCorrect(int count)
    {
        blockPan.gameObject.SetActive(true);
        time_continue = false;
        if (timersound.isPlaying)
        {
            timesoundactive = false;
            timersound.Pause();
        }
        currTime = time;
        if (count == questTrueAnswer)
        {
            score_add();
            buttons[count].GetComponent<Animation>().Play("true");
            questionAudioSource.PlayOneShot(correctSound);
            Invoke("nextQuestion", 0.8f);
            if(isVib)
                SoundandVibManager.Vibrate(30);
            
        }
        else
        {
            buttons[count].GetComponent<Animation>().Play("false");
            buttons[questTrueAnswer].GetComponent<Animation>().Play("true");
            questionAudioSource.PlayOneShot(wrongSound);
            retry_text.text = "Yanlýþ Cevap";
            Invoke("RetryPanOp", 0.9f);
            StartCoroutine(DisableBlockPanelAfterDelay());
            if(isVib)
                SoundandVibManager.Vibrate(60);
        }
    }

    private IEnumerator DisableBlockPanelAfterDelay()
    {
        yield return new WaitForSeconds(1); // Ýþlem süresi
        blockPan.SetActive(false);
    }
    //SCORE ÝÞLEMLERÝ
    private void score_add()
    {
        score += 100;
    }
    private void score_dec()
    {
        if(score>0)
            score -= 50;
    }
    //BUTON ÝÞLEMLERÝ
    public void peopRawExit()
    {
        StartCoroutine("PlayOneShotSequential");
        callPanRect.DOAnchorPos(targetPositionPeop, 2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            peopPan.gameObject.active = false;
        });
        rawBlockPan.active = false;
    }
    public void callRawExit()
    {
        StartCoroutine("PlayOneShotSequential");
        callPanRect.DOAnchorPos(targetPositionCall, 2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            callPan.gameObject.active = false;
        });
        rawBlockPan.active = false;
        time_continue = true;
    }
    public void RetryPanOp()
    {
        int startCoin = 0;
        int startScore = 0;
        int currentCoin, wonCoin;
        retryPan.gameObject.SetActive(true);
        DOTween.To(() => startScore, x =>
        {
            startScore = x;
            score_text.text = x.ToString();
        }, score, 3).SetEase(Ease.OutCubic);
        scoreImage.rectTransform.DOPunchScale(Vector3.one * 0.2f, 4f, 5, 0.5f);
        wonCoin = score / 10;
        coin_text.text = wonCoin.ToString();
        DOTween.To(() => startCoin, x =>
        {
            startCoin = x;
            coin_text.text = x.ToString();
        }, wonCoin, 3).SetEase(Ease.OutCubic);
        coinImage.rectTransform.DOPunchScale(Vector3.one * 0.2f, 4f, 5, 0.5f);
        currentCoin =PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", currentCoin + wonCoin);
        rawBlockPan.active = true;
    }
    
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void anaMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SettingsPanOp()
    {
        StartCoroutine("PlayOneShotSequential");  
        settingsPanRect.DOAnchorPos(originalPositionSettings, 2f).SetEase(Ease.OutBack).OnPlay(()=>{
            settingsPan.gameObject.active = true;
        });
    }

    public void SettingsPanEx()
    {
        StartCoroutine("PlayOneShotSequential");
        settingsPanRect.DOAnchorPos(targetPositionSetting, 2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            settingsPan.gameObject.active = false;
        });
    }

    private void GetPositionPan()
    {
        //AYARLAR
        settingsPanRect = settingsPan.GetComponent<RectTransform>();
        originalPositionSettings = settingsPanRect.localPosition;
        targetPositionSetting = new Vector2(originalPositionSettings.x, -Screen.height/2-300);
        settingsPanRect.anchoredPosition=targetPositionSetting;
        //CALL
        callPanRect=callPan.GetComponent<RectTransform>();
        originalPositionCall=callPanRect.localPosition;
        targetPositionCall = new Vector2(originalPositionCall.x, Screen.height);
        callPanRect.anchoredPosition=targetPositionCall;
        //PEOP
        peopPanRect=peopPan.GetComponent<RectTransform>();
        originalPositionPeop=peopPanRect.localPosition;
        targetPositionPeop= new Vector2(originalPositionPeop.x, Screen.height);
        peopPanRect.anchoredPosition = targetPositionPeop;
    }

    IEnumerator PlayOneShotSequential()
    {
        mainAudio.PlayOneShot(buttonClip);
        yield return new WaitForSeconds(buttonClip.length); // clip1 tamamlanana kadar bekle
        mainAudio.PlayOneShot(slideClip); // ardýndan clip2 çal
    }

    private void TimeEnd()
    {
        timersound.Stop();
        questionAudioSource.PlayOneShot(wrongSound);
        retry_text.text = "Süre Bitti.";
        Invoke("RetryPanOp", 0.9f);
        StartCoroutine(DisableBlockPanelAfterDelay());
    }
}
