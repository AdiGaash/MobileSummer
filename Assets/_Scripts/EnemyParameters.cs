using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "EnemyScriptable", order = 0)]
    public class EnemyParameters : ScriptableObject
    {
        public float speed =5;
        public int hp = 10;
    }
}