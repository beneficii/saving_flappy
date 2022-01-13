using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Game.current.player?.OnTap();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Game.current.ResetGame();
        }
    }
}
