using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QrcodeReader.Models
{
    public class FileSystemClient
    {
        public static FileSystemClient Instance { get; set; } = new FileSystemClient();

        private readonly string _mainDir = FileSystem.AppDataDirectory;
        private readonly string _formFile = "formdata.txt";

        private FileSystemClient() { }

        public void WriteFile(FormData formData)
        {
            var settings = new JsonSerializerSettings
            {
                // メンバ名をキャメルケースで出力するオプション
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            var content = JsonConvert.SerializeObject(formData, settings);
            Debug.WriteLine(content);

            try
            {
                File.WriteAllText(Path.Combine(_mainDir, _formFile), content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        public FormData ReadFile()
        {
            if (File.Exists(Path.Combine(_mainDir, _formFile)))
            {
                var content = File.ReadAllText(Path.Combine(_mainDir, _formFile));
                return JsonConvert.DeserializeObject<FormData>(content);
            }

            return null;
        }
    }
}
