using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taskbar : MonoBehaviour
{
    public GameObject blueBarEdge;
    public GameObject blueBarFolder;
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

        blueBarEdge.SetActive(false);
        blueBarFolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            set_blue_bar_under_edge();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            set_blue_bar_under_folder();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            hide_blue_bar();
        }
    }

    public void set_blue_bar_under_edge()
    {
        blueBarEdge.SetActive(true);
        blueBarFolder.SetActive(false);
    }

    public void set_blue_bar_under_folder()
    {
        blueBarEdge.SetActive(false);
        blueBarFolder.SetActive(true);
    }

    public void hide_blue_bar()
    {
        blueBarEdge.SetActive(false);
        blueBarFolder.SetActive(false);
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
