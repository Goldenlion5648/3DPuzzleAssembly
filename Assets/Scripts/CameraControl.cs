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

        for (int i = 0; i < Mathf.Pow(SceneScript.cubeDim, 3); i++)
        {
            MeshRenderer mesh = GameObject.Find(SceneScript.textPartName + i).GetComponent<MeshRenderer>();
            mesh.enabled = !mesh.enabled;
        }
        //Cursor.visible = false;
    }


    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float movementSpeed = 20.0f;

    public static int moveDistance = 2;

    public static Transform selectedParent;


    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            if (pitch - speedV * Input.GetAxis("Mouse Y") > -90 && pitch - speedV * Input.GetAxis("Mouse Y") < 90)
                pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        float time = Time.deltaTime * 6;
        Vector3 pos = transform.position;

        //if(Input.mouseScrollDelta.y > .1 /*&& transform.rotation.eulerAngles.magnitude < 30*/)
        //{
        //    //transform.position += transform.rotation.eulerAngles;
        //    pos += Camera.main.transform.forward * movementSpeed * time *7;
        //}
        //else if (Input.mouseScrollDelta.y < -.1 /*&& transform.rotation.eulerAngles.magnitude < 30*/)
        //{
        //    //transform.position += transform.rotation.eulerAngles;
        //    pos -= Camera.main.transform.forward * movementSpeed * time * 7;
        //}
        //GameObject.Find("PlayerCamera")

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    pos.x += 3;
        //    Debug.Log("pressed a");

        //}

        if (selectedParent == null)
        {
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

        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //rotation

                if (Input.GetKeyDown(KeyCode.W))
                {
                    selectedParent.Rotate(new Vector3(90, 0, 0), Space.World);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    selectedParent.Rotate(new Vector3(-90, 0, 0), Space.World);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    selectedParent.Rotate(new Vector3(0, 90, 0), Space.World);

                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    selectedParent.Rotate(new Vector3(0, -90, 0), Space.World);

                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectedParent.Rotate(new Vector3(0, 0, 90), Space.World);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    selectedParent.Rotate(new Vector3(0, 0, -90), Space.World);
                }
            }
            else
            {
                //moving position
                
                if (Input.GetKeyDown(KeyCode.W))
                {
                    selectedParent.position += new Vector3(0, 0, moveDistance);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    selectedParent.position += new Vector3(0, 0, -moveDistance);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    selectedParent.position += new Vector3(-moveDistance, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    selectedParent.position += new Vector3(moveDistance, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectedParent.position += new Vector3(0, moveDistance, 0);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    selectedParent.position += new Vector3(0, -moveDistance, 0);
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vec

        //}

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedParent == null)
            {
                RaycastHit target;

                if (Physics.Raycast(transform.position, transform.forward, out target, 40) && target.collider.name.Contains(SceneScript.partName))
                {
                    for (int i = 0; i < Mathf.Pow(SceneScript.cubeDim, 3); i++)
                    {
                        GameObject current = GameObject.Find(SceneScript.partName + i);
                        Renderer r = current.gameObject.GetComponent<Renderer>();
                        r.material.SetColor("_Color", Color.red);
                    }
                    //it is the parent
                    if (target.transform.parent == null)
                    {
                        //target.transform.position += -transform.forward * 10;
                        selectedParent = target.transform;
                        //Debug.Log("Selected Parent: " + selectedParent);


                        Renderer parentObject = target.transform.gameObject.GetComponent<Renderer>();
                        TextMesh objectText = target.transform.gameObject.transform.GetComponentInChildren<TextMesh>();
                        parentObject.material.SetColor("_Color", Color.green);
                        objectText.color = Color.black;

                        Transform[] targetObject2 = target.transform.GetComponentsInChildren<Transform>();
                        //Debug.Log("targetObject2.Length: " + targetObject2.Length);
                        for (int i = 0; i < targetObject2.Length; i++)
                        {
                            //Debug.Log(targetObject2[i].ToString());
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
                        //target.transform.parent.transform.position += -transform.forward * 10;

                        selectedParent = target.transform.parent;

                        //Debug.Log("Selected Parent: " + selectedParent);

                        Renderer parentObject = target.transform.parent.gameObject.GetComponent<Renderer>();
                        TextMesh objectText = target.transform.parent.gameObject.transform.GetComponentInChildren<TextMesh>();
                        parentObject.material.SetColor("_Color", Color.green);
                        objectText.color = Color.black;

                        Transform[] targetObject2 = target.transform.parent.GetComponentsInChildren<Transform>();
                        //Debug.Log("targetObject2.Length: " + targetObject2.Length);

                        for (int i = 0; i < targetObject2.Length; i++)
                        {
                            //Debug.Log(targetObject2[i].ToString());

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
            else
            {
                selectedParent = null;
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (selectedParent == null)
            {

                Cursor.visible = !Cursor.visible;
                if (Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;
                else
                    Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                selectedParent = null;

            }
        }


        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    UnityEditor.EditorApplication.isPlaying = false;

        //}

        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < Mathf.Pow(SceneScript.cubeDim, 3); i++)
            {
                MeshRenderer mesh = GameObject.Find(SceneScript.textPartName + i).GetComponent<MeshRenderer>();
                mesh.enabled = !mesh.enabled;
            }

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("SampleScene");

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("titleScreen");

        }
    }


}
