using UnityEngine;
using UnityEngine.UI;

public class KeyCollector : MonoBehaviour
{
    private int keys = 0;

    [SerializeField] private Text keysText;
    [SerializeField] private AudioSource collectionSoundEffect;
    public Finish finish;
    public Chest_Item Chest_Item;
    public PlayerLife playerLife; // Add this line

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectionSoundEffect.Play();
            keys++;
            keysText.text = "Keys: " + keys;
            if (keys == 3) // Add these lines
            {
                playerLife.BecomeInvincible();
            }
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (keys == 4)
        {
            finish.OpenChestAndCompleteLevel();
            Chest_Item.ChestItemAnimation();
            Debug.Log("Chest opened!");
        }
        else
        {
            Debug.Log("Not enough keys to open the chest.");
        }
    }
}
