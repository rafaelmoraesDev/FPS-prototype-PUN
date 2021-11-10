using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerPhotonManager : MonoBehaviour
{

    private PhotonView photonView;
    [SerializeField] private Component[] destroyComponentIfNotMine;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        IsMineCheck();
    }
    void IsMineCheck()
    {
        if (photonView.IsMine) return;
        foreach (Component i in destroyComponentIfNotMine) Destroy(i);
    }

}
