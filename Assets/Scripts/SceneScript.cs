using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform prefab;

 
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
                    Transform newCube = Instantiate(prefab, new Vector3(curXPos, curYPos, curZPos), Quaternion.identity);
                    t.anchor = TextAnchor.MiddleCenter;
                    t.transform.position = new Vector3(newCube.transform.position.x, newCube.transform.position.y, newCube.transform.position.z - .5f);
                    t.text = total.ToString();


                    total++;
                    Debug.Log("made new Cube");
                }
                curXPos -= cubeSpacing;
                curZPos = startPos.z;
            }
            curYPos += cubeSpacing;
            curXPos = startPos.x;
        }


        //for (int i = 0; i < (int)Mathf.Pow(cubeDim, 3); i++)
        //{
        //    GameObject text = new GameObject();
        //    TextMesh t = text.AddComponent<TextMesh>();


        //    t.fontSize = 20;

        //    if (i % 3 == 0)
        //    {
        //        curXPos -= cubeSpacing;
        //        curZPos = startPos.z;

        //    }
        //    if (i % 9 == 0)
        //    {
        //        curYPos += cubeSpacing;
        //        curXPos = startPos.x;
        //    }
        //    curZPos += cubeSpacing;
        //    Transform x = Instantiate(prefab, new Vector3(curXPos, curYPos, curZPos), Quaternion.identity);
        //    t.anchor = TextAnchor.MiddleCenter;
        //    t.transform.position = new Vector3(x.transform.position.x , x.transform.position.y, x.transform.position.z - .5f);
        //    t.text = i.ToString();



        //    Debug.Log("made new Cube");
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }
}
