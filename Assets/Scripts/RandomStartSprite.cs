using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomStartSprite : MonoBehaviour
{
    public Sprite[] sprites;
    void Start()
    {
        SpriteRenderer thisSR = gameObject.GetComponent<SpriteRenderer>();
        thisSR.sprite = sprites[Random.Range(0, sprites.Length)];
    }

}
