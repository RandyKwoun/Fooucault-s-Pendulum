using UnityEngine;

/// <summary>
/// This script controls Brush's movement (gravity affected)
/// Attached: Brush
/// </summary>

public class BrushMovement : MonoBehaviour
{
    [SerializeField] private float maxDirection = 3.5f;
    [SerializeField] private float speed = 1f;

    private float originalPosY = 0f;

    void Start()
    {
        transform.position = new Vector3(0, originalPosY, 0);
    }

    void Update()
    {
        float posY = maxDirection * Mathf.Sin(speed * Time.time) + originalPosY;
        transform.position = new Vector3(0, posY, 0);
    }
}
