using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class FileIOUtils
    {
        private readonly string PATH;

        public FileIOUtils(string path)
        {
            PATH = path;
        }

        public BindingList<ToDoModel> LoadListData()
        {
            if (!File.Exists(PATH))
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToDoModel>();
            }

            using (var reader = File.OpenText(PATH))
            {
                var infoFromFile = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(infoFromFile);
            }
        }

        public void SaveListData(object todoListData)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string infoToFile = JsonConvert.SerializeObject(todoListData);
                writer.Write(infoToFile);
            }
        }
    }
}