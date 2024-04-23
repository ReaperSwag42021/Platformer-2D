using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest_Item : MonoBehaviour
{

    [SerializeField] private Animator myAnimationController;

    private bool levelCompleted = false;

    private void Start()
    {
        myAnimationController = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            myAnimationController.SetTrigger("PlayAnimChestItem");
            myAnimationController.SetTrigger("PlayAnimChestItem(Loop)");
            levelCompleted = true;
            Invoke("CompleteLevel", 8f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}