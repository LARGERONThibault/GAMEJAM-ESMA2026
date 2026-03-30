using UnityEngine;
using UnityEngine.Events;

public class CandleScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    LightFade playerLight;
    public UnityEvent takeCandle;

    void Start()
    {
        playerLight = player.GetComponentInChildren<LightFade>();
        Debug.Log(playerLight);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E))
            {
            SwitchCandle();
            }
    }

    public void SwitchCandle()
    {
        takeCandle.Invoke();
        Destroy(transform.parent.gameObject);
    }
}
