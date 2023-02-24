using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour
{
    public bool canAttack;

    public bool isAttack;

    public Transform weapon;

    [Header("Weapon Effects")] public AudioSource weaponSound;

    public ParticleSystem weaponEffect;

    public ParticleSystem bulletHole;

    [FormerlySerializedAs("bloodPartickle")]
    public ParticleSystem bloodParticle;

    private Animator weaponAnim;

    public TextMeshProUGUI weaponAMMO;

    [Header("AMMO Settings")] public float fireRate;
    private float _fireRate;
    public float shootRange;

    public int ammoClipCapacity;
    public int ammoRemaining;
    public int ammoCurrentBullet;


    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = GetComponent<Animator>();
        weaponAMMO.text = $"{ammoCurrentBullet} / {ammoRemaining}";
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        FireDelay();
    }

    public void FireDelay()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isAttack && canAttack && Time.time > _fireRate)
        {
            weaponAnim.SetBool("isFire", true);
            Shoot();

            _fireRate = Time.time + fireRate;
        }
        else
        {
            weaponAnim.SetBool("isFire", false);
        }
    }

    public void Shoot()
    {
        weaponEffect.Play();
        weaponSound.Play();
        weaponAnim.Play("Recoil");

        ammoCurrentBullet--;
        AmmoCheck();

        CheckTargetTag();
    }

    public void AmmoCheck()
    {
        canAttack = true;

        weaponAMMO.text = $"{ammoCurrentBullet} / {ammoRemaining}";
        if (ammoCurrentBullet <= 0) // Auto Reload
        {
            ammoCurrentBullet = ammoClipCapacity;
            ammoRemaining -= ammoClipCapacity;
            weaponAMMO.text = $"{ammoCurrentBullet} / {ammoRemaining}";
            if (ammoCurrentBullet == ammoClipCapacity && ammoRemaining == 0)
            {
                Debug.Log("NEED MORE AMMO"); // STOPS ATTACK FUNC.
                canAttack = false;
            }
        }
    }

    public void CheckTargetTag()
    {
        RaycastHit hitRay;
        if (Physics.Raycast(weapon.transform.position, weapon.transform.forward, out hitRay, shootRange))
        {
            Debug.Log(hitRay.transform.name);


            if (hitRay.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(bloodParticle, hitRay.point, Quaternion.LookRotation(hitRay.normal));
            }
            else
            {
                Instantiate(bulletHole, hitRay.point, Quaternion.LookRotation(hitRay.normal));
            }
        }
    }
}