using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidRoll
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance;
        public float timer;
        public float destroyTimer;
        public float delaytimer = 1.0f;
        public GameObject[] bars;
        public List<GameObject> list;
        float yPos = -4f;
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }


        public void Spawn()
        {
            int randIndex = Random.Range(0, bars.Length);
            float range = Random.Range(-1.8f, 1.8f);
            yPos -= 1.5f;
            Vector2 pos = new Vector2(range, yPos);
            GameObject go = Instantiate(bars[randIndex], pos, Quaternion.identity);
            //list.Add(go);
        }
        public void POPList()
        {
            //list.len;
        }
    }
}