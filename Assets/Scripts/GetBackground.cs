using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GetBackground : MonoBehaviour
{
    const int SPI_GETDESKWALLPAPER = 0x73;
    const int SPIF_UPDATEINIFILE = 0x01;
    const int SPIF_SENDWININICHANGE = 0x02;

    public Image image;
    public Texture2D defaultBackground;
    public Text datetimeText;
    public Text timeText;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

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
        byte[] bytes = File.ReadAllBytes(backgroundPath);
        Texture2D texture2D = new Texture2D(0, 0, TextureFormat.RGB24, false);
        texture2D.filterMode = FilterMode.Trilinear;
        ImageConversion.LoadImage(texture2D, bytes);
        if (texture2D.width == 8 || texture2D.height == 8)
        {
            image.sprite = Sprite.Create(defaultBackground, new Rect(Vector2.zero, new Vector2(defaultBackground.width, defaultBackground.height)), Vector2.zero);
            float backgroundAspect = (float)defaultBackground.width / (float)defaultBackground.height;
            if (backgroundAspect > Camera.main.aspect)
            {
                image.rectTransform.localScale = new Vector3(backgroundAspect / Camera.main.aspect, 1, 1);
            }
            else
            {
                image.rectTransform.localScale = new Vector3(1, Camera.main.aspect / backgroundAspect, 1);
            }
        }
        else
        {
            image.sprite = Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2(texture2D.width, texture2D.height)), Vector2.zero);
            float backgroundAspect = (float)texture2D.width / (float)texture2D.height;
            if (backgroundAspect > Camera.main.aspect)
            {
                image.rectTransform.localScale = new Vector3(backgroundAspect / Camera.main.aspect, 1, 1);
            }
            else
            {
                image.rectTransform.localScale = new Vector3(1, Camera.main.aspect / backgroundAspect, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        datetimeText.text = System.DateTime.Now.ToShortDateString();
        timeText.text = System.DateTime.Now.ToShortTimeString();
    }
}
