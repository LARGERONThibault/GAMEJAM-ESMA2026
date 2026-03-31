using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Manoir");
        }
    }
}
