using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
     public Camera camera;
     public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

     // Update is called once per frame
     void Update()
     {
          //left click
          if (Input.GetMouseButtonDown(0))
          {
               //capture current mouse position in screen space
               Vector3 mousePosition = Input.mousePosition;
               //call our raycast function
               checkRaycast(mousePosition, 0);
          }
          //right click
          if (Input.GetMouseButtonDown(1)) {
               //capture current mouse position in screen space
               Vector3 mousePosition = Input.mousePosition;
               //call our raycast function
               checkRaycast(mousePosition, 1);
          }
     }

     private void FixedUpdate()
     {
     }

     private void checkRaycast(Vector3 mousePosition, int mouseButton) {
          //The raycast code is pretty much the code from the Unity documentation, and I'm not quite sure how it works.
          //raycast requires a bit mask of the layer we want to use
          int layerMask = 1 << 5;
          //this is supposed to give us all layers that aren't the layer specified above (5 is the UI layer)
          layerMask = ~layerMask;
          //our hit object
          RaycastHit hit;
          //I had manually set the Z so it was in front of the level geometry
          //mousePosition.z = -10.0f;
          //this translates us to world space, but the values are still off
          mousePosition = camera.ScreenToWorldPoint(mousePosition);
          //Debug.Log(mousePosition.ToString());
          //I hardcoded values to show off functionality
          Vector3 test = Vector3.zero;
          if (mouseButton == 0)
          {
               //this is for the brick that will be broken
               test = new Vector3(28.5f, 5.5f, -10.0f);
          }
          else if (mouseButton == 1) {
               //this is a question mark block
               test = new Vector3(24.5f, 5.5f, -10.0f);
          }
          //This is code from the Unity documentation, I don't quite understand it
          if (Physics.Raycast(test, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
          {
               //If a raycast occurrs output to the debug log
               Debug.Log("Did Hit");
               //if the object is a brick
               if(hit.collider.name.Contains("Brick")){
                    Debug.Log("Object was brick");
                    //get a reference to it
                    GameObject gameObject = hit.collider.gameObject;
                    //destroy it
                    Destroy(gameObject);
               }
               //if the object is a question mark block
               if (hit.collider.name.Contains("QuestionBlock")) {
                    Debug.Log("Object was question block");
                    //add 100 points to score
                    scoreManager.UpdateScore(100);
                    //add a coin to the coin counter
                    scoreManager.UpdateCoins();
               }
          }
          //if we did not hit anything with our raycast
          else
          {
               Debug.Log("Did not Hit");
          }
     }
}
