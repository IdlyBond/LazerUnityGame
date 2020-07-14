using UnityEngine;

namespace Sctipts
{
    public class Position : MonoBehaviour
    {
        public float radius = 1;
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
