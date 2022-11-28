
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Uthopia;

namespace Uthopia.Games.Simple
{

    public class CrossStreet : Game
    {
        public EntityController prefab;
        public float speedScale = 1;
        EntityController m_player;

        List<GameObject> m_createdObjects = new List<GameObject>();


        private void Update()
        {
            if (m_player)
            {
                m_player.transform.position = m_player.position.InsideSquareBounds(15);
            }
        }


        public override void OnEpisodeBegin()
        {
            m_createdObjects.ForEach(g => ObjectPool.Set("entity", g.gameObject));
            m_createdObjects.Clear();
            StopAllCoroutines();

            // Spawn player
            var playerPos = new Vector2(Random.Range(0, 15), -14);
            m_player = ObjectPool.Get("entity", prefab, playerPos, Quaternion.identity, name: "Player");
            m_player.Reset();
            m_player.SetColor(Color.white);
            m_player.speed = speedScale * 8;

            int ways = Random.Range(4,8);
            for (var i = 0; i < ways; i++)
            {
                StartCoroutine(SpawnEnenyCoroutine((-ways / 2 + i) * 2, Random.Range(0, 2) % 2 == 1));
            }

            var goal = CreateGoal(15f);

            goal.onCollisionEnter.AddListener(
               collision =>
               {
                   if (collision.gameObject == m_player.gameObject)
                   {
                       ObjectPool.Set("entity", goal.gameObject);

                       var secondGoal = CreateGoal(-15f);
                       secondGoal.onCollisionEnter.AddListener(
                         collision =>
                         {
                             if (collision.gameObject == m_player.gameObject)
                             {
                                 Win();
                             }
                         }
                         );

                       m_createdObjects.Add(secondGoal.gameObject);
                   }
               }
               );
            

            m_createdObjects.Add(goal.gameObject);
            m_createdObjects.Add(m_player.gameObject);
        }

        public override void OnInputReceived(InputData input)
        {
            m_player.Move(input.direction);
        }

        EntityController CreateGoal(float y)
        {

            var goal = ObjectPool.Get("entity", prefab, new Vector2(0, y), Quaternion.identity, name: "Goal");
            goal.Reset();
            goal.SetColor(Color.green);
            goal.transform.localScale = new Vector3(50, 1, 1);

            m_createdObjects.Add(goal.gameObject);

            return goal;

        }

        IEnumerator SpawnEnenyCoroutine(float y, bool toLeft)
        {

            bool firstTime = true;
            while (true)
            {
                var time = Random.Range(1f, 7f);

                var enemy = ObjectPool.Get("entity", prefab, firstTime ? new Vector2(Random.Range(-15f, 15f), y) : new Vector2(toLeft ? 15 : -15, y), Quaternion.identity, name: "Enemy");
                firstTime = false;
                enemy.Reset();
                enemy.SetColor(Color.red);
                enemy.speed = speedScale * (3 + Random.Range(0, 7));
                enemy.transform.localScale = new Vector3(Random.Range(1, 3), 1, 1);
                enemy.FollowPosition(new Vector2(toLeft ? -15 : 15, y), () =>
                {
                    ObjectPool.Set("entity", enemy.gameObject);
                });


                enemy.onCollisionEnter.AddListener(
                    collision =>
                    {
                        if (collision.gameObject == m_player.gameObject)
                        {
                            Lose();
                        }
                    }
                    );

                m_createdObjects.Add(enemy.gameObject);
                yield return new WaitForSeconds(time);
            }


        }

        public override InputActionMask GetInputMask()
        {
            return new InputActionMask(action5: false, action6: false, action7: false, action8: false);
        }

    }
}