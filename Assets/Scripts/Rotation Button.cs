using UnityEngine;

/// <summary>
/// This button lets player change the painting's rotational direction.
/// Attached: Rotation Button
/// </summary>

public class RotationButton : MonoBehaviour
{
    [SerializeField] private RotatingPaint rotatingPaint;
    public void ChangeDirection()
    {
        rotatingPaint.ChangeRotationDirection();
    }
}
