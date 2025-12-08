using System;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    public float xpAmount = 1;
    public string playerLayer =  "Player";

    public float rotationSpeed = 180f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerLayer) && other.gameObject.TryGetComponent(out PlayerXp player) )
        {
            player.AddXp(xpAmount);
            Destroy(gameObject);
        }
    }
}