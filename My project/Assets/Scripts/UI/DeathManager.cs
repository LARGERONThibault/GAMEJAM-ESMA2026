using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField]  TextMeshProUGUI message;
    [SerializeField] float jitter;
    float fontsize;

    private void Start()
    {
        fontsize = message.fontSize;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Manoir");
        }

        message.fontSize = Random.Range(fontsize - jitter, fontsize + jitter);
        message.characterSpacing = Random.Range(-jitter, jitter);
        message.lineSpacing = Random.Range(-jitter, jitter);
    }
}
