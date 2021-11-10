using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunShoot : MonoBehaviour
{
    private enum SHOOTTYPE { AUTO, SEMIAUTO }
    private PhotonView photonView;

    [SerializeField] private PhotonView pvOwner;
    [SerializeField] private GameObject bullet;
    [SerializeField] private SHOOTTYPE type;
    [SerializeField] private float cooldown = 0.2f;
    [SerializeField] private float pointStartBullet;
    [SerializeField] private Animation animFire;

    private Camera fpsCam;
    private bool incooldown = false;
    private float cooldownT = 0;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        fpsCam = transform.parent.GetComponent<Camera>();
    }
    private void Update()
    {
        if (photonView.IsMine == false) return;
        CooldownCheck();
        if (incooldown) return;
        if (Input.GetButtonDown("Fire1") && type == SHOOTTYPE.SEMIAUTO) Shoot();
        if (Input.GetButton("Fire1") && type == SHOOTTYPE.AUTO) Shoot();

    }

    void Shoot()
    {
        cooldownT = 0;
        Vector3 origin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 pointend;

        if (Physics.Raycast(origin, fpsCam.transform.forward, out hit))
        {
            pointend = hit.point;
        }
        else
            pointend = fpsCam.transform.forward * 50;
        if (hit.collider != null)
            InstanceBulletRPC(hit.point);
        else
            InstanceBulletRPC(origin + pointend);
    }

    void CooldownCheck()
    {
        if (cooldownT >= cooldown)
        {
            incooldown = false;
            cooldownT = cooldown;
        }
        else
        {
            cooldownT += Time.deltaTime;
            incooldown = true;
        }
    }


    public void InstanceBulletRPC(Vector3 destiny)
    {
        photonView.RPC("InstancePunRPC", RpcTarget.All, destiny);
    }

    [PunRPC]
    public void InstancePunRPC(Vector3 destiny)
    {
        Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
        newBullet.SetDestiny(destiny);

        if (photonView.IsMine)
        {
            SetDamage setdamage = newBullet.GetComponent<SetDamage>();
            setdamage.ActiveSc = true;
            setdamage.SetOwner(pvOwner);
        }
        animFire.Play();
    }

}
