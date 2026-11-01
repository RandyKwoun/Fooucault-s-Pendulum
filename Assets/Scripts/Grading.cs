using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Grading : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PreparingGrade preparingGrade;
    [SerializeField] private RotatingPaint rotatingPaint;
    [SerializeField] private GameObject brush;
    [SerializeField] private SpriteRenderer lineArt;
    [SerializeField] private Camera scanCamera;

    [Header("Answer")]
    [SerializeField] private Texture2D answerTexture;

    [Header("Player")]
    [SerializeField] private Transform unpaintedArt;

    [Header("Grading")]
    [SerializeField] private float threeStars = 0.75f;
    [SerializeField] private float twoStars = 0.6f;
    [SerializeField] private float oneStar = 0.45f;

    [Space]
    [SerializeField] private int sameColorBoundary = 25;
    private Color32[] answerPixels;

    public event Action<int> OnGrading;

    void Start()
    {
        answerPixels = answerTexture.GetPixels32();
    }

    //Check if they are the same color
    private bool IsSameColor(Color32 a, Color32 b)
    {
        int dr = a.r - b.r;
        int dg = a.g - b.g;
        int db = a.b - b.b;

        return (dr * dr) + (dg * dg) + (db * db) < (sameColorBoundary * sameColorBoundary);
    }

    //Counting correct pixels
    private int[] CountCorrectPixels(Color32[] playerPixels)
    {
        int[] correct = new int[preparingGrade.RegionSizes.Count];

        for(int i = 0; i < answerPixels.Length; i++)
        {
            if(preparingGrade.RegionId[i] < 0)
            {
                continue;
            }

            if(IsSameColor(playerPixels[i], answerPixels[i]))
            {
                correct[preparingGrade.RegionId[i]]++;
            }
        }

        return correct;
    }

    //Scaning player's paint
    private Color32[] PlayerPaintScan()
    {
        //Stop rotating and set it 0 degree
        unpaintedArt.rotation = Quaternion.Euler(0f, 0f, 0f);
        rotatingPaint.SetRotation(false);

        //Disable unecessary objects
        brush.SetActive(false);

        //Setting Camera
        scanCamera.transform.position = new Vector3(lineArt.bounds.center.x, lineArt.bounds.center.y, -10f);
        scanCamera.orthographicSize = lineArt.bounds.size.y / 2;
        scanCamera.aspect = 1;

        //Scan Start
        RenderTexture playerPaint = new RenderTexture(preparingGrade.Width, preparingGrade.Height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        
        RenderPipeline.StandardRequest request = new RenderPipeline.StandardRequest();
        request.destination = playerPaint;

        if(RenderPipeline.SupportsRenderRequest(scanCamera, request))
        {
            RenderPipeline.SubmitRenderRequest(scanCamera, request);
        }

        else
        {
            Debug.LogError("Didn't scan");
            return null;
        }

        RenderTexture previousActive = RenderTexture.active;
        RenderTexture.active = playerPaint;

        Texture2D scanResult = new Texture2D(preparingGrade.Width, preparingGrade.Height, TextureFormat.RGBA32, false);
        scanResult.ReadPixels(new Rect(0, 0, preparingGrade.Width, preparingGrade.Height), 0, 0);

        Color32[] playerPixels = scanResult.GetPixels32();

        RenderTexture.active = previousActive;
        playerPaint.Release();
        Destroy(playerPaint);
        Destroy(scanResult);

        return playerPixels;
    }

    public void GradeProcess()
    {
        float grade = 0f;

        //Scaning player's answer
        Color32[] playerPixels = PlayerPaintScan();

        if(playerPixels == null)
        {
            return;
        }

        int[] correct = CountCorrectPixels(playerPixels);

        //Getting grade from each region
        for(int i = 0; i < correct.Length; i++)
        {
            grade += (float)correct[i] / preparingGrade.RegionSizes[i];
        }

        grade = grade / preparingGrade.RegionSizes.Count;

        //If the grade is equals or bigger than three stars line, then three stars
        if(grade >= threeStars)
        {
            OnGrading?.Invoke(3);
        }

        //If the grade is equals or bigger than two stars line (but below three stars line), then two stars
        else if(grade >= twoStars)
        {
            OnGrading?.Invoke(2);
        }

        //If the grade is equals or bigger than oen star line (but below two stars line), then one star
        else if(grade >= oneStar)
        {
            OnGrading?.Invoke(1);
        }

        //If the grade is below one star line, then no star = fail
        else
        {
            OnGrading?.Invoke(0);
        }
    }
}