using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RespawnOnDie : MonoBehaviour
{
    private PhotonView photonView;
    
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();        
    }
    private void OnEnable()
    {
        Hp.OnDie += OnDie;
    }
    private void OnDisable()
    {
        Hp.OnDie -= OnDie;
    }

    public void OnDie(int vitime,int assassin)
    {        
        if (vitime == photonView.ViewID && photonView.IsMine)
        {
            GetComponent<PlayerManager>().InnactivePlayer();
            StartCoroutine(SpawnPlayer.Instance.RespawnIE(photonView.ViewID));            
        }
    }


  
}
