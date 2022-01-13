using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public EnvironmentCtrl environment;

    Transform player;
    float playerDeltaX;
    float edgeDelta;

    float edgeX => transform.position.x - edgeDelta;

    private void Start()
    {
        player = Game.current.player.transform;
        playerDeltaX = transform.position.x - player.position.x;

        edgeDelta = GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;

        environment.Init(edgeX);
    }

    void LateUpdate()
    {
        if(!player)
        {
            return;
        }
        var pos = transform.position;
        pos.x = player.position.x + playerDeltaX;
        transform.position = pos;
        environment.UpdateCameraPos(edgeX);
    }
}
