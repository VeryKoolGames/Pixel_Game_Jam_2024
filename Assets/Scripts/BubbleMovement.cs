using DG.Tweening;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    private bool moveRight;
    private float time;
    private float length;
    private float delayBeforeUpdate = .7f;

    private void Start()
    {
        SetRandomStartPosition();
        SetInitialDirection();
        transform.localScale = Vector2.zero;
        transform.DOScale(1, 1f);
    }

    void Update()
    {
        delayBeforeUpdate += Time.deltaTime;
        if (delayBeforeUpdate < 1f) return;
        MakeBubbleMoveRightAndLeft();
    }
    
    private void MakeBubbleMoveRightAndLeft()
    {
        float directionMultiplier = moveRight ? 1 : -1;
        transform.position = new Vector2(Mathf.PingPong(Time.time * time, length) * directionMultiplier, transform.position.y);
    }
    
    private void SetRandomStartPosition()
    {
        time = Random.Range(0.2f, 0.4f);
        length = Random.Range(0.2f, 0.4f);
    }

    private void SetInitialDirection()
    {
        moveRight = Random.value > 0.5f;
    }
}