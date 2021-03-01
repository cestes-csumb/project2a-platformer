using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EthanCharacter : MonoBehaviour
{
     private Animator animator;
     public Rigidbody rb;
     public float modifier = 1;
     public float jumpForce = 5;
     [Range(-2, 2)] public float speed = 0;
     public ScoreManager scoreManager;


     void Awake()
     {
          animator = GetComponent<Animator>();
     }

     void Update()
     {
          float horizontal = Input.GetAxis("Horizontal");
          //if space is pressed we want the player to jump
          if (Input.GetKeyDown(KeyCode.Space))
          {
               rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
          }
          //if shift is currently down, update modifier so their speed increases
          if (Input.GetKeyDown(KeyCode.LeftShift))
          {
               //Debug.Log("Modifier increased");
               modifier = 2;
          }
          //otherwise return modifier to normal
          if (Input.GetKeyUp(KeyCode.LeftShift))
          {
               //Debug.Log("Modifier normal");
               modifier = 1;
          }
          //Use raycasting to check if we have a collision
          RaycastCheck();
          //horizontal = speed;
          //Debug.Log("Current Speed: " + horizontal * modifier);


          //Set character rotation
          //this was modified because my level was generated along the x-axis and NOT the z-axis
          //I also had to modify this y behavior because of issues I had getting the camera setup
          float y = (horizontal < 0) ? -90 : 90;
          Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
          transform.rotation = newRotation;

          //Set character animation
          //this now utilizes modifier so if they're holding shift down they go faster
          animator.SetFloat("Speed", Mathf.Abs(horizontal * modifier));

          //move character
          //because my player is along the x-axis I had to change this code
          //fixedHorizontal fixes the issues I had with the player not turning the correct direction and the running direction being reverse of the input
          float fixedHorizontal = 0.0f;
          //if horizontal is negative
          if (horizontal < 0)
          {
               //we need our horizontal to actually be positive
               fixedHorizontal = horizontal * -1;
          }
          else
          {
               //otherwise we need our horizontal to be negative
               fixedHorizontal = horizontal * -1;
          }
          //this now uses transform.right because my player is moving across the x-axis
          transform.Translate(transform.right * fixedHorizontal * modifier * Time.deltaTime);
          //my player's z value kept increasing and I wasn't sure why so I locked it to 0
          Vector3 position = transform.position;
          position.z = 0.0f;
          transform.position = position;
     }

     //RaycastCheck performs a raycast with a ray shooting up from the player
     void RaycastCheck()
     {
          //create a ray
          Ray ray = new Ray(transform.position, transform.up);
          RaycastHit hit;
          //if anything is 2.0f above our player we have a successful raycast
          if (Physics.Raycast(ray.origin, ray.direction, out hit, 2.0f))
          {
               //if the object is a brick
               if (hit.collider.name.Contains("Brick"))
               {
                    //destory it and give the player 100 points
                    Destroy(hit.collider.gameObject);
                    scoreManager.UpdateScore(100);
               }
               //if the object is a question block
               if (hit.collider.name.Contains("QuestionBlock"))
               {
                    //in mario the behavior is to only give the player 1 coin per question block, so I also removed the object
                    //prior to this the raycast was succcessful multiple times and the player was getting a lot of coins
                    Destroy(hit.collider.gameObject);
                    //give the player one coin
                    scoreManager.UpdateCoins();
                    //give them 100 points
                    scoreManager.UpdateScore(100);
               }
          }
     }
}
