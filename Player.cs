using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG
{
    public class Player : Character
    {
        public int Floor { get; set; }
        public Room Room { get; set; }
        public World world;
        [SerializeField] Encounter encounter;

        static int NoOfStats = 5;
        int[] IncreasedStats = new int[NoOfStats]; //0 is hP

        // Use this for initialization
        void Start()
        {
            
            //first floor
            Life[0] = 100;
            Life[1] = 10;
            Level = 1;
            Floor = 0;
            MaxLife[0] = 100;
            Energy = 10;
            MaxEnergy = 10;
            Attack = 10;
            Defence = 5;
            Speed = 10;
            Experience = 0;
            Gold = 0;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2, 2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            //make sure first point is empty
            this.Room.Empty = true;
            //UIの更新
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();

            //reset at first
            encounter.ResetDynamicControls();
        }

        //level up if enough exp
        public void LevelUp()
        {
            if (Experience >= Level * Level * 10)
            {
                Experience -= Level * Level * 10;
                IncreasedStats[0] = Random.Range(30, 60);
                MaxLife[0] += IncreasedStats[0];
                Life[0] = MaxLife[0];
                Level++;
                for (int x = 1; x < NoOfStats; x++) IncreasedStats[x] = Random.Range(3, 6);
                Attack += IncreasedStats[1];
                Defence += IncreasedStats[2];
                Speed += IncreasedStats[3];
                Energy += IncreasedStats[4];
                Journal.Instance.Log("レベル"+Level+"に上がりました！\n"+"HP+" + IncreasedStats[0] + "/MP+"+ IncreasedStats[4] +"/攻撃+" + IncreasedStats[1] + "/防御+" + IncreasedStats[2] + "/素早さ+" + IncreasedStats[3]);
            }
        }

        public void Move(int direction)
        {
            //prevent it from going down if there is enemy
            /*if (this.Room.Enemy)
            {
                return;
            }*/

            //ｙ＝０で一番上だから
            if(direction == 0&&RoomIndex.y>0)
            {
                RoomIndex -= Vector2.up;
                Journal.Instance.Log("上に動いた");
            }

            //x
            if (direction == 1&& RoomIndex.x < world.Dungeon.GetLength(0) - 1)
            {
                RoomIndex += Vector2.right;
                Journal.Instance.Log("右に動いた");
            }

            //10-1=9
            if (direction == 2&&RoomIndex.y<world.Dungeon.GetLength(1)-1)
            {
                RoomIndex -= Vector2.down;
                Journal.Instance.Log("下に動いた");
            }

            if (direction == 3 && RoomIndex.x > 0)
            {
                RoomIndex += Vector2.left;
                Journal.Instance.Log("左に動いた");
            }

            //only investigate when there is a space to move
            if(this.Room.RoomIndex != RoomIndex)
            {
                Investigate();
            }
            
        }

        //check the room
        public void Investigate()
        {
            
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            encounter.ResetDynamicControls();
            if (this.Room.Empty)
            {
                Journal.Instance.Log("何もないようです。");
            }
            else if (this.Room.Chest != null)
            {
                Journal.Instance.Log("宝箱がありました！どうしますか？");
                encounter.StartChest();
            }
            else if(this.Room.Enemy != null)
            {
                Journal.Instance.Log(Room.Enemy.Description +"が現れた！戦闘開始！");
                //heal enemy before the battle starts, only happens once
                for(int x = 0; x < MaxLife.Length;x++) {
                    this.Room.Enemy.Life[x] = this.Room.Enemy.MaxLife[x];
                }
                
                encounter.StartCombat();
            }else if (this.Room.Exit)
            {
                encounter.Stairs();
                Journal.Instance.Log("階段がありました！下に降りますか？");
            }
        }

        public void AddItem(string item)
        {
            Journal.Instance.Log(item + "を手に入れた。");
            
            Inventory.Add(item);
            UIController.OnPlayerInventoryChange();

        }

        public override void TakeDamage(int amount)
        {
            
            Debug.Log("ダメージを受けた");
            base.TakeDamage(amount);
            UIController.OnPlayerStatChange();
        }

        public override void IntegrateHP()
        {


            base.IntegrateHP();
            UIController.OnPlayerStatChange();
        }

        public override void Die()
        {
            Debug.Log("あなたは死にました");
            
        }
    }
}

