using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject2 : MonoBehaviour {
    public Vector2 velocity = Vector2.zero;
    public Vector2 postPhysicsVelocity = Vector2.zero;

    public Vector2 boundingBoxPosition, boundingBoxSize;
    
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(boundingBoxPosition.x, boundingBoxPosition.y, 0), boundingBoxSize);
    }
}
