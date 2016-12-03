using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    public float playerHealth = 200f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter (Collision otherObject)
    {
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
