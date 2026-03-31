using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] int minutes;
    [SerializeField] int seconds = 59;
    [SerializeField] TextMeshProUGUI txt;
    void Start()
    {
        txt = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (minutes > 0 || seconds > 0) 
        {
            seconds--;
            if (seconds < 0)
            {
                if (minutes>0) minutes--;
                seconds = 59;
            }
            if (seconds > 9)
            {
                string timetext = "0" + minutes + ":" + seconds;
                txt.text = timetext;
            }
            else
            {
                string timetext = "0" + minutes + ":0" + seconds;
                txt.text = timetext;
            }
                yield return new WaitForSecondsRealtime(1f);
        }
        Debug.Log(seconds);
        SceneManager.LoadScene("WinScene");
    }
}
