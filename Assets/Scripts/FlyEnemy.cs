using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class FlyEnemy : MonoBehaviour {
    public int heath = 2;
    private Animator anim;
    private NavMeshAgent agent;
    public bool crushing = false;
    private Rigidbody rb;
    public float crushSpeed = 10f;
    private float orginDis;
    private Vector3 orginDrc;
    private Renderer[] childRenders;
    private Color[] origincalColor;
    // Use this for initialization

  public AudioClip soundGotHit;
   
    public AudioSource audioSource;

	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        agent.SetDestination(Singleton<GameManager>.Instance.lunchBox.position);
        RandomrizeStartSpeed();
       childRenders=  this.transform.Find("offset").transform.Find("MESHES").GetComponentsInChildren<Renderer>();

        origincalColor = new Color[childRenders.Length];
        for (int i = 0;i< childRenders.Length;i++) {
            origincalColor[i] = childRenders[i].material.color;
        }

        audioSource = this.GetComponent<AudioSource>();
      

    }

   
  

    private void PlayGotHitSound() {
        audioSource.clip = soundGotHit;
        audioSource.Play();
    }

    private void RandomrizeStartSpeed() {
        agent.acceleration = Random.Range(0.3f, 1.2f);
        agent.speed = Random.Range(1.1f, 1.8f);
    }
    private bool isDead = false;
	// Update is called once per frame
	void Update () {
        if (Singleton<GameManager>.Instance.isGameover)
        { Dead();
            return;
        }

        if (crushing) {

            rb.velocity = crushSpeed*(Singleton<GameManager>.Instance.lunchBox.position- this.transform.position).normalized;
            this.transform.forward = Vector3.Lerp(orginDrc, rb.velocity,
               (orginDis - (Singleton<GameManager>.Instance.lunchBox.position - this.transform.position).magnitude / orginDis));
          
        }
        if (!isDead&&heath <= 0)
            Dead();


        if (turnRed) {
            if (colorChangeTimeCount < colorChangeTime)
                colorChangeTimeCount += Time.deltaTime;
            else
                ColorChangeBack();
        }
    }

    public void Dead() {

        
        agent.enabled = false;
        anim.SetBool("dead", true);
        this.rb.useGravity = true;
        Destroy(this.gameObject, 0.8f);
        isDead = true;
        if (heath == 0 && Random.Range(1, 4) == 1&&!Singleton<gun>.Instance.turnSuper)
            Singleton<GameManager>.Instance.AppearReward(this.transform.position);
    }

    private float colorChangeTime = 0.3f;
    private float colorChangeTimeCount = 0f;
    private bool turnRed=false;
    public void GotShoot()
    {
        if(heath>0)
        { heath -= 1;
            PlayGotHitSound();
        turnRed = true;
        colorChangeTimeCount = 0f;
            foreach (Renderer render in childRenders)
            {
                render.material.color = Color.red;
            }
        }

    }
    private void ColorChangeBack()
    {
        for (int i = 0; i < childRenders.Length; i++)
        {
             childRenders[i].material.color=origincalColor[i];
        }


    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "lunch")
        {

            Singleton<GameManager>.Instance.lunchBoxHealthRamain -= 1;
            Singleton<LunchBox>.Instance.ChangeColor();
            transform.DORotate(new Vector3(0f,Random.Range(-10f,160f), 0f), 0.3f);
            Dead();
            crushing = false;
            rb.velocity = Vector3.zero;
        }
        else if (other.gameObject.name.StartsWith("bullet"))
        {
            GotShoot();
            Destroy(other.gameObject);
        }
    }

    public void FindLunchBox() 

       {
        if (heath <= 0)
            return;


            agent.enabled = false;
            //rb.AddForce(crushSpeed * (Singleton<GameManager>.Instance.lunchBox.position - this.transform.position).normalized);
            crushing = true;
            orginDis = (Singleton<GameManager>.Instance.lunchBox.position - this.transform.position).magnitude;
            orginDrc = this.transform.forward;

        }
    
}
