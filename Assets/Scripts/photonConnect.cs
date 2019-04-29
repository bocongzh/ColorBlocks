using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class photonConnect : MonoBehaviourPunCallbacks
{
    public GameObject sectionView1, sectionView2, sectionView3;
    public menuLogic logic;

    public void connectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();

        

        Debug.Log("Connecting to photon...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");

        sectionView1.SetActive(false);
        sectionView3.SetActive(true);
        //PhotonNetwork.JoinRandomRoom();
    }

    public void chooseFree()
    {
        sectionView3.SetActive(false);
        sectionView2.SetActive(true);
        logic.id = 0;
    }

    public void chooseGoal1()
    {
        sectionView3.SetActive(false);
        sectionView2.SetActive(true);
        logic.id = 1;
    }

    public void chooseGoal2()
    {
        sectionView3.SetActive(false);
        sectionView2.SetActive(true);
        logic.id = 2;
    }

    public void chooseGoal3()
    {
        sectionView3.SetActive(false);
        sectionView2.SetActive(true);
        logic.id = 3;
    }

}
