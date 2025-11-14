using UnityEngine;

public class codigoMoeda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {   
            Debug.Log("Player entered the trigger!");
            Destroy(gameObject);
        }

    }
}