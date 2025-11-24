using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //first option button of play
    }

    public void EndingCredits() //the one of the credits
    {
        SceneManager.LoadScene("EndingCredits");
    }

    public void Quit() //to stop the game
    {
        Application.Quit();
    }
    
    public void GoBack() //from credits to the starting screen
    {
        SceneManager.LoadScene("Scenes/D16");
    }

}