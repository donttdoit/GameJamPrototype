using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ContactFilter2D MovementFilter;
    public float MoveSpeed = 1f;

    private Vector2 _movementInput;
    private Rigidbody2D _rbPlayer;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    //private float _collisionOffset = 0.05f;

    private const string IS_WALKING_DOWN = "IsWalkingDown";

    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        bool canMove = _movementInput != Vector2.zero;

        if (canMove)
        {
            bool successMove = TryMove(_movementInput);

            if (!successMove)
            {
                successMove = TryMove(new Vector2(_movementInput.x, 0));

                if (!successMove)
                {
                    successMove = TryMove(new Vector2(0, _movementInput.y));
                }
            }
            
        }

        // Анимации
        _animator.SetBool(IS_WALKING_DOWN, _movementInput.y < 0);

        if (_movementInput.x < 0)
        {
            _spriteRenderer.flipX = true;
        } else if (_movementInput.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }


    private bool TryMove(Vector2 direction)
    {
        int count = _rbPlayer.Cast(direction, MovementFilter, _castCollisions, MoveSpeed * Time.deltaTime);

        if (count == 0)
        {
            _rbPlayer.MovePosition(_rbPlayer.position + direction * Time.deltaTime * MoveSpeed);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        Debug.Log("Attack");
    }
}
