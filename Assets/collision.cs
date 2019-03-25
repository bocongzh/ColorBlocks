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
        Debug.Log(currentObj + " " + collidObj);
        if (currentObj.Contains("body") && collidObj.Contains("head"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
