using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Player's Boundaries")]
    [SerializeField] private float leftPadding;
    [SerializeField] private float rightPadding;
    [SerializeField] private float topPadding;
    [SerializeField] private float bottomPadding;

    private Vector2 _moveInput;

    Boundary _boundary;

    private void Awake()
    {
        InitBoundaries();
    }

    /// <summary>
    /// Displays boundaries that player can move around
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));

        Vector3 boundTopLeft = new Vector3(bottomLeft.x + leftPadding, topRight.y - topPadding);
        Vector3 boundTopRight = new Vector3(topRight.x - rightPadding, topRight.y - topPadding);

        Vector3 boundBottomLeft = new Vector3(boundTopLeft.x, bottomLeft.y + bottomPadding);
        Vector3 boundBottomRight = new Vector3(boundTopRight.x, bottomLeft.y + bottomPadding);

        Gizmos.color = Color.green;

        //Horizontal Boundaries
        Gizmos.DrawLine(boundTopLeft, boundTopRight);
        Gizmos.DrawLine(boundBottomLeft, boundBottomRight);

        //Vertical Boundaries
        Gizmos.DrawLine(boundTopLeft, boundBottomLeft);
        Gizmos.DrawLine(boundTopRight, boundBottomRight);
    }

    private void Update()
    {
        Move();
    }

    private void InitBoundaries()
    {
        _boundary = Camera.main.GetBoundaries();
    }

    private void Move()
    {
        Vector2 deltaPosition = _moveInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new()
        {
            x = Mathf.Clamp(
                transform.position.x + deltaPosition.x,
                _boundary.Left + leftPadding,
                _boundary.Right - rightPadding),
            y = Mathf.Clamp(
                transform.position.y + deltaPosition.y,
                _boundary.Bottom + bottomPadding,
                _boundary.Top - topPadding),
        };

        transform.position = newPosition;
    }

    private void OnMove(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }
}
