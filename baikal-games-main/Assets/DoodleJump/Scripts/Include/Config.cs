using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DoodleJump
{
    public class Config : MonoBehaviour
    {
        public class Data
        {
            public float DoodleJumpSensivity = 150f;
        }

        public Data Load()
        {
            var path = Path.Combine(Application.dataPath, "config.txt");
            if (File.Exists(path))
            {
                string text;

                {
                    text = File.ReadAllText(path);
                }

                Data data;

                try
                {
                    data = JsonUtility.FromJson<Data>(text);
                }

                catch (Exception e)
                {
                    return SaveFile(path);
                }

                if (data == null)
                    return SaveFile(path);

                return data;
            }
            else
            {
                return SaveFile(path);
            }
        }

        public Data SaveFile(string path)
        {
            var newData = new Data();

            File.WriteAllText(path, JsonUtility.ToJson(newData));

            return newData;
        }
    }
}
