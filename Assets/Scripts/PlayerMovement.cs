using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Force stats")]
    [SerializeField] private float verticalBounceForce = 10f;
    [SerializeField] private float horizontalBounceForce = 5f;

    [Header("Interval")]
    [SerializeField] private float bounceInterval = 0.5f;
    private float bounceTimer = 0f;

    private bool gameOver;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounceTimer = bounceInterval;
        VerticalBounce(); // initial bounce
    }

    void Update()
    {
        if (gameOver) return;

        //Bounce interval
        bounceTimer -= Time.deltaTime;

        //Vertical bounce
        if (bounceTimer <= 0f)
        {
            VerticalBounce();
        }

        //Input handling
        HandleInput();
    }

    #region Bounce
    void VerticalBounce()
    {
        rb.linearVelocity = Vector2.zero;
        Vector2 bounceForce = new Vector2(0, verticalBounceForce);
        rb.AddForce(bounceForce, ForceMode2D.Force);
        //Resetting bounce timer
        bounceTimer = bounceInterval;
    }

    void DiagonalBounce(int direction)
    {
        rb.linearVelocity = Vector2.zero;
        Vector2 bounceForce = new Vector2(horizontalBounceForce * direction, verticalBounceForce);
        rb.AddForce(bounceForce, ForceMode2D.Force);
        //Resetting bounce timer
        bounceTimer = bounceInterval;
    }
    #endregion

    #region Input
    void HandleInput()
    {
        //Keyboard inputs
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            //Left bounce
            DiagonalBounce(-1);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            //Right bounce
            DiagonalBounce(1);
        }

        // swipe for mobile devices
#if UNITY_ANDROID || UNITY_IOS
        HandleSwipe();
#endif
    }

    void HandleSwipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 swipe = Input.GetTouch(0).deltaPosition;
            if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
            {
                if (swipe.x > 0)
                    DiagonalBounce(1);
                else if (swipe.x < 0)
                    DiagonalBounce(-1);
            }
        }
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            rb.gravityScale = 0;
            gameOver = true;
            GameManager.Instance.OnGameOver.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.OnCoinsCollected.Invoke();
            collision.gameObject.SetActive(false);
        }
    }
    #endregion

}
