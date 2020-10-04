using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityObject : MonoBehaviour {
    private Rigidbody2D _rb;

    private bool _verticallyInverted = false, useDefaultGravity = true;
    private bool airBorne = false;
    private Vector2 currentGravityDirection = Vector2.down;
    private int gravityRefCount = 0;
    
    [SerializeField] private LayerMask GravityLayer;
    
    public bool Airborne => airBorne;
    public bool UseDefaultGravity => useDefaultGravity;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (GravityLayer == (GravityLayer | 1 << other.gameObject.layer)) {
            Trigger2DFuntion(other);
            gravityRefCount++;
            _rb.gravityScale = 0;
        }
    }

    public void OnTriggerStay2D(Collider2D other) {
        if (GravityLayer == (GravityLayer | 1 << other.gameObject.layer)) {
            Trigger2DFuntion(other);
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (GravityLayer == (GravityLayer | 1 << other.gameObject.layer)) {
            if (_verticallyInverted) Flip();
            currentGravityDirection = Vector2.down;
            gravityRefCount--;
            if (gravityRefCount == 0) {
                _rb.gravityScale = 1;
            }
        }
    }

    private void Trigger2DFuntion(Collider2D other) {
        
        CustomGravity customGravity = other.GetComponent<CustomGravity>();
        _rb.AddForce(customGravity.Direction * customGravity.Strength);
        currentGravityDirection = customGravity.Direction;
        
        if (airBorne) {
            float dot = Vector2.Dot(customGravity.Direction, _rb.velocity);
            if ((!_verticallyInverted && dot > 0) || (_verticallyInverted && dot < 0)) {
                Flip();
            }
        }
        else {
            float dot = Vector2.Dot(customGravity.Direction, transform.up);
            if ((!_verticallyInverted && dot > 0) || (_verticallyInverted && dot < 0)) {
                Flip();
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

    private void Flip() {
        _verticallyInverted = !_verticallyInverted;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        airBorne = false;
        useDefaultGravity = currentGravityDirection.y < 0;
    }

    private void OnCollisionExit2D(Collision2D other) {
        airBorne = true;
    }
}
