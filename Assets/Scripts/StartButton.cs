using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("bullet"))
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            Singleton<GameManager>.Instance.StartGame();
            Destroy(this.gameObject, 0.2f);
        }
    }

    }
