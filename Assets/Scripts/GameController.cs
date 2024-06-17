using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject gameElements;
    public GameObject uiCamera;

    void Start()
    {
        uiCanvas.SetActive(true);
        gameElements.SetActive(false);
        uiCamera.SetActive(true);
    }

    public void StartTheGame()
    {
        uiCanvas.SetActive(false);
        gameElements.SetActive(true);
    }
}

