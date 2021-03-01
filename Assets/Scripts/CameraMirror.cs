using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMirror : MonoBehaviour
{
  public Transform character;

  void Update()
  {
    //I was having a lot of issues getting the camera aligned with the player so I modified this to give the desired result
    //it follows the x and y of the player, and y has an additional offset to help the camera show the player more of the level
    Vector3 pos = new Vector3(character.position.x, character.position.y+2.0f, transform.position.z);

    transform.position = pos;
  }

}
