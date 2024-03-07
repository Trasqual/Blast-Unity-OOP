namespace BlastGame.AudioManagement
{
    public struct AudioData
    {
        public AudioType AudioType;
        public float Pitch;
        public float Volume;
        public bool Looping;

        public static AudioData Create(AudioType audioType)
        {
            return new AudioData()
            {
                AudioType = audioType,
                Pitch = 1,
                Volume = 1,
                Looping = false
            };
        }

        public AudioData WithPitch(float pitch)
        {
            Pitch = pitch;
            return this;
        }

        public AudioData WithVolume(float volume)
        {
            Volume = volume;
            return this;
        }

        public AudioData IsLooping(bool looping)
        {
            Looping = looping;
            return this;
        }
    }
}
