using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Executado quando outro collider entra no trigger.
        // "other" é o collider do objeto que entrou.

        // Verifica se o objeto que entrou tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            ReloadCurrentScene();
        }
    }

    private void ReloadCurrentScene()
    {
        // Recarrega a cena atual pelo nome
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Alternativa (pelo índice da cena):
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}