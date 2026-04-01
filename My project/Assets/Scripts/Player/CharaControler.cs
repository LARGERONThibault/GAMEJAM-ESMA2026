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

    Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
        childLight = GetComponentInChildren<LightFade>();
    }
    void Update()
    {
        //Récupère la direction du joueur sur les deux axes en prennant en compte la sensibilité pour joysticks.
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX < 0) myAnimator.SetInteger("Direction", 2);
        else myAnimator.SetInteger("Direction", 3);
        if (inputY > 0) myAnimator.SetInteger("Direction", 0);
        else myAnimator.SetInteger("Direction", 1);

        if (inputX == 0 && inputY == 0) myAnimator.SetInteger("Direction", 4);
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
            collision.gameObject.GetComponentInChildren<CandleScript>().SwitchCandle();
        }
    }
}
