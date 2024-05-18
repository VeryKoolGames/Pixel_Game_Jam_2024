using UnityEngine;

public class FollowFish : MonoBehaviour
{
    [SerializeField] private Transform fishTransform; // The transform of the fish to follow
    [SerializeField] private Vector3 offset = new Vector3(0, .2f, 0); // Offset to position the panel above the fish

    private RectTransform rectTransform; // The RectTransform of the panel

    private void Start()
    {
        // Get the RectTransform component of the panel
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Follow the fish's position with an offset
        FollowFishPosition();
    }

    private void FollowFishPosition()
    {
        // Update the RectTransform position to follow the fish with an offset
        rectTransform.position = fishTransform.position + offset;

        // Ensure the panel does not rotate with the fish
        rectTransform.rotation = Quaternion.identity;
    }
}