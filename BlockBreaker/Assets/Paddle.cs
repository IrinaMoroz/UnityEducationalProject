using UnityEngine;

public class Paddle : MonoBehaviour
{
    /* config params */
    [SerializeField] float ScreenWidthUnitCount = 16f;
    /// <summary>
    /// since the width of paddle is 2 the min position will 0 + paddleWidth/2
    /// where 0 is the start X position of screen
    /// </summary>
    [SerializeField] float MinX = 1f;
    /// <summary>
    /// ScreenWidthUnitCount - paddleWidth/2
    /// </summary>
    [SerializeField] float MaxX = 15f;

    /* Cached component references */
    private GameSession theGameSession;
    private Ball theBall;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), MinX, MaxX);
        transform.position = paddlePos; // moves to defined position 
    }

    private float GetXPos()
    {
        if (theBall.HasGameStarted() && theGameSession.IsAutoplayEnabled())
        {
            return theBall.transform.position.x;
        }
        var mousePosInUnit = Input.mousePosition.x / Screen.width * ScreenWidthUnitCount;//the mouse position for x in pixels
        return mousePosInUnit;
    }
}
