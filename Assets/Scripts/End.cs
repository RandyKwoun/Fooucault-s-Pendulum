using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void OnEnd()
    {
        SceneManager.LoadScene("Credits");
    }
}