using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    PhotonView photonV;
    public static SpawnPlayer Instance { get; private set; }
    private Transform[] spawnPoints;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI tmpCountRespawn;

    private void Awake()
    {
        photonV = GetComponent<PhotonView>();
        Instance = this;
        spawnPoints = transform.GetComponentsInChildren<Transform>();
    }
    public override void OnJoinedRoom()
    {
        Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
        PhotonNetwork.Instantiate(player.name, position, Quaternion.identity, 0);
    }
    public IEnumerator RespawnIE(int id)
    {
        photonV.RPC("InactiveRPC", RpcTarget.All, id);
        for (int a = 3; a > 0; a--)
        {
            if (tmpCountRespawn != null)
                tmpCountRespawn.text = a.ToString();
            yield return new WaitForSeconds(1);
        }
        tmpCountRespawn.text = string.Empty;
        photonV.RPC("RespawnRPC", RpcTarget.All, id, Random.Range(0, spawnPoints.Length - 1));
    }

    [PunRPC]
    public void InactiveRPC(int id)
    {
        Transform tr = PhotonView.Find(id).transform;
        tr.GetComponent<PlayerManager>().InnactivePlayer();
    }

    [PunRPC]
    public void RespawnRPC(int id, int indexPosition)
    {
        Vector3 position = spawnPoints[indexPosition].position;
        Transform tr = PhotonView.Find(id).transform;
        tr.position = position;
        tr.GetComponent<PlayerManager>().ActivePlayer();
    }


}
