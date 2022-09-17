using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyAI
{
    public class PipeHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject pipePrefab;

        [Header("Settings")]
        [SerializeField] private float gapSize = 4f;
        [SerializeField] private float secondsBetweenSpawns = 2f;

        private float spawnTimer;
        private float centerHeight;

        private readonly List<GameObject> pipes = new List<GameObject>();

        private void Update()
        {
            RemoveOldPipes();
            SpawnNewPipes();
        }

        public void ResetPipes()
        {
            foreach (GameObject pipe in pipes)
            {
                Destroy(pipe);
            }

            pipes.Clear();

            spawnTimer = 0f;
        }

        private void RemoveOldPipes()
        {
            for (int i = pipes.Count - 1; i >= 0; i--)
            {
                if (pipes[i].transform.position.x > 15f)
                {
                    Destroy(pipes[i]);

                    pipes.RemoveAt(i);
                }
            }
        }

        private void SpawnNewPipes()
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer > 0f) { return; }

            GameObject topPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f, 0f, 180f));
            GameObject bottomPipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);

            centerHeight = UnityEngine.Random.Range(-1.5f, 4f);

            topPipe.transform.Translate(Vector3.up * (centerHeight + (gapSize / 2)), Space.World);
            bottomPipe.transform.Translate(Vector3.up * (centerHeight - (gapSize / 2)), Space.World);

            pipes.Add(topPipe);
            pipes.Add(bottomPipe);

            spawnTimer = secondsBetweenSpawns;
        }
    }
}
