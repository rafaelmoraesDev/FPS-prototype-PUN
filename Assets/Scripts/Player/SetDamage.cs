using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SetDamage : MonoBehaviour
{
    [SerializeField] PhotonView _pvOwner;
    [SerializeField] int _damage;
    public bool ActiveSc;

    public void SetOwner(PhotonView pv)
    {
        _pvOwner = pv;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ActiveSc) return;
        Hp hp = other.GetComponent<Hp>();
        if (!hp) return;        
        hp.Damage(_damage,_pvOwner.ViewID);
    }

}
