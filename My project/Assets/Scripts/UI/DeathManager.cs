using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField]  TextMeshProUGUI message;
    [SerializeField] float jitter;
    [SerializeField] RectTransform candle;
    Vector2 candleProp;
    float fontsize;

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

        float x = Random.Range(candleProp.x - jitter*2, candleProp.x + jitter*2);
        float y = Random.Range(candleProp.x - jitter, candleProp.x + jitter);
        candle.sizeDelta = new Vector2(x, y);
    }
}
