
using UnityEngine;
using DG.Tweening;
public class Reward : MonoBehaviour {
    private Rigidbody rb;
    public AudioClip gotSuper;
    private Vector3 orignScale;
    private AudioSource audioSor;
	void Start () {
        orignScale = this.transform.localScale;
        audioSor = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(5f, Random.Range(-2f, 2f), Random.Range(-3f, 3f))*8f);
        this.transform.localScale= this.transform.localScale*0.2f;
        this.transform.DOScale(orignScale, 0.4f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void TurnRed() {
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    void OnTriggerEnter(Collider other)
    {

         if (other.gameObject.name.StartsWith("bullet"))
        {
            audioSor.Stop();
            audioSor.clip = gotSuper;
            audioSor.Play();
            this.transform.DOScale(Vector3.zero, 0.4f);
            TurnRed();
            Singleton<gun>.Instance.TurnSuper();
            Destroy(this.gameObject,0.4f);
        }
    }
}
