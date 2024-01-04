using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Scripts
{
    public interface ILocalization
    {
        string GetLocalizedWord(string key);
    }

    public class MockLocalization : ILocalization
    {
        private readonly List<string> words = new() { "hund", "katt", "fisk", "bilk", "husk" };
        private readonly System.Random random = new();


        public string GetLocalizedWord(string key)
        {
            return words[random.Next(words.Count)];
        }
    }

    public interface ISerializer
    {
        void Serialize();
    }

    public class MockSerializer : ISerializer
    {
        public void Serialize()
        {
            Debug.Log("MockSerializer.Serialize");
        }
    }

    public interface IAudioService
    {
        void Play();
    }

    public class MockAudioService : IAudioService
    {
        public void Play()
        {
            Debug.Log("MockAudioService.Play");
        }
    }

    public interface IGameService
    {
        void StartGame();
    }

    public class MockMapService : IGameService
    {
        public void StartGame()
        {
            Debug.Log("MockMapService.StartGame");
        }
    }
}