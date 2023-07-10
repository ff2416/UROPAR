using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;
using System.Text;

public class LobbyScene : MonoBehaviourPunCallbacks
{

    private Dictionary<string, RoomInfo> rooms = new Dictionary<string, RoomInfo>();
    [SerializeField]
    TMP_InputField InputRoom;
    [SerializeField]
    TMP_Text RoomList;
    [SerializeField]
    TMP_InputField InputPlayerName;
   
    void Start()
    {

        if(PhotonNetwork.IsConnected==false)
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            if(PhotonNetwork.CurrentLobby==null)
            {
                PhotonNetwork.JoinLobby();
            }
            
        }
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        print("Lobby joined.");
    }
    public void OnClickCreate()
    {
        string roomName=InputRoom.text;
        if(roomName.Length!=0)
        {
            RoomOptions roomOptions=new RoomOptions();
            roomOptions.CleanupCacheOnLeave=false;
            PhotonNetwork.CreateRoom(roomName,roomOptions);
            
        }
        string playerName=InputPlayerName.text;
        
        if(playerName.Length!=0)
        {
            PhotonNetwork.LocalPlayer.NickName=playerName;
        }
    }
    public override void OnJoinedRoom()
    {
        print("Room Joined!");
        SceneManager.LoadScene("RoomScene");
    }
     public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList || roomList[i].PlayerCount == 0)
            {
                if (rooms.ContainsKey(roomList[i].Name))
                {
                    rooms.Remove(roomList[i].Name);
                }
            }
            else
            {
                if (!rooms.ContainsKey(roomList[i].Name))
                {
                    rooms.Add(roomList[i].Name, roomList[i]);
                }
                else
                {
                    rooms[roomList[i].Name] = roomList[i];
                }
            }
        }
        string b="";
        foreach(string a in rooms.Keys)
        {
            b=b+a+"\r\n";   
        }
        RoomList.text=b;
    }
    public void OnClickJoin()
    {
        string roomName=InputRoom.text;
        if(roomName.Length!=0)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        string playerName=InputPlayerName.text;
        if(playerName.Length!=0)
        {
            PhotonNetwork.LocalPlayer.NickName=playerName;
        }
    }
}
