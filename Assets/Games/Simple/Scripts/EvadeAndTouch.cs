
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Uthopia;

namespace Uthopia.Games.Simple
{

    public class EvadeAndTouch : Game
    {
        public EntityController prefab;
        public float speedScale = 1;
        public int minNumberOfEnemies = 2;
        public int maxNumberOfEnemies = 2;
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
            StopAllCoroutines();


            m_createdObjects.ForEach(g => ObjectPool.Set("entity", g.gameObject));
            m_createdObjects.Clear();


            // Spawn player
            var playerPos = Random.insideUnitCircle * 5;
            m_player = ObjectPool.Get("entity", prefab, Random.insideUnitCircle * 5, Quaternion.identity);
            m_player.Reset();
            m_player.SetColor(Color.white);
            m_player.speed = speedScale * 8;
            var totalEnemies = Random.Range(minNumberOfEnemies, maxNumberOfEnemies+1);

            // Spawn enemys
            for (var i = 0; i < totalEnemies; i++)
            {
                var randomPos = Random.insideUnitCircle * 20;
                var enemy = ObjectPool.Get("entity", prefab, playerPos + randomPos, Quaternion.identity);
                enemy.Reset();
                enemy.SetColor(Color.red);
                enemy.speed = speedScale * (Random.Range(3f,8f));
                StartCoroutine(StartFollow(enemy, m_player.transform, 2));
                enemy.onCollisionEnter.RemoveAllListeners();
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
            }

            // Spawn goal
            for (var i = 0; i < 1; i++)
            {
                var randomPos = Random.insideUnitCircle * 10 * speedScale;
                var goal = ObjectPool.Get("entity", prefab, playerPos + randomPos, Quaternion.identity);
                goal.Reset();
                goal.SetColor(Color.green);
                goal.onCollisionEnter.RemoveAllListeners();
                goal.onCollisionEnter.AddListener(
                    collision =>
                    {
                        if (collision.gameObject == m_player.gameObject)
                        {
                            Win();
                        }
                    }
                    );

                goal.transform.position = goal.position.InsideSquareBounds(15);
                m_createdObjects.Add(goal.gameObject);
            }
            m_createdObjects.Add(m_player.gameObject);
        }

        public override void OnInputReceived(InputData input)
        {
            m_player.Move(input.direction);
        }

        IEnumerator StartFollow(EntityController entity, Transform target, float t) {

            yield return new WaitForSeconds(t);
            entity.FollowTarget(target);
        }

        public override InputActionMask GetInputMask()
        {
            return new InputActionMask(action5:false, action6: false, action7: false, action8: false);
        }
    }
}