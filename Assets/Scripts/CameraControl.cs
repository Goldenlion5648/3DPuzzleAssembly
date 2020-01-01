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

        if(Input.mouseScrollDelta.y > 1 && transform.rotation.eulerAngles.magnitude < 30)
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

            if (Physics.Raycast(transform.position, transform.forward, out target, 40) && target.collider.name.Contains(SceneScript.partName))
            {
                //Debug.Log("Hit " + target.collider);
                //Debug.Log("parent is " + target.collider.transform.parent);

                //it is the parent
                if (target.transform.parent == null)
                {
                    target.transform.position += -transform.forward * 10;

                    Renderer parentObject = target.transform.gameObject.GetComponent<Renderer>();
                    TextMesh objectText = target.transform.gameObject.transform.GetComponentInChildren<TextMesh>();
                    parentObject.material.SetColor("_Color", Color.green);
                    objectText.color = Color.black;

                    Transform[] targetObject2 = target.transform.GetComponentsInChildren<Transform>();
                    Debug.Log("targetObject2.Length: " + targetObject2.Length);

                    for (int i = 0; i < targetObject2.Length; i++)
                    {
                        Debug.Log(targetObject2[i].ToString());

                        if (targetObject2[i].name.Contains(SceneScript.textPartName))
                        {
                            TextMesh mesh2 = targetObject2[i].GetComponent<TextMesh>();
                            mesh2.color = Color.white;
                        }
                        else
                        {
                            Renderer render = targetObject2[i].GetComponent<Renderer>();
                            render.material.color = Color.green;
                        }

                    }

                }
                else
                {
                    target.transform.parent.transform.position += -transform.forward * 10;

                    Renderer parentObject = target.transform.parent.gameObject.GetComponent<Renderer>();
                    TextMesh objectText = target.transform.parent.gameObject.transform.GetComponentInChildren<TextMesh>();
                    parentObject.material.SetColor("_Color", Color.green);
                    objectText.color = Color.black;

                    Transform[] targetObject2 = target.transform.parent.GetComponentsInChildren<Transform>();
                    Debug.Log("targetObject2.Length: " + targetObject2.Length);

                    for (int i = 0; i < targetObject2.Length; i++)
                    {
                        Debug.Log(targetObject2[i].ToString());

                        if (targetObject2[i].name.Contains(SceneScript.textPartName))
                        {
                            TextMesh mesh2 = targetObject2[i].GetComponent<TextMesh>();
                            mesh2.color = Color.white;
                        }
                        else
                        {
                            Renderer render = targetObject2[i].GetComponent<Renderer>();
                            render.material.color = Color.green;
                        }

                    }
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
