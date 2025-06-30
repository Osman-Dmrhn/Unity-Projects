using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapDraw : MonoBehaviour
{

    public Terrain terrain; 
    public RawImage rawImage; 

    private Texture2D izoHipsTexture;
    public float lowContourInterval = 0.05f; 
    public float highContourInterval = 0.1f; 
    public float heightThreshold = 0.5f; 

    void Start()
    {
        // Terrain yükseklik verisini al
        int width = terrain.terrainData.heightmapResolution;
        int height = terrain.terrainData.heightmapResolution;
        float[,] heights = terrain.terrainData.GetHeights(0, 0, width, height);

        // Min ve max yükseklik deðerlerini bul
        float minHeight = float.MaxValue;
        float maxHeight = float.MinValue;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float heightValue = heights[y, x];
                if (heightValue < minHeight) minHeight = heightValue;
                if (heightValue > maxHeight) maxHeight = heightValue;
            }
        }

        // Normalize height values between 0 and 1
        float heightRange = maxHeight - minHeight;

        // Ýzohips haritasýný oluþtur
        izoHipsTexture = new Texture2D(width, height);
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                bool isContourLine = IsContourLine(heights, x, y, minHeight, heightRange);

                // Kontur çizgisi rengini ayarla
                Color color = isContourLine ? Color.black : Color.white;
                izoHipsTexture.SetPixel(x, y, color);
            }
        }
        izoHipsTexture.Apply();
        //rawImage.texture = izoHipsTexture;
        // Ýzohips haritasýný kaydet
        SaveTextureAsPNG(izoHipsTexture, "IzoHipsHarita.png");
        // Raw Image'a izohips haritasýný ata
        //rawImage.texture = izoHipsTexture;
    }

    bool IsContourLine(float[,] heights, int x, int y, float minHeight, float heightRange)
    {
        float heightValue = (heights[y, x] - minHeight) / heightRange; // Normalize height value

        // Uygun kontur aralýðýný seç
        float contourInterval = heightValue < heightThreshold ? lowContourInterval : highContourInterval;
        float contourLevel = Mathf.Floor(heightValue / contourInterval) * contourInterval;

        // Kenar kontrolü için komþu piksellerin yükseklik farklarýný kontrol et
        return Mathf.Floor((heights[y - 1, x] - minHeight) / heightRange / contourInterval) * contourInterval != contourLevel ||
               Mathf.Floor((heights[y + 1, x] - minHeight) / heightRange / contourInterval) * contourInterval != contourLevel ||
               Mathf.Floor((heights[y, x - 1] - minHeight) / heightRange / contourInterval) * contourInterval != contourLevel ||
               Mathf.Floor((heights[y, x + 1] - minHeight) / heightRange / contourInterval) * contourInterval != contourLevel;
    }

    void SaveTextureAsPNG(Texture2D texture, string fileName)
    {
        byte[] bytes = texture.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(path, bytes);
        Debug.Log("ÝzoHips haritasý kaydedildi: " + path);
    }
}



