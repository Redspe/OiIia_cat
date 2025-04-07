using UnityEngine;
using UnityEngine.SceneManagement;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            Debug.Log("Morreu!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
