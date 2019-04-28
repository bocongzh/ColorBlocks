using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class menuLogic : MonoBehaviour
{

    public void disableMenuUI()
    {
        //PhotonNetwork.LoadLevel("FreeModel");
        PhotonNetwork.LoadLevel("GoalModel0");
    }
}
