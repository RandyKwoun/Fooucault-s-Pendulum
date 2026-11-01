using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void OnRetry()
    {
        SceneManager.LoadScene("Paint");
    }
}