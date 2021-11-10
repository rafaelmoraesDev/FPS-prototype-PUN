using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class ConnectAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI _tmpFeedback;
    [SerializeField] string _roomname;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "v0.1";
        FeedBack("Connectando..");
    }

    public override void OnConnectedToMaster()
    {
        FeedBack("Entrando no Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        FeedBack("Entrando na sala");
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom(_roomname, ro,TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        FeedBack(string.Empty);
    }

    void FeedBack(string message)
    {
        if (!_tmpFeedback) return;
        _tmpFeedback.text = message;
    }

}
