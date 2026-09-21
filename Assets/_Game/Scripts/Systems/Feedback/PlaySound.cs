using UnityEngine;

// PlaySound：播放音效（需要物体上有 AudioSource）
[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    public AudioClip clip; // 留空则播放 AudioSource 里自带的 Clip

    // 由 UnityEvent 调用
    public void Play()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source == null) return;

        if (clip != null) source.PlayOneShot(clip);
        else source.Play();
    }
}
