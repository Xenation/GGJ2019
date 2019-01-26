using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;

public class GetBackground : MonoBehaviour
{
    public GameObject backgroundPrefab;

    const int SPI_GETDESKWALLPAPER = 0x73;
    const int SPIF_UPDATEINIFILE = 0x01;
    const int SPIF_SENDWININICHANGE = 0x02;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    GameObject backgroundGO;
    string currentImagePath;

    // Start is called before the first frame update
    void Start()
    {
        string backgroundPath = new string (' ', 1000);
        SystemParametersInfo(SPI_GETDESKWALLPAPER, 1000, backgroundPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        int lastUsefulIndex = 0;
        for (int i = 0; i < backgroundPath.Length; i++)
        {
            if (backgroundPath[i] == '\0')
            {
                lastUsefulIndex = i;
                break;
            }
        }
        backgroundPath = backgroundPath.Remove(lastUsefulIndex);
        backgroundGO = Instantiate(backgroundPrefab);
        currentImagePath = backgroundPath;
        changeImage(backgroundPath);
    }

    void changeImage(string imagePath)
    {
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture2D.filterMode = FilterMode.Trilinear;
        ImageConversion.LoadImage(texture2D, bytes);
        backgroundGO.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture2D, new Rect(new Vector2(-5f, -5f), new Vector2(1920, 1080)), Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        string backgroundPath = new string(' ', 1000);
        SystemParametersInfo(SPI_GETDESKWALLPAPER, 1000, backgroundPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        int lastUsefulIndex = 0;
        for (int i = 0; i < backgroundPath.Length; i++)
        {
            if (backgroundPath[i] == '\0')
            {
                lastUsefulIndex = i;
                break;
            }
        }
        backgroundPath = backgroundPath.Remove(lastUsefulIndex);
        if (backgroundPath != currentImagePath)
        {
            changeImage(backgroundPath);
        }
    }
}
