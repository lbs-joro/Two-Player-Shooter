using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    string newSceneName;

    private void Start()
    {
        Button btn = GetComponent<Button>();

        if(btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                ChangeSceneTo();
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeSceneTo();
    }

    public void ChangeSceneTo()
    { 
        SceneManager.LoadScene(newSceneName);
    }
}
