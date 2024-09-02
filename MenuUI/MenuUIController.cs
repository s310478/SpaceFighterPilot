using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuUIController : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI volumeText;
    public RectTransform soundSlider;
    public RectTransform arrow;
    public float enlargedFontSize = 80f;
    public float originalFontSize = 50f;
    public Vector3 sliderMoveOffset = new Vector3(0, -20f, 0);
    public Vector3 sliderScaleIncrease = new Vector3(1.5f, 1.5f, 1.5f);

    private Vector3 originalSliderPosition;
    private Vector3 originalSliderScale;
    private Vector3 arrowOffset = new Vector3(100f, 0, 0); // Adjust based on your UI
    private static GameObject currentSelectedObject;
    private Oscillator arrowOscillator;

    void Start()
    {
        if (soundSlider != null)
        {
            originalSliderPosition = soundSlider.localPosition;
            originalSliderScale = soundSlider.localScale;
        }

        if (arrow != null)
        {
            arrowOscillator = arrow.GetComponent<Oscillator>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightElement(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Do nothing to keep the item selected
    }

    public void OnSelect(BaseEventData eventData)
    {
        HighlightElement(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelectedObject);
        }
        else if (currentSelectedObject != gameObject)
        {
            HighlightElement(false);
        }
    }

    private void HighlightElement(bool isHighlighted)
    {
        if (isHighlighted && currentSelectedObject != gameObject)
        {
            if (currentSelectedObject != null)
            {
                var previousMenuUIController = currentSelectedObject.GetComponent<MenuUIController>();
                if (previousMenuUIController != null)
                {
                    previousMenuUIController.ResetElement();
                }
            }
            currentSelectedObject = gameObject;
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(currentSelectedObject);
            }
        }

        if (gameObject.name == "Start Button")
        {
            SetStartButtonState(isHighlighted);
        }
        else if (gameObject.name == "Volume Parent")
        {
            SetVolumeParentState(isHighlighted);
        }
        else if (gameObject.name == "Sound Slider")
        {
            SetVolumeParentState(true); // Always highlight the volume parent when interacting with the slider
        }
    }

    private void SetStartButtonState(bool isHighlighted)
    {
        if (startText != null)
        {
            startText.fontSize = isHighlighted ? enlargedFontSize : originalFontSize;
        }
        if (arrow != null && isHighlighted)
        {
            arrow.SetParent(startText.transform, false);
            Vector3 newPosition = new Vector3(startText.preferredWidth / 2 + arrowOffset.x, 0, 0);
            arrow.localPosition = newPosition;
            if (arrowOscillator != null)
            {
                arrowOscillator.SetStartingPosition(arrow.transform.position); // Use transform.position for global position
            }
        }
    }

    private void SetVolumeParentState(bool isHighlighted)
    {
        if (volumeText != null)
        {
            volumeText.fontSize = isHighlighted ? enlargedFontSize : originalFontSize;
        }
        if (arrow != null && isHighlighted)
        {
            arrow.SetParent(volumeText.transform, false);
            Vector3 newPosition = new Vector3(volumeText.preferredWidth / 2 + arrowOffset.x, 0, 0);
            arrow.localPosition = newPosition;
            if (arrowOscillator != null)
            {
                arrowOscillator.SetStartingPosition(arrow.transform.position); // Use transform.position for global position
            }
        }
        if (soundSlider != null)
        {
            soundSlider.localPosition = isHighlighted ? originalSliderPosition + sliderMoveOffset : originalSliderPosition;
            soundSlider.localScale = isHighlighted ? sliderScaleIncrease : originalSliderScale;
        }
    }

    private void ResetElement()
    {
        if (gameObject.name == "Start Button" && startText != null)
        {
            startText.fontSize = originalFontSize;
        }
        else if (gameObject.name == "Volume Parent" || gameObject.name == "Sound Slider")
        {
            if (volumeText != null)
            {
                volumeText.fontSize = originalFontSize;
            }
            if (soundSlider != null)
            {
                soundSlider.localPosition = originalSliderPosition;
                soundSlider.localScale = originalSliderScale;
            }
        }
    }
}
