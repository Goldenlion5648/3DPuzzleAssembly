using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform prefab;
    public GameObject originalCube;

    public static string partName = "cubePart";
    public static string textPartName = "textPart";
    public static string hasBeenUsedTag = "hasBeenUsed";
    public static string isParentTag = "isParent";
    public static int maxNumAttached = 5;
    public static bool shouldReset = false;
    public static int cubeDim = 3;
    public static int seedNum = 0;
    public Text seedText;

    //public static 


    void Awake()
    {
        setup();
    }

    void setup()
    {
        //Transform test = prefab.transform;

        Vector3 startPos = originalCube.transform.position;
        float sideLength = 6;
        //float cubeSpacing = sideLength / cubeDim + 2;
        float cubeSpacing = sideLength / cubeDim;

        float curXPos = 0;
        float curYPos = 8.5f;
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
                    GameObject newCube = Instantiate(originalCube, new Vector3(curXPos, curYPos, curZPos), Quaternion.identity);

                    newCube.name = "cubePart" + total;
                    text.name = textPartName + total;

                    text.transform.parent = newCube.transform;

                    t.anchor = TextAnchor.MiddleCenter;
                    t.transform.position = new Vector3(newCube.transform.position.x, newCube.transform.position.y, newCube.transform.position.z - .5f);
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

        GameObject og = GameObject.Find("Cube");
        Destroy(og);


        //for (int y = 0; y < cubeDim; y++)
        //{

        //tried seeds: 5, 15
        //int seedNum = Random.Range(0, 100);
        //int seedNum = 39;
        if(seedNum == 0)
        {
            seedNum = Random.Range(1, 1000);
        }
        Random.InitState(seedNum);
        Debug.Log("Seed is: " + seedNum);
        seedText.text = "Seed: " + seedNum;

        for (int i = 0; i < Mathf.Pow(cubeDim, 3); i++)
        {
            int addNewPartChecker = Random.Range(0, 3);
            //if (addNewPartChecker == 0)
            GameObject currentObjectBase = GameObject.Find(partName + i);
            //GameObject currentShapeHolder = new GameObject();
            if (currentObjectBase.tag.Contains(hasBeenUsedTag))
                continue;

            cubeProperties properties = currentObjectBase.AddComponent<cubeProperties>();

            int currentPartsAttached = 0;
            int currentNumPos = i;
            //total number of parts to attach
            for (int j = 0; j < maxNumAttached; j++)
            {
                int directionDecider = Random.Range(0, 3);

                if (directionDecider == 0)
                {
                    currentNumPos += 9;
                }
                else if (directionDecider == 1)
                {
                    if ((currentNumPos + 3) < (((currentNumPos / (int)(Mathf.Pow(cubeDim, 2))) + 1) * (Mathf.Pow(cubeDim, 2))))
                    {
                        //Debug.Log(currentNumPos + 3 + " was less than " +
                        //    (((currentNumPos / (int)(Mathf.Pow(cubeDim, 2))) + 1) * (Mathf.Pow(cubeDim, 2))));
                        //Debug.Log(Mathf.Pow(cubeDim, 2));
                        currentNumPos += 3;
                    }
                    else
                    {
                        if (currentNumPos >= 3)
                            currentNumPos -= 3;
                        else
                            continue;
                    }
                }
                else if (directionDecider == 2)
                {
                    if ((currentNumPos + 1) % 3 != 0)
                    {
                        currentNumPos += 1;
                    }
                    else
                    {
                        currentNumPos -= 1;
                    }
                }

                GameObject objectToAttach = GameObject.Find(partName + currentNumPos);

                if (currentNumPos < (Mathf.Pow(cubeDim, 3)) && objectToAttach.tag.Contains(hasBeenUsedTag) == false)
                {
                    objectToAttach.transform.SetParent(currentObjectBase.transform, true);
                    //GameObject.Find(partName + (i + 9)).transform.parent = (GameObject.Find(partName + i ).transform);
                    Debug.Log("combined " + partName + i + " and " + partName + currentNumPos);

                    currentObjectBase.tag = hasBeenUsedTag;
                    objectToAttach.tag = hasBeenUsedTag;
                    currentPartsAttached += 1;
                }
                else
                {
                    break;
                }

                if (currentPartsAttached > 4)
                    break;


            }
            //string newText = GameObject.Find(textPartName + i).GetComponentsInChildren<Text>
        }

        //get left over pieces
        for (int i = 0; i < Mathf.Pow(cubeDim, 3); i++)
        {
            GameObject currentObject = GameObject.Find(partName + i);

            bool foundParent = false;

            GameObject potentialParent;
            if (currentObject.tag.Contains(hasBeenUsedTag) == false)
            {
                if (i >= Mathf.Pow(cubeDim, 2))
                {
                    potentialParent = GameObject.Find(partName + (i - (int)(Mathf.Pow(cubeDim, 2))));
                    foundParent = addTag(ref currentObject, ref potentialParent);
                }
                else
                {
                    potentialParent = GameObject.Find(partName + (i + (int)(Mathf.Pow(cubeDim, 2))));
                    foundParent = addTag(ref currentObject, ref potentialParent);
                }

                if (foundParent == false)
                {
                    if (i % 3 != 0)
                    {
                        potentialParent = GameObject.Find(partName + (i - 1));
                        foundParent = addTag(ref currentObject, ref potentialParent);
                    }
                    else
                    {
                        potentialParent = GameObject.Find(partName + (i + 1));
                        foundParent = addTag(ref currentObject, ref potentialParent);
                    }
                }
                // check left edge
                if (foundParent == false)
                {
                    if ((i) < (i / (int)(Mathf.Pow(cubeDim, 2)) + 1) * (Mathf.Pow(cubeDim, 2)) &&
                        (i) >= ((i / (int)(Mathf.Pow(cubeDim, 2)) + 1) * (Mathf.Pow(cubeDim, 2))) - cubeDim)
                    {
                        potentialParent = GameObject.Find(partName + (i - cubeDim));
                        foundParent = addTag(ref currentObject, ref potentialParent);
                    }
                }

            }

        }

        //connect pieces with only 2 pieces
        for (int i = 0; i < Mathf.Pow(cubeDim, 3); i++)
        {
            GameObject currentObject = GameObject.Find(partName + i);
            GameObject parentObject;
            int childCount;
            if (currentObject.transform.parent != null)
            {
                //has a parent or alone
                parentObject = currentObject.transform.parent.gameObject;
                childCount = parentObject.transform.childCount;
            }
            else
            {
                //is the parent
                parentObject = currentObject.transform.gameObject;
                childCount = parentObject.transform.childCount;

            }

            if (childCount <= 2)
            {
                Debug.Log("checking " + (partName + i));
                bool isDone = false;
                int relativeReference = i + cubeDim * cubeDim;
                isDone = joinSmallerParts(relativeReference, parentObject);

                if (isDone == false)
                {
                    relativeReference = i - cubeDim * cubeDim;
                    isDone = joinSmallerParts(relativeReference, parentObject);
                }
                if (isDone == false)
                {
                    if (i / cubeDim == (i - 1) / cubeDim)
                    {
                        relativeReference = i - 1;
                        isDone = joinSmallerParts(relativeReference, parentObject);
                    }
                }
                if (isDone == false)
                {
                    if (i / cubeDim == (i + 1) / cubeDim)
                    {
                        relativeReference = i + 1;
                        isDone = joinSmallerParts(relativeReference, parentObject);
                    }
                }
                if (isDone == false)
                {
                    if (i / (cubeDim * cubeDim) == (i - cubeDim) / (cubeDim * cubeDim))
                    {
                        relativeReference = i - cubeDim;
                        isDone = joinSmallerParts(relativeReference, parentObject);
                    }
                }
                if (isDone == false)
                {
                    if (i / (cubeDim * cubeDim) == (i + cubeDim) / (cubeDim * cubeDim))
                    {
                        relativeReference = i + cubeDim;
                        isDone = joinSmallerParts(relativeReference, parentObject);
                    }
                }

            }

        }

        scramble();

    }

    public void scramble()
    {
        for (int i = 0; i < Mathf.Pow(cubeDim, 3); i++)
        {
            if (GameObject.Find(partName + i).transform.parent == null)
            {
                Transform parent = GameObject.Find(partName + i).transform;
                //rotations
                for (int j = 0; j < Random.Range(0, 4); j++)
                {
                    parent.Rotate(new Vector3(90, 0, 0), Space.World);
                }

                for (int j = 0; j < Random.Range(0, 4); j++)
                {
                    parent.Rotate(new Vector3(0, 90, 0), Space.World);
                }
                for (int j = 0; j < Random.Range(0, 4); j++)
                {
                    parent.Rotate(new Vector3(0, 0, 90), Space.World);
                }

                //positions
                int directionModifier = Random.Range(-1, 2);
                for (int k = 0; k < 10; k++)
                {
                    if (directionModifier == 0)
                    {
                        directionModifier = Random.Range(-1, 2);
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = 0; j < Random.Range(0, 4); j++)
                {
                    parent.position += new Vector3(0, directionModifier * CameraControl.moveDistance, 0);
                }
                for (int k = 0; k < 10; k++)
                {
                    if (directionModifier == 0)
                    {
                        directionModifier = Random.Range(-1, 2);
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = 0; j < Random.Range(0, 4); j++)
                {
                    parent.position += new Vector3(0, 0, directionModifier * CameraControl.moveDistance);
                }
                for (int k = 0; k < 10; k++)
                {
                    if (directionModifier == 0)
                    {
                        directionModifier = Random.Range(-1, 2);
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = 0; j < Random.Range(0, 4); j++)
                {
                    parent.position += new Vector3(directionModifier * CameraControl.moveDistance, 0, 0);
                }




            }
        }

    }

    public bool joinSmallerParts(int relativeReference, GameObject parentObject)
    {
        if (GameObject.Find(partName + relativeReference) != null)
        {
            GameObject potentialAttachie = GameObject.Find(partName + relativeReference);
            if (potentialAttachie.transform.IsChildOf(parentObject.transform) == false &&
                parentObject.transform.IsChildOf(potentialAttachie.transform) == false)
            {
                //it is the parent
                if (potentialAttachie.transform.parent == null)
                {
                    if (potentialAttachie.transform.childCount < 5)
                    {
                        parentObject.transform.SetParent(potentialAttachie.transform);
                        for (int i = 0; i < parentObject.transform.childCount; i++)
                        {
                            if (parentObject.transform.GetChild(i).name.Contains(partName))
                            {
                                parentObject.transform.GetChild(i).transform.SetParent(potentialAttachie.transform);
                            }
                        }
                        return true;

                    }
                }
                else //it has a parent
                {
                    if (potentialAttachie.transform.parent.childCount < 5)
                    {
                        parentObject.transform.SetParent(potentialAttachie.transform.parent.transform);
                        for (int i = 0; i < parentObject.transform.childCount; i++)
                        {
                            if (parentObject.transform.GetChild(i).name.Contains(partName))
                            {
                                parentObject.transform.GetChild(i).transform.SetParent(potentialAttachie.transform.parent.transform);
                            }

                        }
                        return true;

                    }
                }

            }
        }
        return false;
    }

    public bool addTag(ref GameObject currentObject, ref GameObject potentialParent)
    {
        if (potentialParent == null)
            return false;

        if (potentialParent.tag.Contains(hasBeenUsedTag))
        {
            if (potentialParent.transform.parent != null)
            {
                if (potentialParent.transform.parent.childCount < maxNumAttached)
                {
                    currentObject.transform.SetParent(potentialParent.transform.parent.transform);
                    Debug.Log("1attached leftover piece " + currentObject.name + " to " + potentialParent.transform.parent.transform.name);
                    currentObject.tag = hasBeenUsedTag;
                    return true;
                }
                else
                    return false;
            }

            if (potentialParent.transform.childCount < maxNumAttached)
            {
                currentObject.transform.SetParent(potentialParent.transform);
                currentObject.tag = hasBeenUsedTag;
                Debug.Log("2attached leftover piece " + currentObject.name + " to " + potentialParent.name);
                return true;
            }


        }
        if (potentialParent.tag.Contains(hasBeenUsedTag) == false)
        {
            currentObject.transform.SetParent(potentialParent.transform);
            Debug.Log("attached two leftover pieces " + currentObject.name + " to " + potentialParent.name);

            currentObject.tag = hasBeenUsedTag;
            potentialParent.tag = hasBeenUsedTag;
            return true;
        }
        return false;
    }


    void checkSolution()
    {
        //for (int i = 0; i < Mathf.Pow(cubeDim, 3); i++)
        //{
        //    GameObject current = GameObject.Find(partName + i);
        //    if(current.c)
        //    //if(current.)
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
            //if (shouldReset)
            //{
            //    for (int i = 0; i < Mathf.Pow(cubeDim, 3); i++)
            //    {
            //        GameObject current = GameObject.Find(partName + (i));
            //        Destroy(current);


            //    }
            //    setup();

            //    shouldReset = false;
            //}

        }
}
