using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleColisionHandler : MonoBehaviour
{
    [SerializeField] private OnBubbleKill onBubbleKill;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            other.GetComponent<FishReproductionManager>().OnBubblePop();
            onBubbleKill.Raise(gameObject);
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.bubblePop, transform.position);
        }
        else if (other.CompareTag("Limit"))
        {
            onBubbleKill.Raise(gameObject);
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.bubblePop, transform.position);
        }
    }
}
