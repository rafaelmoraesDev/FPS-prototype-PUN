using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] disableMonoOnInactive;    
    

    public void InnactivePlayer()
    {
        foreach (MonoBehaviour c in disableMonoOnInactive) if (c != null) c.enabled = false;
        foreach(Transform tr in transform.GetComponentsInChildren<Transform>())
            tr.gameObject.layer = LayerMask.NameToLayer("Invisible");
    }
    public void ActivePlayer()
    {
        GetComponent<Hp>().HpFull();
        foreach (MonoBehaviour c in disableMonoOnInactive) if (c != null) c.enabled = true;
        foreach (Transform tr in transform.GetComponentsInChildren<Transform>())
            tr.gameObject.layer = LayerMask.NameToLayer("Player");
    }

}
