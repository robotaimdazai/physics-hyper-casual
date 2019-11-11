using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSpawner : MonoBehaviour
{
    public Queue<GameObject> Pool = new Queue<GameObject>();

    [SerializeField] GameObject hook;
    [SerializeField] Transform levelHolder = null;

    [Header("Spawn Config")]
    [SerializeField] float startX = 13;
    [SerializeField] float startY = 1.7f;
    [SerializeField] float xStepSize = 15;
    [SerializeField] Vector2 hookHeightMinMax;

    float currentX;
    float currentY;
    int poolSize = 30;
    float hookRefreshRatio = 0.6f; // when player reraches near this hook number repostionHooks;
    float garbageHookRatio = 0.4f; // how many hooks to deque
    float estimatedHookPosition = 0f;
    float playerX = 0f;

    private void Awake()
    {
        InitPool();
        
    }

    private void Start()
    {
        currentX = startX;
        currentY = startY;
        PlaceHooks();

        estimatedHookPosition = currentX * hookRefreshRatio;
    }

    void Update()
    {
        playerX = Player.Instance.transform.position.x;

        if (playerX >= estimatedHookPosition)
        {
            RepositionHooks();
            estimatedHookPosition = currentX * hookRefreshRatio;
        }

    }

    void InitPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
           GameObject newHook = Instantiate(hook);
           newHook.transform.SetParent(levelHolder);
           Pool.Enqueue(newHook);
        }
    }

    void PlaceHooks()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject hookToBePlaced = Pool.Dequeue() as GameObject;
            hookToBePlaced.transform.position = new Vector2(currentX, currentY);
            Pool.Enqueue(hookToBePlaced);
            currentX += xStepSize;

            //------Diffculty depends on currentY--------

            currentY = Random.Range(hookHeightMinMax.x, hookHeightMinMax.y);
        }
           
    }

    void RepositionHooks()
    {
        int garbageHookCount = (int) (poolSize * garbageHookRatio);
        for (int i = 0; i < garbageHookCount; i++)
        {
            GameObject garbageHook = Pool.Dequeue() as GameObject;
            Hook gHook = garbageHook.GetComponent<Hook>();
            gHook.IsOn = false;
            garbageHook.transform.position = new Vector2(currentX, currentY);
            Pool.Enqueue(garbageHook);
            currentX += xStepSize;
        }

    }

    public void Reset()
    {
        currentX = startX;
        currentY = startY;
        PlaceHooks();
        estimatedHookPosition = currentX * hookRefreshRatio;
    }
}
