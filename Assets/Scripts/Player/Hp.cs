using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class Hp : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField] protected private int currentHp = 90;
    private bool isDie;
    private int maxHp = 90;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void Damage(int damage, int assassin)
    {
        if (!photonView.IsMine)
            photonView.RPC("DamageRPC", RpcTarget.All, damage, (int)photonView.ViewID, assassin);
    }
    public void HpFull()
    {
        currentHp = maxHp;
        isDie = false;
    }


    [PunRPC]
    private void DamageRPC(int damage, int victim, int assassin)
    {
        if (isDie) return;
        currentHp -= damage;
        if (OnDamage != null) OnDamage(victim, assassin);
        if (currentHp <= 0)
        {
            Die(victim, assassin);
        }
    }

    private void Die(int victim, int assassin)
    {
        isDie = true;
        currentHp = 0;
        if (OnDie != null) OnDie(victim, assassin);
    }

    public static Action<int, int> OnDamage;
    public static Action<int, int> OnDie;

}
