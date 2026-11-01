using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// This script lets the painting rotate and change the direction.
/// Attached: Unpained Art
/// </summary>

public class RotatingPaint : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotatingSpeed = 10f;
    private float direction = 1f;

    private bool isRotating = true;

    void Update()
    {
        if(isRotating)
        {
            //Vector3.forward == new Vector3(0, 0, 1)
            transform.Rotate(Vector3.forward * (rotatingSpeed * direction) * Time.deltaTime);
        }
    }

    //Change the rotational direction of the uncolored paint
    public void ChangeRotationDirection()
    {
        direction *= -1f;
    }

    public void SetRotation(bool value)
    {
        isRotating = value;
    }
}
