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
        if (direction.y > 0) myAnimator.SetInteger("Direction", 0);
        else if (direction.y < 0) myAnimator.SetInteger("Direction", 1);
        else if (direction.x < 0) myAnimator.SetInteger("Direction", 2);
        else if (direction.x > 0) myAnimator.SetInteger("Direction", 3);

        if (direction.x == 0 && direction.y == 0) myAnimator.SetInteger("Direction", 0);
    }
}
