using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class MainMenuFunctionality : MonoBehaviour
    {
        [SerializeField] private int gameLevelSceneIndex = 1;

        public void StartGame() => SceneManager.LoadSceneAsync(gameLevelSceneIndex, LoadSceneMode.Single);

        public void ExitGame() => Application.Quit();
    }
}
