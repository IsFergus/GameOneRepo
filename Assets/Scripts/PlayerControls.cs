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
    private Vector3 _halfSize;
    private Vector3 _defSize;
    private float _controlValue;
    private bool _crouched;

    //Public Getter&Setters
    public bool Alive
    {
        get => _alive;
        set => _alive = value;
    }
    
    public delegate void DeathDelegate();
    public static event DeathDelegate onPlayerDied;
    
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
        _defSize =  _boxCollider.size;
        _halfSize = new Vector2(_boxCollider.size.x, _boxCollider.size.y * .5f);
        _grounded = true;
    }
    
    private void HandleUpOrDown(float value)
    {
        _controlValue = value;
        
        //If input is up, pulse upwards.
        if (_controlValue == 1 && _grounded && !_crouched)
        {
            _rigidbody2D.AddForceY(jumpForce, ForceMode2D.Impulse);
            Debug.Log("1");

        }
        
        //If player input is not up or down, do nothing.
        if (_controlValue == 0)
        {
            _boxCollider.size =  _defSize;
            _crouched = false;
            Debug.Log("0");
        }
        
        //If input is down, print crouch.
        else if (_controlValue == -1 && _grounded)
        {
            _boxCollider.size = _halfSize;
            _crouched = true;
            Debug.Log("-1");

        }
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _grounded = true;
            if (_controlValue == -1)
            {
                _boxCollider.size = _halfSize;
                _crouched = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D obj)
    {
        _grounded = false;
    }

    public void Death()
    {
        onPlayerDied?.Invoke();
    }
    
}
