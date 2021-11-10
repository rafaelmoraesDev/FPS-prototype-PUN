using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class Hp : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField] protected private float currentHp = 90;
    private bool isDie;
    private float maxHp = 90;
    private float emptyHp = 0;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {

    }
    public void Damage(int damage, int assassin)
    {
        if (!photonView.IsMine)
            photonView.RPC("DamageRPC", RpcTarget.All, damage, (int)photonView.ViewID, assassin);

    }
    public void HpFull()
    {
        currentHp = maxHp;
        isDie = !isDie;
    }

    [PunRPC]
    private void DamageRPC(int damage, int victim, int assassin)
    {
        if (isDie) return;
        currentHp -= damage;
        if (OnDamage != null) OnDamage(victim, assassin);
        if (currentHp <= emptyHp)
        {
            Die(victim, assassin);
        }
    }

    private void Die(int victim, int assassin)
    {
        isDie = !isDie;
        currentHp = emptyHp;
        if (OnDie != null) OnDie(victim, assassin);
    }

    public static Action<int, int> OnDamage;
    public static Action<int, int> OnDie;

}
