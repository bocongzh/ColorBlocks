using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string currentObj = transform.name;
        string collidObj = collision.name;
        int id1 = currentObj.ToCharArray()[0] - '0';
        int id2 = collidObj.ToCharArray()[0] - '0';
        //Debug.Log(currentObj + " " + collidObj);
        Debug.Log("current:" + currentObj);
        Debug.Log("collid:" + collidObj);
        if (collidObj.Contains("head"))
        {
            GameObject[] objects = SceneManager.GetSceneByName("FreeModel").GetRootGameObjects();
            char id = currentObj.ToCharArray()[0];
            if (objects[8].name.ToCharArray()[0] == id)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel("GameOver_Lose");
            }
        } else
        {

        }
       
    }
}
