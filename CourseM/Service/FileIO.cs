using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace CourseM.Service
{
    class FileIO
    {
        private readonly string PATH;

        public FileIO(string path)
        {
            PATH = path;
        }

        public BindingList<Client> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<Client>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<Client>>(fileText);
            }
        }

        public void SaveData(BindingList<Client> clients)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(clients);
                writer.Write(output);
            }
        }
    }
}
