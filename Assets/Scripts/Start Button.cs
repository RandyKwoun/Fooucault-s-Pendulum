using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject explainPanel;
    [SerializeField] private TextMeshProUGUI explanation;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField, TextArea] private string text = "In 1851, a French scientist named Léon Foucault hung a heavy ball on a very long wire and let it swing. The ball kept swinging in the same direction, but the floor below it slowly turned, because the Earth is spinning! This is called Foucault's Pendulum. In this game, your brush swings like that pendulum while the picture turns underneath it. Use it to color the pictures!";
    [SerializeField] private Image completeImage;
    [SerializeField, TextArea] private string artText = "The above art is the art you need to paint! You can check this art whenever you want during the game! Good Luck!";

    void Start()
    {
        explainPanel.SetActive(false);
        explanation.text = "";
        completeImage.enabled = false;
    }

    public void OnStart()
    {
        explainPanel.SetActive(true);
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i < text.Length; i++)
        {
            explanation.text += text[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3);

        explanation.text = "";

        completeImage.enabled = true;
        for(int i = 0; i < artText.Length; i++)
        {
            explanation.text += artText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Paint");
    }
}