using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public float playerHealth = 200f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter (Collision otherObject)
    {
        if (otherObject.collider.tag == "Enemy")
        {
            DecreaseHealth(otherObject.collider.GetComponent<Stats>().damageOnTouch);
        }
    }

    void IncreaseHealth(float boost)
    {
        playerHealth += boost;
    }

    void DecreaseHealth(float damage)
    {
        playerHealth -= damage;
    }
}
