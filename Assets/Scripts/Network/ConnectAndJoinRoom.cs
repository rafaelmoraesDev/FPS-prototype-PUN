using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class ConnectAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI tmpFeedback;
    [SerializeField] private string roomName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "v0.1";
        FeedBack("Connectando...");
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
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        FeedBack(string.Empty);
    }

    private void FeedBack(string message)
    {
        if (!tmpFeedback) return;
        tmpFeedback.text = message;
    }

   
}
