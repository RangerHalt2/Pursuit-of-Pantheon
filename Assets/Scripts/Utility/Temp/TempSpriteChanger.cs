using UnityEngine;

public class TempSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    
    private SpriteRenderer spriteRenderer;

    private FollowerStatblock fSB;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fSB = GetComponentInParent<FollowerStatblock>();
    }

    private void Update()
    {
        if(fSB.classID == 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        if(fSB.classID == 1)
        {
            spriteRenderer.sprite = sprites[1];
        }
        if(fSB.classID == 2)
        {
            spriteRenderer.sprite = sprites[2];
        }
    }
}
