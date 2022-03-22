using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private Input _Input;
    
    private void Start() => _Input = GetComponent<Input>();

        private void Update()
    {
        if (_Input.ExitGame)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
