using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static MainManager Instance;


    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }



        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
