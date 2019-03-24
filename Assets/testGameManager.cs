using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class testGameManager : MonoBehaviourPun
{


    int num = 0;
    public GameObject player;
    public GameObject body;

    Vector3[] startPositions = { new Vector3(-16f, 9.5f, 0f), new Vector3(9f, 9.5f, 0f), new Vector3(-16f, -9.5f, 0f), new Vector3(9f, -9.5f, 0f) };

    // Start is called before the first frame update
    void Start()
    {
        int playerNum = PhotonNetwork.CurrentRoom.PlayerCount;
        playerNum+=3;
        PhotonNetwork.Instantiate(playerNum+"_head", startPositions[playerNum - 1], Quaternion.identity, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
