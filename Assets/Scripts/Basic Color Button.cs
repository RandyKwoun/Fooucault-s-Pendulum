using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script makes the player to change the brush color
/// Attached: All color buttons (Red, Blue, ... , White (erase));
/// </summary>

public class BasicColorButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer brush;
    [SerializeField] private Image image;
    [SerializeField] private Color color;

    private void Awake()
    {
        image.color = color;
    }

    public void ChangeBrushColor()
    {
        brush.color = color;
    }
}
