using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public TypewriterEffect typewriterEffect;

    private void Start()
    {
        if (typewriterEffect == null)
        {
            Debug.LogError("Typewriter Effect is not assigned!");
        }
    }

    public void UpdateSubtitle(string newText)
    {
        typewriterEffect.ShowText(newText);
    }
}