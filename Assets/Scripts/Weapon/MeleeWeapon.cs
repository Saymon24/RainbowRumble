using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GunData weaponData;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private InputActionReference Shoot;

    private Animator animator;
    private bool dealDamage = false;
    private bool performSwing = false;

    private void Start()
    {
        animator = GameObject.Find("PlayerRig").GetComponent<Animator>();
    }

    private void Update()
    {
        GameObject.Find("Model").transform.position = GameObject.Find("WeaponSocket").transform.position;
        GameObject.Find("Model").transform.rotation = GameObject.Find("WeaponSocket").transform.rotation;

        GameObject.Find("WeaponSocket").transform.position = GameObject.Find("RightHandSocket").transform.position;
        GameObject.Find("WeaponSocket").transform.rotation = GameObject.Find("RightHandSocket").transform.rotation;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            performSwing = false;
            dealDamage = false;
        }

        if (Shoot.action.WasPressedThisFrame() && !performSwing)
        {
            Attack();
        }
    }

    public void EndMeleeAttack()
    {
        performSwing = false;
        dealDamage = false;
    }

    private void Attack()
    {
        animator.Play("Attack_1");
        AudioManager.instance.PlaySFX("Hammer" + Random.Range(1, 4));
     
        performSwing = true;
        dealDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Touché");
        if (other.CompareTag("Enemy") && dealDamage)
        {
            print("Touché enemi");
            other.GetComponent<Enemy>().takeDamage(30000);
        }
    }

}
