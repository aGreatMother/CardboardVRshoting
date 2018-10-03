using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour {

    public FlyEnemy father;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.drag = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        //if (this.GetComponent<Rigidbody>()==null&&father.crushing) {
        //    rb = this.gameObject.AddComponent<Rigidbody>();
        //    rb.useGravity = false;
        //    rb.isKinematic = true;
        //    rb.drag = 0f;
        //}
	}


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.StartsWith("lunchOut"))
        {
            father.FindLunchBox();

        }
    }
}
