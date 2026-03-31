using UnityEngine;
using UnityEngine.UI;

public class CandleUI : MonoBehaviour
{
    [SerializeField] LightFade reference;
    Image self;
    RectTransform rtransform;
    [SerializeField] Sprite sp1;
    [SerializeField] Vector2 ref1;
    [SerializeField] Sprite sp2;
    [SerializeField] Vector2 ref2;
    [SerializeField] Sprite sp3;
    [SerializeField] Vector2 ref3;
    [SerializeField] Sprite sp4;
    [SerializeField] Vector2 ref4;
    [SerializeField] Sprite sp5;
    [SerializeField] Vector2 ref5;

    private void Start()
    {
        self = GetComponent<Image>();
        rtransform = GetComponent<RectTransform>();
    }

    void SetSprite(Sprite sprite, Vector2 reference)
    {
        self.sprite = sprite;
        rtransform.sizeDelta = reference;
    }

    private void Update()
    {
        var progression = reference.currentTime / reference.fadeDuration * 100;
        if (progression > 80) SetSprite(sp5,ref5);
        else if (progression > 60) SetSprite(sp4,ref4);
        else if (progression > 40) SetSprite(sp3,ref3);
        else if (progression > 20) SetSprite(sp2, ref2);
        else SetSprite(sp1, ref1);
    }
}
