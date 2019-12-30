using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }


    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float movementSpeed = 20.0f;


    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if(Input.mouseScrollDelta.y > 1)
        {
            transform.position += transform.rotation.eulerAngles;
        }
        //GameObject.Find("PlayerCamera")
        Vector3 pos = transform.position;
        if (Input.GetButtonDown("Fire1"))
        {

        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    pos.x += 3;
        //    Debug.Log("pressed a");

        //}
        float time = Time.deltaTime * 6; 

        if (Input.GetKey(KeyCode.W))
        {
            pos += Camera.main.transform.forward * movementSpeed * time;

        }

        if (Input.GetKey(KeyCode.S))
        {
            pos -= Camera.main.transform.forward * movementSpeed * time;

        }
        if (Input.GetKey(KeyCode.A))
        {
            pos += Vector3.Cross(Camera.main.transform.forward, Vector3.up) * movementSpeed * time;

        }
        if (Input.GetKey(KeyCode.D))
        {
            pos -= Vector3.Cross(Camera.main.transform.forward, Vector3.up) * movementSpeed * time;

        }

        if (Input.GetKey(KeyCode.Space))
        {
            pos.y += movementSpeed * time / 2;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            pos.y -= movementSpeed * time / 2;

        }
        transform.position = pos;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vec

        //}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit target;

            if(Physics.Raycast(transform.position, transform.forward, out target, 40) && target.collider.name.Contains(SceneScript.partName))
            {
                //Debug.Log("Hit " + target.collider);
                //Debug.Log("parent is " + target.collider.transform.parent);
                if (target.transform.parent != null)
                {
                target.transform.parent.transform.position += -transform.forward * 10;

                }
                else
                {
                    target.transform.position += -transform.forward * 10;
                }
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            //Cursor.visible = !Cursor.visible;
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEditor.EditorApplication.isPlaying = false;

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("SampleScene");

        }
    }

    
}
