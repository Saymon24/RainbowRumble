using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{
    public void MeleeAttackEnd()
    {
        if (GameObject.Find("WeaponHolder").TryGetComponent<WeaponHolder>(out WeaponHolder holder))
        {
            if (holder.GetSelectedWeapon().TryGetComponent<MeleeWeapon>(out MeleeWeapon meleeWeapon))
            {
                meleeWeapon.EndMeleeAttack();
            }
        }
    }
}
