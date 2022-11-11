
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Uthopia;

namespace Uthopia.Games.Simple
{

    public class SimpleGameController : GameController
    {
        public EntityController prefab;
        public float speedScale = 1;
        EntityController m_player;

        List<GameObject> m_createdObjects = new List<GameObject>();


        public override void OnEpisodeBegin()
        {
            m_createdObjects.ForEach(g => ObjectPool.Set("entity", g.gameObject));
            m_createdObjects.Clear();

            var playerPos = Random.insideUnitCircle * 5;
            m_player = ObjectPool.Get("entity", prefab, Random.insideUnitCircle * 5, Quaternion.identity);
            m_player.Reset();
            m_player.SetColor(Color.white);
            m_player.speed = speedScale * 8;

            for (var i = 0; i < 5; i++)
            {
                var randomPos = Random.insideUnitCircle * 20 * speedScale;
                var enemy = ObjectPool.Get("entity", prefab, playerPos + randomPos, Quaternion.identity);
                enemy.Reset();
                enemy.SetColor(Color.red);
                enemy.speed = speedScale * (6 + i);
                enemy.FollowTarget(m_player.transform);
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


            for (var i = 0; i < 1; i++)
            {
                var randomPos = Random.insideUnitCircle * 13 * speedScale;
                var goal = ObjectPool.Get("entity", prefab, playerPos + randomPos, Quaternion.identity);
                goal.Reset();
                goal.SetColor(Color.green);
                goal.speed = speedScale * (6 + i);
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

                m_createdObjects.Add(goal.gameObject);
            }



            m_createdObjects.Add(m_player.gameObject);

            base.OnEpisodeBegin();
        }

        public override void OnInputReceived(InputData input)
        {
            m_player.Move(input.direction);
        }


    }
}