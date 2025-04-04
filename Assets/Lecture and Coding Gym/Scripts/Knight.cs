using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class Knight : MonoBehaviour
{

    SpriteRenderer sr;
    Animator animator;
    public Animator dustAnimator;

    public AudioSource stepAudio;
    public AudioClip[] footSteps;

    bool canRun = true;
    bool dust = false;

    public GameObject Dust;

    public float speed = 2f;

    public CinemachineImpulseSource impulseSource;

    public Tilemap tilemap;

    public Tile grass;
    public Tile stone;

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

        if (!dust)
        {
            Dust.SetActive(false);
        }
        else
        {
            Dust.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            
            if (tilemap.GetTile(gridPos) == stone)
            {
                transform.position = mousePos;
            }
        }
    }

    public void attackHasFinshed()
    {
        Debug.Log("The attack animation just finished.;");
        canRun = true;
    }

    public void showDust()
    {
        dust = true;
        dustAnimator.SetTrigger("startDust");

        if (tilemap.GetTile(tilemap.WorldToCell(transform.position)) == stone)
        {
            stepAudio.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
        }

        impulseSource.GenerateImpulse();
    }

    public void hideDust()
    {
        dust = false;
    }
}
