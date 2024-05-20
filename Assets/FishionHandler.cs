using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishionHandler : MonoBehaviour
{
    [SerializeField] private Sprite flashSprite;
    private Sprite normalSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        normalSprite = spriteRenderer.sprite;
        StartCoroutine(SwitchSprite());
    }

    private IEnumerator SwitchSprite()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            spriteRenderer.sprite = flashSprite;
            yield return new WaitForSeconds(.2f);
            spriteRenderer.sprite = normalSprite;
        }
    }
}
