using UnityEngine;

public class FinishPaintingButton : MonoBehaviour
{
    [SerializeField] private GameObject recheckingPanel;
    private bool isRechecking = false;

    void Start()
    {
        isRechecking = false;
        recheckingPanel.SetActive(false);
    }

    public void ToggleReCheckingPanel()
    {
        isRechecking = !isRechecking;
        recheckingPanel.SetActive(isRechecking);
    }
}
