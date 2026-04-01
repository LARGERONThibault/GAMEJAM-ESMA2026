using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string scenename;
    [SerializeField] bool isstart;
    [SerializeField] float overmult;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isstart) SceneManager.LoadScene(scenename);
        //else { Application.Quit(); EditorApplication.ExitPlaymode(); }
    }
}
