using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private int lifeTime = 5;
    private Rigidbody rgbd;
    private Vector3 destiny;
    private bool activated = false;
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }
    public void SetDestiny(Vector3 position)
    {
        activated = true;
        destiny = position;
        StartCoroutine("DestroyIE", lifeTime);
    }
    private void Update()
    {
        if (activated == false) return;
        rgbd.MovePosition(Vector3.MoveTowards(transform.position, destiny, Time.deltaTime * speed));
    }
    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine("DestroyIE");
        StartCoroutine("DestroyIE", 0);
    }
    IEnumerator DestroyIE(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
