using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    private int levelCounter = 1;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        Debug.Log(SceneManager.sceneCountInBuildSettings);
    }

    public void LoadNextLevel() {
        if (levelCounter >= SceneManager.sceneCountInBuildSettings) {
            // Do game fini    sh
            Debug.Log("Game Finished");
        }
        else {
            SceneManager.LoadScene(levelCounter);
            levelCounter++;
        }
    }
}
