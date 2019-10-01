using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoeScript : MonoBehaviour
{
    private Animator animator;

    private static readonly int PlayIdle = Animator.StringToHash("playIdle");

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 1f) < 0.0025f)
        {
            animator.SetTrigger(PlayIdle);
        }
    }
}
