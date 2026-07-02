using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<OptionsManager>().Initialize();
        SceneManager.LoadScene("SampleScene");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
