using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using DG.Tweening;
using UnityEditor.ShaderGraph.Internal;

public class ButtonEvents : MonoBehaviour
{
    private Color startColor;
    private Image image;
    private RectTransform rectTransform;
    public RectTransform targetRectTransform;
    public RectTransform startRectTransform;
    [SerializeField] private Color hoverColor;
    [SerializeField] private float speed = 0.2f;

    void Start()
    {
        image = GetComponent<Image>();
        startColor = image.color;
        startColor.a = 1;
        rectTransform = GetComponent<RectTransform>();

    }

    public void Hover()
    {
        image.color = hoverColor;

        rectTransform.DOMove(targetRectTransform.position, speed);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.hoverUISound, startRectTransform.position);
    }

    public void Exit()
    {
        image.color = startColor;
        rectTransform.DOMove(startRectTransform.position, speed);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

     public void Credits()
    {
        image.color = hoverColor;

        rectTransform.DOMove(targetRectTransform.position, speed);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.hoverUISound, startRectTransform.position);
    }
    
}
