using UnityEngine;
using UnityEngine.UI;

public class GradeStars : MonoBehaviour
{
    [SerializeField] private Grading grading;

    [Header("Images")]
    [SerializeField] private Sprite threeStars;
    [SerializeField] private Sprite twoStars;
    [SerializeField] private Sprite oneStar;
    [SerializeField] private Sprite noStar;

    [Header("Panel")]
    [SerializeField] private GameObject starPanel;
    [SerializeField] private Image apprImage;

    void Start()
    {
        starPanel.SetActive(false);
    }

    void OnEnable()
    {
        grading.OnGrading += ShowStars;
    }

    void OnDisable()
    {
        grading.OnGrading -= ShowStars;
    }

    private void ShowStars(int grade)
    {
        if(grade == 3)
        {
            apprImage.sprite = threeStars;
        }

        else if(grade == 2)
        {
            apprImage.sprite = twoStars;
        }

        else if(grade == 1)
        {
            apprImage.sprite = oneStar;
        }

        else
        {
            apprImage.sprite = noStar;
        }

        starPanel.SetActive(true);
    }
}