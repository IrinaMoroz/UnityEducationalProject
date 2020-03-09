using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    /* config params */
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoplayEnabled;

    /* state variables */
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length; //how many gamestatus object there are
        if (gameStatusCount == 0)
        {
            return;
        }
        if (gameStatusCount > 1)
        {
            DestroyGameObject();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        PrintScore();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

    }

    public void AddToScore()
    {
        currentScore += pointPerBlockDestroyed;
        PrintScore();
    }
    
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoplayEnabled()
    {
        return isAutoplayEnabled;
    }

    private void PrintScore()
    {
        scoreText.text = currentScore.ToString();
    }
    private void DestroyGameObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
