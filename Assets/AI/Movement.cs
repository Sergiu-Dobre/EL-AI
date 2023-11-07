using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Movement : MonoBehaviour
{
  
    public CharacterController controlls;

    public float speed = 12f;
    public float gravity = 0;
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        // we are gonna use the axis for movement obv x&z because we dont want to go up
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        //this way the movement will be transformed depending where does our player "look"

        controlls.Move(move * speed * Time.deltaTime);
        //the move in the brackets is the Vector3 from above, the speed variable, and the time delta time so that the movement wont be affected by frames

        velocity.y += gravity * Time.deltaTime;
        controlls.Move(velocity * Time.deltaTime);
    }
}