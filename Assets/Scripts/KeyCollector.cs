using UnityEngine;
using UnityEngine.UI;

public class KeyCollector : MonoBehaviour
{
    private int keys = 0;

    [SerializeField] private Text keysText;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private GameObject chest;
    [SerializeField] private Finish finishScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectionSoundEffect.Play();
            keys++;
            keysText.text = "Keys: " + keys;
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (keys == 4)
        {
            finishScript.OpenChestAnimation();
            Debug.Log("Chest opened!");
        }
        else
        {
            Debug.Log("Not enough keys to open the chest.");
        }
    }
}