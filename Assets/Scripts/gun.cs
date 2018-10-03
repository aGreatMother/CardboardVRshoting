using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletApearPoint;
    private Animator anim;
    public float bulletAppearRate = 0.1f;
    public bool shooting = true;
    public bool turnSuper = false;
    public float superTime = 3f;
    private AudioSource audioSor;
    public Renderer fire;
    // Use this for initialization
	void Start () {
        audioSor = this.GetComponent<AudioSource>();
        anim = this.GetComponent<Animator>();
        StartCoroutine(AppearBullet());
        orginalBulletRate = bulletAppearRate;

    }
	
	// Update is called once per frame
	void Update () {
        if (turnSuper) {
            superTimecount += Time.deltaTime;
            if (superTimecount >= superTime)
            {  turnSuper = false;
                Singleton<GameManager>.Instance.superSound.Stop();
                superTimecount = 0f;
                bulletAppearRate =orginalBulletRate;
                anim.SetBool("turn", false);
            }

        }

        if (isFireAppear) {
            fireApearTimeCount += Time.deltaTime;
            if (fireApearTimeCount >= fireAppearTime) {
                fire.enabled = false;
                fireApearTimeCount = 0f;
                isFireAppear = false;
            }
        }

	}
    private float superTimecount=0f;
    private float orginalBulletRate = 0f;
    public void TurnSuper() {
        
        turnSuper = true;
        anim.SetBool("turn", true);
        superTimecount = 0f;
    }

    public void StartShooting() {
        if (!shooting)
            shooting = true;
        StartCoroutine(AppearBullet());
    }
    

    public void FasterBulletRate() {
        if (turnSuper)
        {
            Singleton<GameManager>.Instance.superSound.Play();
            bulletAppearRate /= 2.5f;
        }
    }
    private float fireAppearTime = 0.14f;
    private float fireApearTimeCount = 0f;
    private bool isFireAppear = false;

    public IEnumerator AppearBullet() {
        while (shooting)
        {
            isFireAppear = true;
            fire.enabled = true;
            fire.transform.parent.transform.localEulerAngles = Random.Range(0f, 360f) * Vector3.right;
            audioSor.Stop();
            audioSor.Play();
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = bulletApearPoint.position;
            bullet.transform.forward = this.transform.right;
            yield return new WaitForSeconds(bulletAppearRate);
        }
    }
}
