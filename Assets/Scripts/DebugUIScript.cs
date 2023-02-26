using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugUIScript : MonoBehaviour
{

    //FIXME : This script is only here to run the debug menu and will have to be replaced ASAP by a clean and final version.
    // Start is called before the first frame update

    public Button TestMap1;
    public Button PopopMap;
    
    void Start () {
        Button btn = TestMap1.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickTestMap);
        
        Button btn2 = PopopMap.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClickPopop);
    }

    void TaskOnClickTestMap()
    {
        SceneManager.LoadScene(1);
    }
    
    void TaskOnClickPopop()
    {
        SceneManager.LoadScene(2);
    }
}
