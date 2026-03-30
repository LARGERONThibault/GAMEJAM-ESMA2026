using UnityEngine;

public class CharaControler : MonoBehaviour
{
    [Header("Speed variable")]
    [SerializeField] float speed = 3f;
    float inputX;
    float inputY;

    //Rigidbody du joueur.
    Rigidbody2D rb;
    LightFade childLight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        childLight = GetComponentInChildren<LightFade>();
    }
    void Update()
    {
        //Récupère la direction du joueur sur les deux axes en prennant en compte la sensibilité pour joysticks.
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //Attribue à la vélocité du rigidboy le sens de déplacement du joueur * la vitesse.
        var v = rb.linearVelocity;
        v.x = inputX * speed;
        v.y = inputY * speed;
        rb.linearVelocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Candle"))
        {
            childLight.ResetLight();
            Destroy(collision.gameObject);
        }
    }
}
