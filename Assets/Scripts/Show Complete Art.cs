using UnityEngine;

public class ShowCompleteArt : MonoBehaviour
{
    [SerializeField] private GameObject completeArt;
    private bool isCompleteArt = false;

    void Start()
    {
        isCompleteArt = false;
        completeArt.SetActive(isCompleteArt);
    }

    public void ToggleCompleteArt()
    {
        isCompleteArt = !isCompleteArt;

        completeArt.SetActive(isCompleteArt);

        Time.timeScale = isCompleteArt ? 0f : 1f;
    }
}
