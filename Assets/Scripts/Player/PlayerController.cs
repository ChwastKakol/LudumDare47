using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    private Rigidbody2D _rb;
    private GravityObject _gravityObject;
    private Animator _animator;
    private PlayerSoundController _playerSoundController;
    
    [SerializeField] private float jumpStrength;
    [SerializeField] private float speed;
    [SerializeField] [Range(float.Epsilon, 1)] private float acceleration;
    [SerializeField] [Range(float.Epsilon, 1)] private float airborneAcceleration;

    private bool _inverse = false;
    private bool _airborne = false;
    private int _jumpCount = 0, _jumpCountLimit = 2;

    private bool wasWalking = false, isWalking = false;
    
    public bool Airborne => _airborne;

    //private Vector2 targetVelocity;
    private float currentHorizontalSpeed = 0, previousHorizontalSpeed = 0;
    
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _gravityObject = GetComponent<GravityObject>();
        _animator = GetComponent<Animator>();
        _playerSoundController = GetComponent<PlayerSoundController>();
    }

    private void Update() {
        Vector2 velocity = _rb.velocity;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        bool jump = Input.GetButtonDown("Jump");

        currentHorizontalSpeed = Mathf.Lerp(velocity.x, input.x * speed, _airborne ? airborneAcceleration : acceleration);

        Vector2 targetVelocity = new Vector2(currentHorizontalSpeed,  velocity.y);
        
        if (jump && _jumpCount < _jumpCountLimit) {
            _animator.SetTrigger("Jump");
            targetVelocity += Vector2.up * (jumpStrength * (_gravityObject.UseDefaultGravity ? 1 : -1));
            _jumpCount++;
        }

        if ((targetVelocity.x > .1f && _inverse) || (targetVelocity.x < -.1f && !_inverse)) {
            Flip();
        }

        if (!_airborne) {
            wasWalking = isWalking;
            isWalking = Mathf.Abs(targetVelocity.x) > .1f;
            _animator.SetBool("IsRunning", isWalking);
            if (wasWalking && !isWalking) {
                _playerSoundController.StopWalking();
            }
            else if(isWalking && !wasWalking) {
                _playerSoundController.StartWalking();
            }
        }
        else {
            _animator.SetBool("IsRunning", false);
            if (wasWalking || isWalking) {
                isWalking = wasWalking = false;
                _playerSoundController.StopWalking();
            }
        }

        _rb.velocity = targetVelocity;
    }

    

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _inverse = !_inverse;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        _jumpCount = 0;
        _airborne = false;
    }

    private void OnCollisionExit2D(Collision2D other) {
        _airborne = true;
    }
    
}
