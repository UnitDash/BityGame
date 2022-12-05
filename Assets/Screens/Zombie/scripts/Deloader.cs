using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deloader : MonoBehaviour
{
    public static Deloader Instance;

    private List<GameObject> Trash;

    private void Awake()
    {
        Instance = this;
        Trash = new List<GameObject>();
    }

    private void DeloadAll()
    {
        for(int i = 0; i < Trash.Count; i++)
        {
            Destroy(Trash[i]);
        }
    }

    public void AddToTrash(GameObject T)
    {
        Trash.Add(T);
    }

    private void OnDestroy()
    {
        DeloadAll();
    }
}
