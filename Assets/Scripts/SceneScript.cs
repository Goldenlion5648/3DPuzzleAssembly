using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform prefab;
    public GameObject prefab2;

    public static string partName = "cubePart";
    public static string textPartName = "textPart";

    void Start()
    {
        Transform test = prefab.transform;

        int cubeDim = 3;

        Vector3 startPos = prefab.position;

        float sideLength = 6;

        float cubeSpacing = sideLength / cubeDim + 2;

        

        float curXPos = 0;
        float curYPos = 1.5f;
        float curZPos = 0;
        int total = 0;
        for (int y = 0; y < cubeDim; y++)
        {
            for (int x = 0; x < cubeDim; x++)
            {
                for (int z = 0; z < cubeDim; z++)
                {
                    GameObject text = new GameObject();
                    TextMesh t = text.AddComponent<TextMesh>();


                    t.fontSize = 20;
                    curZPos += cubeSpacing;
                    //Transform newCube = Instantiate(prefab, new Vector3(curXPos, curYPos, curZPos), Quaternion.identity);
                    GameObject newCube2 = Instantiate(prefab2, new Vector3(curXPos, curYPos, curZPos), Quaternion.identity);

                    newCube2.name = "cubePart" + total;
                    text.name = textPartName + total;

                    text.transform.parent = newCube2.transform;

                    t.anchor = TextAnchor.MiddleCenter;
                    t.transform.position = new Vector3(newCube2.transform.position.x, newCube2.transform.position.y, newCube2.transform.position.z - .5f);
                    t.text = total.ToString();


                    total++;
                    //Debug.Log("made new Cube");
                }
                curXPos -= cubeSpacing;
                curZPos = startPos.z;
            }
            curYPos += cubeSpacing;
            curXPos = startPos.x;
        }


        //for (int y = 0; y < cubeDim; y++)
        //{

        for (int i = 0; i < cubeDim * cubeDim; i++)
        {
            int addNewPartChecker = Random.Range(0, 3);
            if (addNewPartChecker == 0)
            {
                GameObject currentObject = GameObject.Find(partName + i);
                currentObject.transform.SetParent(GameObject.Find(partName + (i + 9)).transform, true);
                //GameObject.Find(partName + (i + 9)).transform.parent = (GameObject.Find(partName + i ).transform);
                Debug.Log("combined " + partName + i + " and " + partName + (i + 9));

                currentObject.tag = "hasBeenUsed";



                //string newText = GameObject.Find(textPartName + i).GetComponentsInChildren<Text>

            }
        }


        //}



    }



    // Update is called once per frame
    void Update()
    {


    }
}
