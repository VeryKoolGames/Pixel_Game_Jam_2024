using UnityEngine;
using DG.Tweening;

public class LarryMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float swimSpeed = 1f; // Speed of the fish movement
    [SerializeField] private float swimFrequency = 1f; // Frequency of the sinusoidal swim pattern
    [SerializeField] private float swimAmplitude = 0.5f; // Amplitude of the sinusoidal swim pattern
    [SerializeField] private float turnDuration = 0.5f; // Duration of the turning animation

    [Header("Boundary Settings")]
    [SerializeField] private Collider2D swimZone; // The zone in which the fish should stay

    private Vector2 _targetPosition;

    private void Start()
    {
        SetRandomTargetPosition();
        MoveToTarget();
    }

    private void SetRandomTargetPosition()
    {
        Bounds bounds = swimZone.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        _targetPosition = new Vector2(x, y);
    }
    
    public void RotateToPoint(Vector2 targetPoint)
    {
        Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.DORotate(new Vector3(0, 0, angle), turnDuration);
    }

    private void MoveToTarget()
    {
        // Calculate the movement duration based on distance and swim speed
        float distance = Vector2.Distance(transform.position, _targetPosition);
        float duration = distance / swimSpeed;

        Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RotateToPoint(_targetPosition);

        transform.DOMove(_targetPosition, duration).OnComplete(() =>
        {
            SetRandomTargetPosition();
            MoveToTarget();
        });

        DOTween.To(() => 0f, x =>
        {
            Vector3 offset = transform.right * Mathf.Sin(x * swimFrequency) * swimAmplitude; // Use transform.right for horizontal sinusoidal movement
            transform.position += offset * Time.deltaTime;
        }, duration, duration).SetEase(Ease.Linear);
    }
    
}
