using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    [SerializeField]ShadowsManager manager;
    Animator myAnimator;

    private void Start()
    {
        manager = GetComponentInParent<ShadowsManager>();
        myAnimator = GetComponent<Animator>();
    }
        private void Update()
    {
        Vector2 direction = manager.direction;
        if (direction.x == 0 && direction.y == 0)
        {
            myAnimator.SetInteger("Direction", 4); 
        }
        else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                myAnimator.SetInteger("Direction", 3); 
            else
                myAnimator.SetInteger("Direction", 2);
        }
        else
        {
            if (direction.y > 0)
                myAnimator.SetInteger("Direction", 0); 
            else
                myAnimator.SetInteger("Direction", 1); 
        }
    }

        
}
