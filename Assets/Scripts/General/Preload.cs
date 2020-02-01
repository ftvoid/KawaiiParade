using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
