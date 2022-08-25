using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int velocity = 2, velSalto = 5;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;

    const int ANI_CORRER = 1;
    const int ANI_QUIETO = 0;
    const int ANI_SALTO = 2;
    const int ANI_MUERTO = 3;
    bool puedeSaltar = true;
    bool muerto = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANI_CORRER);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANI_CORRER);
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_QUIETO);
        }
        if(Input.GetKeyDown(KeyCode.Space) && puedeSaltar==true){
            //rb.velocity = new Vector2(rb.velocity.x, velSalto);
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            puedeSaltar = false;
        }
        if(muerto == true){
            ChangeAnimation(ANI_MUERTO);
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        puedeSaltar = true;

        if(other.gameObject.tag == "Enemy"){
            muerto=true;
            Debug.Log("Estas muerto");
        }
        else muerto =false;
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
}
