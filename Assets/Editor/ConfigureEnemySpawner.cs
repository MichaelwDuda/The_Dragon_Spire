using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class ConfigureEnemySpawner
{
    private const string ScenePath = "Assets/Scenes/SampleScene.unity";
    private const string DemonPrefabPath = "Assets/enemy/DemonEnemy.prefab";

    public static void Run()
    {
        EditorSceneManager.OpenScene(ScenePath);

        GameObject spawnerObject = GameObject.Find("Enemy Spawner");
        if (spawnerObject == null)
        {
            spawnerObject = new GameObject("Enemy Spawner");
        }

        EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
        if (spawner == null)
        {
            spawner = spawnerObject.AddComponent<EnemySpawner>();
        }

        GameObject demonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(DemonPrefabPath);
        if (demonPrefab == null)
        {
            throw new System.InvalidOperationException("Could not find DemonEnemy prefab at " + DemonPrefabPath);
        }

        Transform waypoint0 = RequireTransform("waypoint_0");
        Transform waypoint1 = RequireTransform("waypoint_1");
        Transform waypoint2 = RequireTransform("waypoint_2");
        GameObject home = RequireGameObject("home");

        HomeTowerHealth homeHealth = home.GetComponent<HomeTowerHealth>();
        if (homeHealth == null)
        {
            homeHealth = home.AddComponent<HomeTowerHealth>();
        }

        spawner.transform.position = waypoint0.position;
        spawner.spawnPoint = waypoint0;
        spawner.waypoints = new[] { waypoint0, waypoint1, waypoint2 };
        spawner.homeTower = homeHealth;
        spawner.startOnPlay = true;
        spawner.waves = new[]
        {
            new EnemySpawner.Wave
            {
                enemyPrefab = demonPrefab,
                enemyCount = 3,
                spawnDelay = 1f,
                timeBeforeNextWave = 3f
            }
        };

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        Debug.Log("Configured Enemy Spawner with DemonEnemy in " + ScenePath);
    }

    private static GameObject RequireGameObject(string name)
    {
        GameObject found = GameObject.Find(name);
        if (found == null)
        {
            throw new System.InvalidOperationException("Could not find scene object named " + name);
        }

        return found;
    }

    private static Transform RequireTransform(string name)
    {
        return RequireGameObject(name).transform;
    }
}
