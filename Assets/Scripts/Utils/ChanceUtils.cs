using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ChanceUtils
    {
        public static bool HitChance(float chance)
        {
            if (chance < 0f || chance > 1f)
            {
                throw new System.ArgumentException($"chance should be in range from 0 to 1 but was {chance}");
            }
            else
            {
                return Random.value > chance;
            }
        }
    }
}
