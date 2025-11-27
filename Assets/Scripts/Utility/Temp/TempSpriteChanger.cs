using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    
    private SpriteRenderer spriteRenderer;
    private Image spriteAlt;

    private FollowerStatblock fSB;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fSB = GetComponentInParent<FollowerStatblock>();
        spriteAlt = GetComponent<Image>();
    }

    private void Update()
    {
        int classID = 0;
        if (fSB == null)
        {
            FollowerPrefabController controller = GetComponentInParent<FollowerPrefabController>();
            classID = controller.followerData.classID;
        }
        else
            classID = fSB.classID;
        if(spriteRenderer != null)
            spriteRenderer.sprite = sprites[classID];
        if(spriteAlt != null)
            spriteAlt.sprite = sprites[classID];
    }
}
