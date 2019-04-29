using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class menuLogic : MonoBehaviour
{

    public int id;

    public void disableMenuUI()
    {
        switch (id)
        {
            case 0: PhotonNetwork.LoadLevel("FreeModel");break;
            case 1: PhotonNetwork.LoadLevel("GoalModel0");break;
        }
    }
}
