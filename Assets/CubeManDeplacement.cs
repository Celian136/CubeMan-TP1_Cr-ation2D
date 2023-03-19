using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeManDeplacement : MonoBehaviour
{
    float vitesseX;      //vitesse horizontale actuelle
    public float vitesseXMax;   //vitesse horizontale Maximale d�sir�e
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut d�sir�e
    float partieTerminee;
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
    // Update is called once per frame
    void Update()
    {
        //D�placement vers la gauche....� CONTINUER !!
        if(Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            vitesseX = -vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //D�placement vers la droite
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            vitesseX = vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x; //m�morisation de la vitesse actuelle en X.
        }

        print (Physics2D.OverlapCircle(transform.position, 0.20f) == true);

        //D�placement vers le haute, faire sauter le CubeMan avecc la touche "w"
        if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && Physics2D.OverlapCircle(transform.position, 0.20f) == true)
        {
            vitesseY = vitesseSaut;
            GetComponent<Animator>().SetBool("saute", true);
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y; //vitesse actuelle vertical de "saute"
        }

        //Appliquer les vitesses en X et Y
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);

        
        //******************************Gestion des animations des variables Bool�ennes "course" et "repos"***********//

        //Activation de l'animation de "course" si la vitesse de d�placement n'est pas �gale � 0, et sinon, "repos" sera jou� par Animator
        if (vitesseX > 0.1f || vitesseX < -0.1f){
            GetComponent<Animator>().SetBool("course", true);
        } else{
            GetComponent<Animator>().SetBool("course", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.OverlapCircle(transform.position, 0.20f) == true) 
        {
            //partieTerminee = true;
            GetComponent<Animator>().SetBool("saute", false);

        }

        if (collision.gameObject.name == "Mossy - Roches" || collision.gameObject.name == "Mossy - Decorations&Hazards_3" || collision.gameObject.name == "Mossy - Decorations&Hazards_8")
        {
            GetComponent<Animator>().SetTrigger("mort");

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10, 30);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 30);
            }
            Invoke("Restart", 4f);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}