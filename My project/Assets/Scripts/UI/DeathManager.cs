using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    [SerializeField]  TextMeshProUGUI message;
    [SerializeField] float jitter;
    [SerializeField] RectTransform candle;
    [SerializeField] Image candlei;
    [SerializeField] List<Sprite> sprite;
    Vector2 candleProp;
    float fontsize;
    float timer;

    private void Start()
    {
        fontsize = message.fontSize;
        candleProp = candle.sizeDelta;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Manoir");
        }

        if (Input.GetMouseButton(1))
        {
            SceneManager.LoadScene("Menu");
        }

        message.fontSize = Random.Range(fontsize - jitter, fontsize + jitter);
        message.characterSpacing = Random.Range(-jitter, jitter);
        message.lineSpacing = Random.Range(-jitter, jitter);

        /*
        float x = Random.Range(candleProp.x - jitter*2, candleProp.x + jitter*2);
        float y = Random.Range(candleProp.x - jitter, candleProp.x + jitter);
        candle.sizeDelta = new Vector2(x, y);
        */

        timer += Time.deltaTime;
        if (timer > 0.5f) 
        { 
            if (candlei.sprite == sprite[0])
            {
                candlei.sprite = sprite[1];
            }

            else if (candlei.sprite == sprite[1])
            {
                candlei.sprite = sprite[2];
            }

            else if (candlei.sprite == sprite[2])
            {
                candlei.sprite = sprite[0];
            }

            timer = 0;
        }
    }
}
