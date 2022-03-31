using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New MinigameLevel", menuName ="MiniLevel")]
public class MiniGameLevels :ScriptableObject
{
    public new string name;

    public List<Transform> levelNodes;
}
