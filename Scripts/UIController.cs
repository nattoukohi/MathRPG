using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathRPG
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Text playerStatsText, enemyStatsText, playerInventoryText;
        public delegate void OnPlayerUpdateHandler();
        public static OnPlayerUpdateHandler OnPlayerStatChange;
        public static OnPlayerUpdateHandler OnPlayerInventoryChange;

        public delegate void OnEnemyUpdateHandler(Enemy enemy);
        public static OnEnemyUpdateHandler OnEnemyUpdate;

        //private string numberXs;

        // Use this for initialization
        void Start()
        {
            OnPlayerStatChange += UpdatePlayerStats;
            OnPlayerInventoryChange += UpdatePlayerInventory;
            OnEnemyUpdate += UpdateEnemyStats;
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdatePlayerStats()
        {
            //playerStatsText.text = string.Format("プレイヤー  Level:{0}\nHP:{1}/攻撃:{2}/防御:{3}\nGold:{4}/Exp:{5}", player.Level,player.Energy, player.Attack,player.Defence, player.Gold, player.Experience);
            playerStatsText.text = string.Format("プレイヤー  Level:{0}\nHP:{1}", player.Level, player.Life[0]);
            for (int x = 1; x < player.Life.Length; x++)

                if (player.Life[x] != 0) { 
                playerStatsText.text += " + "+ player.Life[x] + "x" + "<size=30>^"+x + "</size>";
                }
            playerStatsText.text += string.Format("/MP:{0}/攻撃:{1}/防御:{2}\nGold:{3}/Exp:{4}", player.Energy, player.Attack, player.Defence, player.Gold, player.Experience);
        }

        public void UpdatePlayerInventory()
        {
            playerInventoryText.text = "アイテム：";
            foreach (string item in player.Inventory)
            {
                playerInventoryText.text += item + "/";
            }
        }

        public void UpdateEnemyStats(Enemy enemy)
        {
            //show stats when enemy is alive
            if (enemy) //&&enemy.Checker != 0
                if (enemy.Life[0] > 0|| enemy.Checker != 0)
                {
                    //enemyStatsText.text = string.Format("{0}  Level:{1}\nHP:{2}/攻撃:{3}/防御:{4}", enemy.Description, enemy.Level, enemy.Energy, enemy.Attack, enemy.Defence);
                    enemyStatsText.text = string.Format("{0}  Level:{1}\nHP:{2}", enemy.Description, enemy.Level, enemy.Life[0]);

                    for (int x = 1; x < enemy.Life.Length; x++)

                        if (enemy.Life[x] != 0)
                        {
                            enemyStatsText.text += " + " + enemy.Life[x] + "x" + "<size=30>^" + x + "</size>";
                        }
                }
                else
                {
                    enemyStatsText.text = "";
                    //enemy.Energy = enemy.MaxEnergy;
                }
        }
    }
}

