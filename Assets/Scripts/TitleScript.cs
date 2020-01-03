using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour
{
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        //if (doneTypingSeed == null)
        //    doneTypingSeed = new UnityEvent();

        //doneTypingSeed.AddListener(;
        SceneManager.UnloadSceneAsync("Scenes/SampleScene");
        if (SceneScript.seedNum != 0)
        {
            input.text = SceneScript.seedNum.ToString();
        }

        input.ActivateInputField();
        //inpt
        Cursor.visible = true;




    }
    
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneScript.seedNum = int.Parse(input.text);
            Debug.Log("Switching scenes");
            Cursor.visible = false;
            SceneManager.LoadScene("Scenes/SampleScene");

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    SceneScript.seedNum = int.Parse(input.text);
        //    Debug.Log("Switching scenes");
        //    SceneManager.LoadScene("SampleScene");

        //}

    }

}
