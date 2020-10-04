using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoors : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        Debug.Log("Collision");
        if (controller != null) {
            FindObjectOfType<GameManager>().LoadNextLevel();
        }
    } 
}
