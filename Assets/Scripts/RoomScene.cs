using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;


public class RoomScene : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_Text textRoomName;
    [SerializeField]
    TMP_Text Playerlist;
    [SerializeField]
    Button StartGame;

    void Start()
    {
        if(PhotonNetwork.CurrentRoom==null)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            textRoomName.text="Room Name: "+PhotonNetwork.CurrentRoom.Name;
        }
        UpdatePlayerList();
        StartGame.interactable=PhotonNetwork.IsMasterClient;
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGame.interactable=PhotonNetwork.IsMasterClient;
    }
 
   public void UpdatePlayerList()
   {
        string namelist="";
        foreach(var k in PhotonNetwork.CurrentRoom.Players)
        {
            namelist=namelist+k.Value.NickName+"\r\n";
        }
        Playerlist.text=namelist;
   }
   public override void OnPlayerEnteredRoom(Player newPlayer)
   {
        UpdatePlayerList();
   }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
    public void OnClickStartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
