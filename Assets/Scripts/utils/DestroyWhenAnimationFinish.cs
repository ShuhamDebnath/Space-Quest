using System.Collections;
using UnityEngine;

public class DestroyWhenAnimationFinish : MonoBehaviour
{
    public Animator animator;
    
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Delay(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator Delay(float length)
    {
        yield return new WaitForSeconds(length);
        gameObject.SetActive(false);
    }
}
