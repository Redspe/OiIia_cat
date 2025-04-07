using UnityEngine;
using UnityEngine.SceneManagement;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Morreu!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
