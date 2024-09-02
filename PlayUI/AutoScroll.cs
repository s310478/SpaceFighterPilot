using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TextMeshProUGUI textMeshPro;
    public int maxLines = 2;

    private void Update()
    {
        int currentLineCount = textMeshPro.textInfo.lineCount;

        // Check if the current number of lines exceeds the maximum allowed lines
        if (currentLineCount > maxLines)
        {
            // Calculate the new vertical position to scroll up by one line
            float scrollValue = (float)(currentLineCount - maxLines) / (float)currentLineCount;
            scrollRect.verticalNormalizedPosition = 1 - scrollValue;
        }
    }
}