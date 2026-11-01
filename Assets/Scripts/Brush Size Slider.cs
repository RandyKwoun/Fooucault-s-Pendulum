using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script ables the player to change size of the brush
/// Attached: Brush Size Slider
/// </summary>

public class BrushSizeSlider : MonoBehaviour
{
    [SerializeField] private GameObject brush;
    [SerializeField] private Slider slider;
    [SerializeField] private float startValue = 1f;

    void Start()
    {
        slider.value = startValue;
        brush.transform.localScale = new Vector3(startValue, startValue, 1f);
    }

    public void ChangeBrushSize()
    {
        float newValue = slider.value;
        brush.transform.localScale = new Vector3(newValue, newValue, 1f);
    }
}
