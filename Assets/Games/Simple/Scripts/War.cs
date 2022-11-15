
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Uthopia;

namespace Uthopia.Games.Simple
{

    public class War : GameController
    {
        public EntityController prefab;
        public float speedScale = 1;
        EntityController m_player;

        List<EntityController> teamL = new List<EntityController>();
        List<EntityController> teamR = new List<EntityController>();
        List<EntityController> allies = new List<EntityController>();
        List<EntityController> enemies = new List<EntityController>();


        List<GameObject> m_createdObjects = new List<GameObject>();

        bool m_isWar;


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
            teamL.Clear();
            teamR.Clear();
            allies.Clear();
            enemies.Clear();
            m_isWar = false;

            StopAllCoroutines();

            // Spawn player
            var playerPos = new Vector2(0, 0);
            m_player = ObjectPool.Get("entity", prefab, playerPos, Quaternion.identity, name: "Player");
            m_player.Reset();
            m_player.SetColor(Color.white);
            m_player.speed = speedScale * 8;

            m_player.onCollisionEnter.AddListener(
                c =>
                {
                    var e = c.gameObject.GetComponent<EntityController>();
                    if (teamL.Contains(e))
                    {
                        allies = teamL.ToList();
                        enemies = teamR.ToList();
                    }
                    else
                    {
                        allies = teamR.ToList();
                        enemies = teamL.ToList();
                    }

                    allies.Add(m_player);

                    m_player.onCollisionEnter.RemoveAllListeners();
                    StartCoroutine(FindTargetForAllCoroutine());
                    m_isWar = true;
                }
                );


            CreateTeam("Left", Color.blue, new Rect(new Vector2(-14, -14), new Vector2(10, 29)), teamL);
            CreateTeam("Right", Color.yellow, new Rect(new Vector2(4, -14), new Vector2(10, 29)), teamR);



            m_createdObjects.Add(m_player.gameObject);
        }

        public override void OnInputReceived(InputData input)
        {
            m_player.Move(input.direction);
        }


        void CreateTeam(string team, Color color, Rect spawnArea, List<EntityController> list)
        {
            var size = Random.Range(2, 9);
            for (var i = 0; i < size; i++)
            {

                Debug.DrawLine(new Vector2(spawnArea.x, spawnArea.y), new Vector2(spawnArea.x + spawnArea.width, spawnArea.y + spawnArea.height));
                var pos = new Vector2(Random.Range(spawnArea.x, spawnArea.x + spawnArea.width), Random.Range(spawnArea.y, spawnArea.y + spawnArea.height));

                var entity = ObjectPool.Get("entity", prefab, pos, Quaternion.identity, name: $"Team {team}");
                entity.Reset();
                entity.SetColor(color);
                entity.speed = Random.Range(4, 7) * speedScale;

                entity.onCollisionEnter.AddListener(
                    (c) =>
                    {
                        if (m_isWar)
                            Attack(entity, c.GetComponent<EntityController>());
                    }
                    );

                list.Add(entity);
                m_createdObjects.Add(entity.gameObject);
            }
        }

        void Attack(EntityController a, EntityController b)
        {


            if (allies.Contains(a) != allies.Contains(b))
            {

                if (a == m_player || b == m_player)
                {
                    Lose();
                    return;

                }

                ObjectPool.Set("entity", a.gameObject);
                ObjectPool.Set("entity", b.gameObject);

                allies.Remove(a);
                allies.Remove(a);
                enemies.Remove(b);
                enemies.Remove(b);
            };


            if (allies.Count == 1 && allies.Contains(m_player))
                Lose();
            else
            if (enemies.Count == 0)
                Win();

        }



        IEnumerator FindTargetForAllCoroutine()
        {
            while (true)
            {
                foreach (var e in allies)
                {
                    var t = enemies.OrderBy((en) => (en.transform.position - e.transform.position).magnitude).First();
                    if (e != m_player)
                        e.FollowTarget(t.transform);
                }

                foreach (var e in enemies)
                {
                    var t = allies.OrderBy((a) => (a.transform.position - e.transform.position).magnitude).First();
                    e.FollowTarget(t.transform);
                }

                yield return new WaitForSeconds(1f);
            }
        }

    }
}