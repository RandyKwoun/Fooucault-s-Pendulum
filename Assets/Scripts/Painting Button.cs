using System;
using UnityEngine;

/// <summary>
/// This script makes player can color whenever they want
/// Attached: Painting Button
/// </summary>

public class PaintingButton : MonoBehaviour
{
    [SerializeField] private GameObject brush;
    [SerializeField] private GameObject colorPaint;
    [SerializeField] private GameObject unpaintedArt;

    public event Action<GameObject> OnPainted;

    public void Painting()
    {
        SpriteRenderer brushSprite = brush.GetComponent<SpriteRenderer>();
        GameObject paint = Instantiate(colorPaint, brush.transform.position, brush.transform.rotation);
        SpriteRenderer paintSprite = paint.GetComponent<SpriteRenderer>();

        paintSprite.color = brushSprite.color; //set color
        paint.transform.localScale = brush.transform.localScale; //set scale

        paint.transform.SetParent(unpaintedArt.transform); //set the paint as a child of the art
        OnPainted?.Invoke(paint); //To put the paint into the stack
    }
}
