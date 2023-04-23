using UnityEngine;


namespace PII.Dungeon
{
    /// <summary>
    /// Monobehaviour representing a door, it has 2 boxcolliders
    ///     - one for opening the doors when the player enters it
    ///     - one for preventing the player from going forward
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        private BoxCollider2D trigger;
        [SerializeField] private BoxCollider2D boxCollider;
        private Animator animator;
        void Awake()
        {
            animator = GetComponent<Animator>();
            trigger = GetComponent<BoxCollider2D>();

            boxCollider.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Settings.PLAYER_TAG))
                OpenDoor();
        }
        public void OpenDoor()
        {
            trigger.enabled = false;
            boxCollider.enabled = false;
            animator.SetBool(Settings.doorOpen, true);
        }
        public void LockDoor()
        {
            boxCollider.enabled = true;
            trigger.enabled = false;
            animator.SetBool(Settings.doorOpen, false);
        }
        public void UnlockDoor()
        {
            boxCollider.enabled = false;
            trigger.enabled = true;
        }



    }
}