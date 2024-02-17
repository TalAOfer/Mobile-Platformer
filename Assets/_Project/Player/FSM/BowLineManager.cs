using UnityEngine;

public class BowLineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private bool clampLength;
    [SerializeField] private float maxLength;
    [SerializeField] private Player player;
    private Vector3 temp;

    private void Awake()
    {
        // Get the LineRenderer attached to the GameObject
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        // Set the number of line segments (2 for a simple line)
        lineRenderer.positionCount = 2;
        player = GetComponentInParent<Player>();
    }

    public void EnableDraw(bool enable) => lineRenderer.enabled = enable;

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            DrawLine();
        }
    }

    private void DrawLine()
    {
        Vector3 start = transform.position;
        float aimingAngle = player.playerInput.AimingAngle;
        Vector3 endPosition = GetEndPoint(start, aimingAngle, maxLength);

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, endPosition);
    }



    private Vector3 GetEndPoint(Vector3 start, float AimingAngle, float maxLength)
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0; // Adjust for 2D setup

        // Calculate the distance from start to mouse position
        float distanceToMouse = Vector3.Distance(start, mousePositionInWorld);

        // If the distance to the mouse is less than the maxLength, use the mouse position; otherwise, use maxLength
        float length = maxLength;
        if (clampLength) length = Mathf.Min(distanceToMouse, maxLength);

        // Convert AimingAngle from degrees to radians
        float angleInRadians = AimingAngle * Mathf.Deg2Rad;

        // Calculate the aiming direction based on the angle
        temp.x = Mathf.Cos(angleInRadians);
        temp.y = Mathf.Sin(angleInRadians);
        temp.z = 0;
        Vector3 aimingDirection = temp;

        // Calculate the end point using the aiming direction and the clamped length
        Vector3 endPosition = start + aimingDirection * length;
        return endPosition;
    }
}