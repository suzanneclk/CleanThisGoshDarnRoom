using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
// play
public void PlayGame(){
    SceneManager.LoadScene(4); //cutscene
}
public void RetryGame(){
    SceneManager.LoadScene(5); //game
}
//main menu
public void MainMenu(){
    SceneManager.LoadScene(1); //menu
}
//quit 
public void QuitGame(){
    Application.Quit();
}
//credits
public void Credits(){
    SceneManager.LoadScene(2); //credits
}
// instructions
public void Instructions(){
     SceneManager.LoadScene(3); //instrutcions
}
}
