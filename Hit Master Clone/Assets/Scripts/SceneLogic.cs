using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    [SerializeField] GameObject player, gameStartUI;
    [SerializeField] int sceneOrder = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartGame()
    {
        player.GetComponent<CharaController>().enabled = true;
        gameStartUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneOrder);
    }
}
