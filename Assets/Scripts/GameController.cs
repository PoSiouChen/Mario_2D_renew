using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject gameElements;

    void Start()
    {
        uiCanvas.SetActive(true);
        gameElements.SetActive(false);
    }

    public void StartTheGame()
    {
        uiCanvas.SetActive(false);
        gameElements.SetActive(true);
    }
}

