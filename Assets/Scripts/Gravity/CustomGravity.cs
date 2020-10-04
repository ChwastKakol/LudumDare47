using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour {
    [SerializeField] private float strength = 1;

    public float Strength => strength;
    public Vector2 Direction => transform.up;
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * strength * .1f);
    }
}
