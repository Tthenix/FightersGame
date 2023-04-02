using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador1 : MonoBehaviour
{
private PlayerControls _playerControls = null;

    public float _speed = 5f;
    private Vector2 _direction = Vector2.zero;
    private Rigidbody2D _rigidbody = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _playerControls = new PlayerControls();

        _playerControls.GamePlay.Move.performed += ReadInput;
        _playerControls.GamePlay.Move.canceled += ReadInput;

        _playerControls.GamePlay.Jump.performed += JumpCharacter;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        if (_playerControls != null)
        {
            _playerControls.Disable();
        }
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void ReadInput(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        _direction.x = input.x;
        _direction.y = input.y;
    }

    private void MoveCharacter()
    {
        Vector2 movement = _direction * _speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private void JumpCharacter(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1 && _rigidbody != null && _rigidbody.velocity.magnitude == 0)
        {
            _rigidbody.AddForce(Vector2.up * 500);
        }
    }
}
