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
        //Debug.Log("current:" + currentObj);
        //Debug.Log("collid:" + collidObj);
        if (collidObj.Contains("head"))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
            int index = 0;
            if (sceneName.Contains("FreeModel"))
            {
                index = 8;
            } else if (sceneName.Contains("GoalModel0"))
            {
                index = 13;
            } else if (sceneName.Contains("GoalModel1"))
            {
                index = 15;
            } else if (sceneName.Contains("GoalModel2"))
            {
                index = 25;
            }
            char id = currentObj.ToCharArray()[0];
            if (objects[index].name.ToCharArray()[0] == id)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel("GameOver_Lose");
            }
        } else
        {

        }
       
    }
}
