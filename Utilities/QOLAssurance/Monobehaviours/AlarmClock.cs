using UnityEngine.Serialization;

namespace MarrowUtils.Utilities.QolAssurance;

[RegisterTypeInIl2Cpp]
public class AlarmClock : MonoBehaviour
{
    public AudioSource audioSource;

    private readonly int[] _playTimes = { 0, 12 };

    private bool _hasPlayedToday = false;

    private void Update()
    {
        if (!_hasPlayedToday && Array.IndexOf(_playTimes, DateTime.Now.Hour) != -1 && DateTime.Now.Minute == 0)
        {
            audioSource.Play();
            _hasPlayedToday = true;
        }
        
        if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
        {
            _hasPlayedToday = false;
        }
    }
    
    public AlarmClock(IntPtr ptr) : base(ptr) { }
}