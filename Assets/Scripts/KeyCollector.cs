// Updated KeyCollector script
using UnityEngine;
using UnityEngine.UI;

public class KeyCollector : MonoBehaviour
{
    private int keys = 0;

    [SerializeField] private Text keysText;
    [SerializeField] private AudioSource collectionSoundEffect;
    public Finish finish;
    public Chest_Item Chest_Item;
    public PlayerLife playerLife;
    public Weapon weapon; // Reference to the Weapon script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectionSoundEffect.Play();
            keys++;
            keysText.text = "Keys: " + keys;
            if (weapon.bulletCount < 3) // Check if the bullet count is less than 3
            {
                weapon.AddBullets(3 - weapon.bulletCount); // Add bullets up to a maximum of 3
            }
            if (keys == 3)
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
