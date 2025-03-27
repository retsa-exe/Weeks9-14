using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    SpriteRenderer sr;
    Animator animator;
    public Animator dustAnimator;

    public AudioSource stepAudio;
    public AudioClip[] footSteps;

    bool canRun = true;

    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        stepAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        sr.flipX = direction < 0;
        animator.SetFloat("movement", Mathf.Abs(direction));

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }

        if (canRun)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;
        }
    }

    public void attackHasFinshed()
    {
        Debug.Log("The attack animation just finished.;");
        canRun = true;
    }

    public void showDust()
    {
        dustAnimator.SetTrigger("startDust");
        int randomNumber = Random.Range(0, footSteps.Length);
        stepAudio.PlayOneShot(footSteps[randomNumber]);
    }
}
