using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SetDamage : MonoBehaviour
{
    [SerializeField] PhotonView pvOwner;
    [SerializeField] int damage;
    public bool ActiveSc;

    public void SetOwner(PhotonView pv)
    {
        pvOwner = pv;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ActiveSc) return;
        Hp hp = other.GetComponent<Hp>();
        if (!hp) return;
        hp.Damage(damage, pvOwner.ViewID);
        if (OnSetDamage != null)
            OnSetDamage();
    }

    public static Action OnSetDamage;


}
