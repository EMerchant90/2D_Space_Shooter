using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    private UIManager _uIManager;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    //if game over is true
    //if space key pressed
    //spawn player
    //game over is false
    //hide title screen

    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
                gameOver = false;
                _uIManager.HideTitleScreen();
            }
        }
    }


}
