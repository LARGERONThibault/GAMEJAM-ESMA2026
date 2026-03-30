using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    [SerializeField] Transform follow;
    //Délais pour que la position de la caméra soit la position du joueur
    [SerializeField] float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero; 
    private Vector3 offset = new Vector3(0, 0, -10f);

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp (transform.position, follow.position + offset, ref velocity, smoothTime);
    }
}
