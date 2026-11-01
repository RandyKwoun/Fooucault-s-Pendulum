using UnityEngine;
using System.Collections.Generic;

public class UndoButton : MonoBehaviour
{
    [Header("Undo")]
    [SerializeField] private PaintingButton paintingButton;
    private Stack<GameObject> paintedPaints = new Stack<GameObject>();

    private void Start()
    {
        paintedPaints.Clear(); //Reset the stack
    }

    private void OnEnable()
    {
        paintingButton.OnPainted += StackPush;
    }

    private void OnDisable()
    {
        paintingButton.OnPainted -= StackPush;
    }

    //Add a paint to the stack
    private void StackPush(GameObject paint)
    {
        paintedPaints.Push(paint);
    }

    public void Undo()
    {
        //Stack is empty
        if(paintedPaints.Count <= 0)
        {
            return;
        }

        GameObject removePaint = paintedPaints.Peek();
        paintedPaints.Pop();

        Destroy(removePaint);
    }
}