using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taskbar : MonoBehaviour
{
    public Texture2D normalTexture;
    public Texture2D errorTexture;
    Sprite normalSprite;
    Sprite errorSprite;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        normalSprite = Sprite.Create(normalTexture, new Rect (Vector2.zero, new Vector2 (normalTexture.width, normalTexture.height)), Vector2.zero);
        errorSprite = Sprite.Create(errorTexture, new Rect(Vector2.zero, new Vector2(errorTexture.width, errorTexture.height)), Vector2.zero);
        image = GetComponent<Image>();

        set_normal_texture_to_taskbar();
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    public void set_normal_texture_to_taskbar()
    {
        image.sprite = normalSprite;
    }

    public void set_error_texture_to_taskbar()
    {
        image.sprite = errorSprite;
    }
}
