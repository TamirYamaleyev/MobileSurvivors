using UnityEngine;

public class HpPot : MonoBehaviour
{
    public GameObject hpPot;
    public int hpAmount = 30;
    public GameObject player;

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);

        if (collision.gameObject == player)
        {
            Debug.Log("Player picked up HP!");
            
        }
    }
    
}