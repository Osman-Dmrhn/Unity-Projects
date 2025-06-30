using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonAnim : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    public float slideDuration = 1f;
    public float slidedirec = 1f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        originalPosition = rectTransform.anchoredPosition;

        rectTransform.anchoredPosition = new Vector2(-Screen.width*slidedirec, originalPosition.y);

        rectTransform.DOAnchorPos(originalPosition, slideDuration).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
