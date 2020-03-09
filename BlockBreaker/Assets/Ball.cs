using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    /* config params */
    [SerializeField] Paddle paddle1;
    [SerializeField] bool hasStarted = false;

    /* Cached component references */
    private AudioSource audioSource;
    private Rigidbody2D myRigidBody2D;

    /* state variables */
    /// <summary>
    /// state -> distance between the paddle and the ball
    /// </summary>
    private Vector2 paddleToBallVector;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y + 0.5f);
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource   = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnStart();
    }

    private void OnStart()
    {
        if (hasStarted)
        {
            return;
        }
        LockToPaddle();
        LauchOnClick();
    }

    public bool HasGameStarted()
    {
        return hasStarted;
    }

    private void LauchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasStarted)
        {
            return;
        }

        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length - 1)];
        audioSource.PlayOneShot(clip);

        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        myRigidBody2D.velocity += velocityTweak;
    }
}
