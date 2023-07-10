using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviourPunCallbacks
{
   public void OnClickStart()
   {
        PhotonNetwork.AutomaticallySyncScene=true;
        PhotonNetwork.ConnectUsingSettings();
   }
    public override void OnConnectedToMaster()
    {
        print("connected!");
        SceneManager.LoadScene("Lobby");
    }
}
