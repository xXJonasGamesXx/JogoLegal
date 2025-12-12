using UnityEngine;

public class codigoMoeda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManeger.Instance != null)
            {
                CoinManeger.Instance.AddCoins(1);
            }
            
            Destroy(gameObject);
        }
    }
}