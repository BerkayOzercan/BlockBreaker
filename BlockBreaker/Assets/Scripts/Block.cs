using UnityEngine;

public class Block : MonoBehaviour
{
    //Audio Source Config
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //Chached referance
    Level level;

    //State Variable
    [SerializeField] int timesHit; // TO DO Only Serialized DeBug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
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
        int SpriteIndex = timesHit - 1;
        if (hitSprites[SpriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[SpriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing" + gameObject.name);
        }
        
    }

    private void DestroyBlock()
    {
        PlayBlockSoundSFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockSoundSFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
