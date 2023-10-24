using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject Body;

    [SerializeField] private int damagePerShot = 20;
    [SerializeField] private float timeBetweenBullets = 0.15f;
    [SerializeField] private float range = 100f;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;

    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    public Light faceLight;
    float effectsDisplayTime = 0.2f;
    public int DamagePerShot
    {
        get 
        { 
            return damagePerShot;
        } 
        
        set
        {
            damagePerShot = value;
        } 
    }

    void Awake()
    {


        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();



    }


    void Update()
    {

        timer += Time.deltaTime;




        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }


    }

    public void DisableEffects()
    {

        gunLine.enabled = false;
        faceLight.enabled = false;
        gunLight.enabled = false;
    }

    public void StartShoot()
    {
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();


        gunLight.enabled = true;
        faceLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();


        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
       
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay, out shootHit, range))
        {

            Rigidbody targetRigidbody = shootHit.collider.GetComponent<Rigidbody>();
            ITakingDamage playerHealth = shootHit.collider.GetComponent<ITakingDamage>();

            if (targetRigidbody)
            {

                targetRigidbody.AddForce(transform.forward * 300);


            }
            if (playerHealth != null)
            {

                playerHealth.TakeDamage(damagePerShot);
            }
            gunLine.SetPosition(1, shootHit.point);
        }




        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }


    public void Drop()
    {
        transform.SetParent(null);
        StartCoroutine(WaitDrop());
    }

    IEnumerator WaitDrop()
    {
        yield return new WaitForSeconds(0.3f);

        Body.transform.SetParent(null);
        Body.GetComponent<Collider>().enabled = true;
        if (Body.GetComponent<Rigidbody>() == null)
        {
            Body.AddComponent<Rigidbody>();
        }
        Body.GetComponent<Rigidbody>().isKinematic = false;
        Body.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 200);
    }
}