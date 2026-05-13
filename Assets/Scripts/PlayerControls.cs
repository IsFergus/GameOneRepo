using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Non-Basic
    private Rigidbody2D  _rigidbody2D;
    private InputActions _playerController;
    private BoxCollider2D  _boxCollider;
    
    //Basic
    [SerializeField] private float jumpForce;
    private bool _grounded;
    private bool _alive;

    //Public Getter&Setters
    public bool Alive
    {
        get => _alive;
        set => _alive = value;
    }
    
    //Events
    public event Action onDeath;

    private void Awake()
    {
        if (TryGetComponent(out _playerController))
        {
            _playerController.UpOrDown += HandleUpOrDown;
        }

        if (!TryGetComponent(out _rigidbody2D))
        {
            Debug.LogWarning("No rigidbody found on " + name);
        }

        if (!TryGetComponent(out _boxCollider))
        {
            Debug.LogWarning("No box collider found on " + name);
        }

        _grounded = true;
    }
    
    private void HandleUpOrDown(float value)
    {
        //If player input is not up or down, do nothing.
        if (value == 0)
            return;
        //If input is up, pulse upwards.
        if (value == 1 && _grounded)
        {
            _rigidbody2D.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
        
        //If input is down, print crouch.
        else if (value == -1 && _grounded) { }
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _grounded = true;
    }

    private void OnCollisionExit2D(Collision2D obj)
    {
        _grounded = false;
    }

    public void Death()
    {
        onDeath?.Invoke();
    }
}
