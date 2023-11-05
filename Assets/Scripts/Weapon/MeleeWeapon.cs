using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GunData weaponData;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private InputActionReference Shoot;
    [SerializeField] private bool hasAnimation = false;
    [SerializeField] private string attackAnimation;
    [SerializeField] private string idleAnimation;

    private Animator animator;
    private bool dealDamage = false;

    private void Start()
    {
        if (hasAnimation)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Shoot.action.WasPressedThisFrame())
        {
            StartCoroutine(Swing());
        }
    }

    private IEnumerator Swing()
    {
        if (hasAnimation)
            animator.Play(attackAnimation);
        dealDamage = true;
        yield return new WaitForSeconds(1.5f);
        if (hasAnimation)
            animator.Play(idleAnimation);
        dealDamage = false;
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
