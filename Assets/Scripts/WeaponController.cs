using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour
{
    public bool canAttack;

    public bool isAttack;

    public bool canReload;

    public bool isReload;

    public Transform weapon;

    [Header("Weapon Effects")] public AudioSource weaponSound;

    public ParticleSystem weaponEffect;

    public ParticleSystem bulletHole;

    [FormerlySerializedAs("bloodPartickle")]
    public ParticleSystem bloodParticle;

    private Animator weaponAnim;

    [FormerlySerializedAs("weaponAMMO")] public TextMeshProUGUI weaponAmmo;

    [Header("AMMO Settings")] public float fireRate;
    private float _fireRate;
    public float shootRange;

    public int ammoClipCapacity;
    public int ammoRemaining;
    public int ammoCurrentBullet;

    public float reloadTime = 2f;


    public float weaponDamage = 10f;


    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = GetComponent<Animator>();
        weaponAmmo.text = $"{ammoCurrentBullet} / {ammoRemaining}";
        canAttack = true;
        canReload = true;
    }

    // Update is called once per frame
    void Update()
    {
        FireDelay();
        AmmoCheck();
    }

    public void FireDelay()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isAttack && canAttack && Time.time > _fireRate && !isReload)
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

    public IEnumerator Reload()
    {
        Debug.Log("Reloading..");
        canAttack = false;
        isReload = true;

        weaponAnim.SetBool("isReload", true);

        yield return new WaitForSeconds(reloadTime); // Wait for Reload time seconds

        weaponAnim.SetBool("isReload", false);

        ammoCurrentBullet = ammoClipCapacity;
        ammoRemaining -= ammoClipCapacity;

        weaponAmmo.text = $"{ammoCurrentBullet} / {ammoRemaining}";

        canAttack = true;
        isReload = false;
    }

    public void AmmoCheck()
    {
        weaponAmmo.text = $"{ammoCurrentBullet} / {ammoRemaining}";

        if ((ammoCurrentBullet <= 0 || Input.GetKeyDown(KeyCode.R)) && !isReload && canReload) // Auto Reload
        {
            StartCoroutine(Reload()); // Starts the reload coroutine
        }

        else if (ammoCurrentBullet <= 0)
        {
            canAttack = false;
        }
        

        if (ammoRemaining <= 0)
        {
            Debug.Log("Need to Claim AmmoBox");
            canReload = false;
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

                hitRay.transform.GetComponent<EnemyController>().GetDamage(weaponDamage);
            }
            else
            {
                Instantiate(bulletHole, hitRay.point, Quaternion.LookRotation(hitRay.normal));
            }
        }
    }
}