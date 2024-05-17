using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    private Color startColor;
    private Image image;
    [SerializeField] private Color hoverColor;

    void Start()
    {
        image = GetComponent<Image>();
        startColor = image.color;
    }

    public void Hover()
    {
        image.color = hoverColor;
    }
    public void Exit()
    {
        image.color = startColor;
    }
    
}
