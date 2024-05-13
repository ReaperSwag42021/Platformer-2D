using UnityEngine;
using UnityEngine.UI;

// KeyCollector script
public class KeyCollector : MonoBehaviour
{
    private int keys = 0;

    [SerializeField] private Text keysText;
    [SerializeField] private AudioSource collectionSoundEffect;
    public Finish finish;
    public Chest_Item Chest_Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectionSoundEffect.Play();
            keys++;
            keysText.text = "Keys: " + keys;
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        Debug.Log("OpenChest called. Finish is null: " + (finish == null) + ", Chest_Item is null: " + (Chest_Item == null));
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