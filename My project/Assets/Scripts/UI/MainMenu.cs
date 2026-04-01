using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] List<Sprite> sprite;
    float timer;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            if (menu.sprite == sprite[0])
            {
                menu.sprite = sprite[1];
            }

            else if (menu.sprite == sprite[1])
            {
                menu.sprite = sprite[2];
            }

            else if (menu.sprite == sprite[2])
            {
                menu.sprite = sprite[0];
            }

            timer = 0;
        }
    }
}
