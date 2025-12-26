using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("City_part");
    }
}
