using UnityEngine;

public class AddScore : MonoBehaviour
{
    
    public void ClickAddScore()
    {
        GameManager.Instance.AddScore(10);
    }
    
    
    public void ClickLoadAnotherScene()
    {
        GameManager.Instance.LoadAnotherScene();
    }
}
