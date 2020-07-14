using UnityEngine;

namespace Sctipts
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 20f;
        public float bulletSpeed = 10f;
        public float shootingRate = .5f;
        public GameObject bulletPrefab;
        private float minX = -5f;
        private float maxX = 5f;
        private float boardOffset = 1f;
        public AudioClip fireSound;


        void Start()
        {
            //Finding boutders coordinates
            float distance = transform.position.z - Camera.main.transform.position.z;
            Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
            Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
            //Setting minimum and maximum values
            minX = leftMost.x + boardOffset;
            maxX = rightMost.x - boardOffset;
            print("Screen left " + leftMost + "; Screen right " + rightMost);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InvokeRepeating(nameof(Shoot), .00000001f, shootingRate);
            } else if (Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke(nameof(Shoot));
            }
            //* Getting Position
            var position = transform.position;
        
            //Tracking input of Player
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                position += Vector3.left * (speed * Time.deltaTime);
            } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                position += Vector3.right * (speed * Time.deltaTime);
            }
            //Clamp Player position through max and min positions
            position = new Vector3(Mathf.Clamp(position.x, minX, maxX), position.y, position.z);
        
            //* Setting Position
            transform.position = position;
        }

        void Shoot()
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        }
    }
}
