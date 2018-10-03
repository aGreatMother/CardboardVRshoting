using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    public float speed = 10f;
    public float rotateSpeed = 500f;
    private Rigidbody rigidbody;


	// Use this for initialization
	void Start () {
        this.gameObject.name = "bullet";
        rigidbody= this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,45f)*rotateSpeed * Time.deltaTime*RandomSign());

        rigidbody.velocity=-this.gameObject.transform.forward * speed;
	}
    float RandomSign()
    {
        return Random.value < .5 ? 1f : -1f;
    }

}
