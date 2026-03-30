using UnityEngine;

public class LightManager : MonoBehaviour
{
    Vector3 rotationAxis = new Vector3(0f, 0f, 1f);

    //Variables qui gèrent le smooth.
    [Tooltip("In degree / second.")]
     [SerializeField] float rotationSpeed = 360f;
     [SerializeField] bool smooth = false;

    //Input joueur.
    float inputX;
    float inputY;

    void Update()
    {
        //Récupère les inputs du joueur.
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");


        //Définit la direction.
        //Haut.
        if (inputY > 0f)
            SetAngle(0f);

        //Bas.
        else if (inputY < 0f)
            SetAngle(180f);

        //Gauche.
        else if (inputX < 0f)
            SetAngle(90f);
        
        //Droite.
        else if (inputX > 0f)
            SetAngle(270f);
    }
    private void SetAngle(float angle)
    {
        if (smooth)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, rotationAxis.normalized), rotationSpeed * Time.deltaTime * 3);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle, rotationAxis.normalized);
        }
    }
    
}