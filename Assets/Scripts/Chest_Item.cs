using UnityEngine;
using UnityEngine.SceneManagement;

// Chest_Item script
public class Chest_Item : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void Start()
    {
        myAnimationController = GetComponent<Animator>();
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
