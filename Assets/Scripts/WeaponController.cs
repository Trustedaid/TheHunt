using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool isAttack;

    private float _fireRate;
    public  float fireRate;

    public float shootRange;

    public Transform weapon;

    public AudioSource weaponSound;
    public ParticleSystem weaponEffect;

    public ParticleSystem bulletHole;
    public ParticleSystem bloodPartickle;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        
    }

    public void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isAttack && Time.time > _fireRate)
        {
            ShootRange();
            _fireRate = Time.time + fireRate;

        }
    }
    
    public void ShootRange()
    {
        RaycastHit hitRay;
        if (Physics.Raycast(weapon.transform.position, weapon.transform.forward, out hitRay, shootRange))
        {
            weaponSound.Play();
            // weaponEffect.Play();
            Debug.Log(hitRay.transform.name);
        }
            // Instantiate(bulletHole, hitRay.point, Quaternion.LookRotation(hitRay.normal));
    }
}
