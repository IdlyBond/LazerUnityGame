using System;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Sctipts
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        private float xMaxScreen;
        private float xMinScreen;
        public float formationWidth = 2;
        public float formationHeight = 3;
        public float speed = 15;
        private bool moveRight = true;
        public float spawnDelay = 0.5f;
        
        void Start()
        {
            //Setting World Boundaries 
            float zToCamera = transform.position.z - Camera.main.transform.position.z;
            Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zToCamera));
            Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zToCamera));
            xMaxScreen = rightBoundary.x;
            xMinScreen = leftBoundary.x;
            
            SpawnEnemiesUntilFull();

        }

        private void Update()
        {
            CoordinateMoving();
            if (AllMembersAreDead())
            {
                SpawnEnemiesUntilFull();
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(formationWidth, formationHeight));
        }

        private void SpawnEnemiesUntilFull()
        {
            Transform freePosition = NextFreePosition();
            if (freePosition)
            {
                var enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity);
                enemy.transform.parent = freePosition.transform;
            }

            if (NextFreePosition())
            {
                Invoke(nameof(SpawnEnemiesUntilFull), spawnDelay);
            }
            
        }
        private void SpawnEnemiesOnPositions()
        {
            foreach (Transform item in transform)
            {
                var enemy = Instantiate(enemyPrefab, item.transform.position, Quaternion.identity);
                enemy.transform.parent = item.transform;
            }
        }

        private void CoordinateMoving()
        {
            if (!moveRight)
            {
                transform.position += Vector3.left * (speed * Time.deltaTime);
            } else if (moveRight)
            {
                transform.position += Vector3.right * (speed * Time.deltaTime);
            }
            float rightEdgeOfFormation = transform.position.x + (0.5f * formationWidth);
            float leftEdgeOfFormation = transform.position.x - (0.5f * formationWidth);
            if (leftEdgeOfFormation < xMinScreen)
            {
                moveRight = true;
            }  else if (rightEdgeOfFormation > xMaxScreen)
            {
                moveRight = false;
            }
        }

        Transform NextFreePosition()
        {
            foreach (Transform child in transform)
            {
                if (child.childCount == 0)
                {
                    return child;
                }
            }
            return null;
        }

        bool AllMembersAreDead()
        {
            foreach (Transform child in transform)
            {
                if (child.childCount > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    
}
