using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;
    private bool inrange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inrange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
        {
            enemyParent.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inrange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inrange = false;
            enemyParent.SelectTarget();
        }
    }

}
