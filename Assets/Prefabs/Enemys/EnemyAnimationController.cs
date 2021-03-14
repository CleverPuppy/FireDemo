using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    // Start is called before the first frame update

    AnimatorStateInfo stateInfo;
    Animator m_ani;
    void Start()
    {
        m_ani = gameObject.GetComponent<Animator>();
        if (m_ani)
        {
            stateInfo = m_ani.GetCurrentAnimatorStateInfo(0);
        }
        else
        {
            Debug.LogError("object don't have animator\n");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        m_ani.SetTrigger("Damage");
    }

    public void Idle()
    {
        m_ani.SetBool("Idle", true);
    }

    public void Death()
    {
        m_ani.SetBool("Death", true);
    }

    public void Run()
    {
        m_ani.SetBool("Run", true);
    }

    public void Attack()
    {
        m_ani.SetBool("Attack", true);
    }

}
