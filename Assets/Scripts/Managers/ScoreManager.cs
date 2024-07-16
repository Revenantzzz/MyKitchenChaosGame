using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyKitchenChaos
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        public static ScoreManager Instance { get; private set; }
        private int score = 0;
        public int Score { get { return score; } private set { score = value; } }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            ChangeScore(0);  
        }
        private void Start()
        {
            Score = 0;
            UIDishOrdered.Instance.OnChangeScore += ChangeScore;
        }
        private void ChangeScore(int score)
        {
            if((this.Score + score) <= 0)
            {
                this.Score = 0;
            }
            else
            {
                this.Score += score;
            }           
            scoreText.text = "Score: " + Score;
        }
    }
}
