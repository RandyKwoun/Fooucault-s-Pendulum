using UnityEngine;
using System.Collections.Generic;

public class PreparingGrade : MonoBehaviour
{
    [Header("Textures")]
    [SerializeField] private Texture2D outlineTexture;

    [Header("Region Settings")]
    [SerializeField] private int outlineAlphaThreshold = 100;
    [SerializeField] private int minRegionSize = 20;

    //Changable Variables
    private int width;
    private int height;
    private int[] regionId;
    private List<int> regionSizes;

    //Read-only Variables
    public int Width => width;
    public int Height => height;
    public int[] RegionId => regionId;
    public IReadOnlyList<int> RegionSizes => regionSizes;

    void Start()
    {
        width = outlineTexture.width;
        height = outlineTexture.height;
        Color32[] pixels = outlineTexture.GetPixels32();

        BuildRegions(pixels);
    }

    //Returns if the color is outline
    private bool IsOutline(Color32 c)
    {
        return c.a > outlineAlphaThreshold;
    }

    //Seprating regions by outline
    private void BuildRegions(Color32[] pixels)
    {
        regionId = new int[width * height];
        regionSizes = new List<int>();

        //Filling -1 into regionId
        System.Array.Fill(regionId, -1);

        for (int i = 0; i < pixels.Length; i++)
        {
            //The pixel is outline
            if(IsOutline(pixels[i]))
            {
                continue;
            }

            //We already checked this pixel
            if(regionId[i] != -1)
            {
                continue;
            }

            //Found a new pixel
            int newId = regionSizes.Count;
            int size = FloodFill(i, newId, pixels);

            //Add only valid regions.
            if(size >= 0)
            {
                regionSizes.Add(size);
            }
        }
    }

    //Using BFS to separate
    private int FloodFill(int start, int id, Color32[] pixels)
    {
        Queue<int> queue = new Queue<int>();
        List<int> curSave = new List<int>();
        queue.Enqueue(start);
        regionId[start] = id;
        int count = 0;

        int invalidValue = -2;
        bool isValid = true;


        //Right, Down, Left, Up
        int[] dirx = new int[] {1, 0, -1, 0};
        int[] diry = new int[] {0, -1, 0, 1};
        int[] curdir = new int[] {1, -width, -1, width};

        while (queue.Count > 0)
        {
            int cur = queue.Dequeue();
            curSave.Add(cur);
            count++;

            int x = cur % width;
            int y = cur / width;

            //This region touches the image border which the valid region should NOT
            if(x == width - 1 || x == 0 || y == height - 1 || y == 0)
            {
                isValid = false;
            }

            //Checking cur's neighborhood
            for(int i = 0; i < 4; i++)
            {
                int nx = x + dirx[i];
                int ny = y + diry[i];
                int neighbor = cur + curdir[i];

                //This pixel is out of the image
                if(nx < 0 || nx >= width || ny < 0 || ny >= height)
                {
                    continue;
                }

                //This pixel is an outline
                else if(IsOutline(pixels[neighbor]))
                {
                    continue;
                }

                //This pixel is already checked
                else if(regionId[neighbor] != -1)
                {
                    continue;
                }

                regionId[neighbor] = id;
                queue.Enqueue(neighbor);
            }
        }

        //If the counted region is touched with the image border
        //or too small, then don't count the current region.
        if(!isValid || count <= minRegionSize)
        {
            for(int i = 0; i < count; i++)
            {
                regionId[curSave[i]] = invalidValue;
            }

            return invalidValue;
        }

        return count;
    }

}