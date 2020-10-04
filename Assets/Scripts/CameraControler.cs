using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {
    public Transform target;
    [SerializeField] [Range(0f, 1)] private float CameraSmoothnes;
    
    private Vector3 _offset;
    
    private void Awake() {
        _offset = transform.position - target.position;
    }

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, target.position + _offset, CameraSmoothnes);
    }
}
