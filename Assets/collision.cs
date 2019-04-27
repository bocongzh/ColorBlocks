using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        if (id1==id2&&currentObj.Contains("body") && collidObj.Contains("head"))
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            PhotonNetwork.LeaveRoom();

        }
    }
}
