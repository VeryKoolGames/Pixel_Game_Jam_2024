using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    private bool moveRight;

    private void Start()
    {
        SetRandomStartPosition();
        SetInitialDirection();
    }

    void Update()
    {
        MakeBubbleMoveRightAndLeft();
    }
    
    private void MakeBubbleMoveRightAndLeft()
    {
        float directionMultiplier = moveRight ? 1 : -1;
        transform.position = new Vector2(Mathf.PingPong(Time.time * 0.2f, 0.2f) * directionMultiplier, transform.position.y);
    }
    
    private void SetRandomStartPosition()
    {
        transform.position = new Vector2(Random.Range(-0.2f, 0.2f), transform.position.y);
    }

    private void SetInitialDirection()
    {
        moveRight = Random.value > 0.5f;
    }
}