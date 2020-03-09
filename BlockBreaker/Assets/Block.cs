using UnityEngine;

public class Block : MonoBehaviour
{
    /* config params */
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSpites;

    /* Cached component references */
    private Level level;
    private GameSession gameStatus;

    /* state variables */
    [SerializeField] int timesHit; //serialized for debugging

    private float resizeFactor = 0.8f; //in percent to resize hit object
    private bool resized = false;

    private const string BreakableTageName = "Breakable";
    private const string UnbreakableTagName = "Unbreakable";

    private void Start()
    {
        level = FindObjectOfType<Level>(); //initializes level
        if (level == null) throw new InitializationComponentException(nameof(Level));

        if (tag == BreakableTageName)
        {
            level.CountBlocks();
        }

        gameStatus = FindObjectOfType<GameSession>();
        if (gameStatus == null) throw new InitializationComponentException(nameof(GameSession));
    }

    private void OnCollisionEnter2D(Collision2D collision)//what object trigered collision
    {
        if (tag == UnbreakableTagName)
        {
            return;
        }

        var maxHits = hitSpites.Length + 1;
        if (++timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        if (!resized)
        {
            this.transform.localScale = new Vector3(transform.localScale.x * resizeFactor, transform.localScale.y * resizeFactor);
            resized = true;
        }

        var spriteIndex = timesHit - 1;
        if(hitSpites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSpites[spriteIndex];

        }
        else
        {
            Debug.Log("block missing from array");
        }

    }

    private void DestroyBlock()
    {
        PlayBlockDestorySFX();
        Destroy(gameObject);//can destroy gameobject, asset, sprite        
        level.RemoveBreakableBlocks();
        TriggerSparclesVFX();
    }

    /// <summary>
    /// PlayBlockDestory sound effect
    /// </summary>
    private void PlayBlockDestorySFX()
    {
        gameStatus.AddToScore();

        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
    }

    private void TriggerSparclesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);//destroy sparkles after 1 so we don't create many unessasary objects in hierarchy
    }
}
