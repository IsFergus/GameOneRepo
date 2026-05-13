using System;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] 
    private float _movementSpeed;

    public float MovementSpeed => _movementSpeed;

    private void Awake()
    {
        if (!TryGetComponent(out _rb))
        {
            Debug.LogWarning(name + ": No Rigidbody2D found");
        }
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out PlayerControls player))
            {
                player.Alive = false;
            }
            Destroy(other.gameObject);
            Time.timeScale = 0;
            Debug.Log("player died");
        }
    }
}
