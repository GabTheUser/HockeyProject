using UnityEngine;
using TMPro;
using Photon.Pun;

public class ChangeScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] scoreText;
    [SerializeField]
    private TextMeshProUGUI[] enemyScoreText;
    private int myScore;
    private int enemyScore;

    [PunRPC]
    public void EnemyScoreUp()
    {
        enemyScore++;
        for (int i = 0; i < enemyScoreText.Length; i++)
        {
            enemyScoreText[i].text = "" + enemyScore;
        }
    }
    [PunRPC]
    public void PlayerScoreUp()
    {
        myScore++;
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = "" + myScore;
        }
    }
    [PunRPC]
    public void ScoreToZero()
    {
        myScore = 0;
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = "" + myScore;
        }
        enemyScore = 0;
        for (int i = 0; i < enemyScoreText.Length; i++)
        {
            enemyScoreText[i].text = "" + enemyScore;
        }
    }
}
