using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float typingSpeed = 0.04f;

    private Coroutine typingCoroutine;

    public void ShowText(string textToDisplay)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(textToDisplay));
    }

    private IEnumerator TypeText(string textToDisplay)
    {
        subtitleText.text = "";
        foreach (char letter in textToDisplay.ToCharArray())
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
