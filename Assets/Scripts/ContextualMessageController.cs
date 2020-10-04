﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextualMessageController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private TMP_Text messageText;
    [SerializeField] private float textDuration;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponent<TMP_Text>();

        canvasGroup.alpha = 0;

        StartCoroutine(ShowMessage("Testing", textDuration));

    }


    private IEnumerator ShowMessage(string message, float duration)
    {
        canvasGroup.alpha = 1;
        messageText.text = message;
        //wait for duration
        yield return new WaitForSeconds(duration);
        canvasGroup.alpha = 0;
    }

}
