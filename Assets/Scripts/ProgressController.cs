using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressController : MonoBehaviour
{
    public GameObject[] levelButtons;
    private Transform playerTransform;
    private Animator playerAnimator;
    public Transform[] checkPoints;
    private int currentCheckpoint;

    private void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "Levels")
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
            if (PlayerPrefs.HasKey("Progress"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("Progress"); i++)
                {
                    levelButtons[i].GetComponent<BoxCollider2D>().enabled = true;
                }

                if (PlayerPrefs.GetInt("Progress") >= 2)
                {
                    playerTransform.transform.position = levelButtons[PlayerPrefs.GetInt("Progress") - 1].GetComponent<Transform>().position;
                    playerTransform.transform.position += Vector3.up / 2;

                    switch(PlayerPrefs.GetInt("Progress"))
                    {
                        case 2:
                            currentCheckpoint = 3;
                            break;

                        case 3:
                            currentCheckpoint = 4;
                            break;

                        case 4:
                            currentCheckpoint = 5;
                            break;

                        case 5:
                            currentCheckpoint = 6;
                            break;

                        case 6:
                            currentCheckpoint = 8;
                            break;
                    }
                }
            }
            else
            {
                currentCheckpoint = 0;
                levelButtons[0].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Levels")
        {
            if (PlayerPrefs.GetInt("Progress") == 1)
            {
                if(playerTransform.position.x < checkPoints[0].position.x)
                {
                    playerAnimator.Play("Walk");
                    playerTransform.Translate((Vector3.right * 3) * Time.deltaTime);
                }
                else
                {
                    playerAnimator.Play("Stand");
                    currentCheckpoint = 1;
                }    
            }
        }
    }

    public void ChangeProgress(int num)
    {
        PlayerPrefs.SetInt("Progress", num);
    }

}
