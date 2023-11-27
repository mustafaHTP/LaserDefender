using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Player's Boundaries")]
    [SerializeField] private float horizontalPadding;
    [SerializeField] private float verticalPadding;

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
        float cubeHeight = Mathf.Abs(
            (_boundary.Top - verticalPadding) - (_boundary.Bottom + verticalPadding));
        float cubeWidth = Mathf.Abs(
            (_boundary.Right - horizontalPadding) - (_boundary.Left + horizontalPadding));

        Vector3 camPosition = Camera.main.transform.position;
        camPosition.z = 0f;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(camPosition, new Vector3(cubeWidth, cubeHeight));
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
                _boundary.Left + horizontalPadding,
                _boundary.Right - horizontalPadding),
            y = Mathf.Clamp(
                transform.position.y + deltaPosition.y,
                _boundary.Bottom + verticalPadding,
                _boundary.Top - verticalPadding),
        };

        transform.position = newPosition;
    }

    private void OnMove(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }
}
