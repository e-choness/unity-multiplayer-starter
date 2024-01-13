using UnityEngine;

namespace kart.Kart.Scripts.DebugLog
{
    [System.Serializable]
    public class JsonTests : MonoBehaviour
    {
        public int Id;
        public string Name;
        public float Value;
        // Start is called before the first frame update
        void Start()
        {

            Id = 1;
            Name = "One";
            Value = 1.1f;
            
            var js = ConvertToJson();
            
            var anotherJson = "{\"name\":\"Dr Charles\",\"lives\":3,\"health\":0.8}";
            BackToSelf(anotherJson);
        }

        string ConvertToJson()
        {
            var jsonFromObj = JsonUtility.ToJson(this);
            Debug.Log($"Object to json: {jsonFromObj}");
            return jsonFromObj;
        }

        void BackToSelf(string json)
        {
            // var self = JsonUtility.FromJson()
            var self = JsonUtility.FromJson<JsonTests>(json);
            Debug.Log($"Converted data id: {self.Id} name: {self.Name} value: {self.Value}");
        }
    }
}
