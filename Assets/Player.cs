using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Movement Settings")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private bool continuousMovement = false;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationOffset = 0f;
    [SerializeField] private float rotationSpeed = 180f; // degrees per second

    [Header("Scale Settings")]
    [SerializeField] private float scaleSpeed = 1f;
    private float zDirection;

    [Header("References")]
    [SerializeField] private Transform currentWordHolderPosition;

    private Vector3 lastInputVector = Vector3.up;
    private Letter currentLetter;
    private bool canMove = true;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private float targetRotation;
    [SerializeField] SpriteRenderer kiteImage;
    [SerializeField] Sprite[] allKiteImages;
    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // kiteImage.sprite = allKiteImages[PlayerPrefs.GetInt("SelectedKite", 0)];

        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        speed += LevelManager.Instance.GetLevelSpeed();
    }
    private void Update()
    {
        if (!canMove)
        {
            movementInput = Vector2.zero;
            return;
        }

        ReadInput();
        HandleScaling();
    }
    private void FixedUpdate()
    {
        if (movementInput.sqrMagnitude < 0.01f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (!canMove)
            return;

        // If no stick input, don’t rotate or move

        HandleDirectMoveAndRotate();
    }

    private void HandleDirectMoveAndRotate()
    {
        // 1. Compute the desired facing angle from the raw joystick vector
        float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg + rotationOffset;

        // 2. Rotate the Rigidbody2D (and thus the whole GameObject) instantly to that angle
        rb.MoveRotation(angle);

        // 3. Use the player’s own “forward” (its local right vector after rotation) to drive velocity
        Vector2 forward = transform.right;
        rb.linearVelocity = forward * speed;

        // 4. Optionally clamp position so you can’t drift outside your bounds
        //    (you can also clamp by manually setting rb.position if you prefer MovePosition)
        Vector2 clamped = new Vector2(
            Mathf.Clamp(rb.position.x, minX, maxX),
            Mathf.Clamp(rb.position.y, minY, maxY)
        );
        rb.position = clamped;
    }


    private void ReadInput()
    {
        movementInput.x = joystick.Horizontal;
        movementInput.y = joystick.Vertical;

        if (movementInput.sqrMagnitude > 0.01f)
            lastInputVector = movementInput.normalized;

        // Altitude input (for scaling)
        // zDirection set via public methods
    }

    private void HandleScaling()
    {
        if (Mathf.Abs(zDirection) > 0f)
        {
            Vector3 scale = transform.localScale;
            scale += Vector3.one * zDirection * scaleSpeed * Time.deltaTime;
            float a = Mathf.Clamp(scale.x, .5f, 2);
            scale.x = scale.y = scale.z = a;


            transform.localScale = scale;
        }
    }

    private void HandleContinuousMove()
    {
        // Rotate around Z using joystick.x
        float turnInput = joystick.Horizontal;
        float newRot = rb.rotation - turnInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(newRot);

        // Always move forward (right vector)
        Vector2 moveDir = transform.right;
        Vector2 newPos = rb.position + moveDir * speed * Time.fixedDeltaTime;
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        rb.MovePosition(newPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Letter"))
        {
            if (currentLetter == null)
            {
                var letter = collision.GetComponent<Letter>();
                if (letter != null && letter.canBePicked)
                {
                    letter.canBePicked = false;
                    currentLetter = letter;
                    letter.transform.SetParent(currentWordHolderPosition, false);
                    letter.transform.SetPositionAndRotation(
                        currentWordHolderPosition.position,
                        currentWordHolderPosition.rotation);
                }
            }
        }
        else if (collision.CompareTag("Letter Holder") && currentLetter != null)
        {
            var holder = collision.GetComponent<LetterHolder>();
            if (holder != null && holder.SetUp(currentLetter))
            {
                holder.SetLetter(currentLetter);
                currentLetter = null;
            }
        }
    }

    // Public methods for scaling input
    public void DecreaseZ() => zDirection = -1;
    public void IncreaseZ() => zDirection = 1;
    public void Stop() => zDirection = 0;
    public void ResetLetter()
    {
        currentLetter.ResetLetterPosition();
        currentLetter = null;
    }
    // Method to disable movement externally
    public void SetCanMove(bool value) => canMove = value;

    // Optional: method to delete current letter
    public void DeleteLetter()
    {
        if (currentLetter != null)
        {
            Destroy(currentLetter.gameObject);
            currentLetter = null;
        }
    }
}
