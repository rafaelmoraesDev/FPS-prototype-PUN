using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class HpEffects : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Image reddamage;
    private PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void OnEnable()
    {
        Hp.OnDamage += OnDamageListen;
    }
    private void OnDisable()
    {
        Hp.OnDamage -= OnDamageListen;
    }
    public void OnDamageListen(int victim, int assassin)
    {
        if (photonView.IsMine && photonView.ViewID == victim)
            StartCoroutine(DamageEffectIE());
    }
    IEnumerator DamageEffectIE()
    {
        reddamage.enabled = true;
        photonView.RPC("ChangeColorMesh", RpcTarget.All, 1.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.15f);
        photonView.RPC("ChangeColorMesh", RpcTarget.All, 1.0f, 1.0f, 1.0f);
        reddamage.enabled = false;
        photonView.RPC("ChangeColorMesh", RpcTarget.All, 0.0f, 1.0f, 1.0f);
    }

    [PunRPC]
    void ChangeColorMesh(float r, float g, float b)
    {
        mesh.material.color = new Color(r, g, b);
    }

}
