using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.killClip);
        }
        if(other.gameObject.CompareTag("Ball")) GameManager.Instance.KillBall();
    }
}
