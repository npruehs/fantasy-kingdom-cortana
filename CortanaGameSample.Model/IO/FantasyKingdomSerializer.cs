namespace CortanaGameSample.IO
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Windows.Storage;
    using Windows.Storage.Streams;

    public class FantasyKingdomSerializer
    {
        #region Public Methods and Operators

        public async Task<T> Load<T>()
        {
            var fileName = this.GetFileName<T>();
            return await this.Load<T>(fileName);
        }

        public async Task<T> Load<T>(string filename)
        {
            // Get local storage.
            var storageFolder = ApplicationData.Current.LocalFolder;

            // Open file.
            var dataFile = await storageFolder.TryGetItemAsync(filename) as StorageFile;

            if (dataFile == null)
            {
                return default(T);
            }

            using (var stream = await dataFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // Check if any data present.
                var size = (uint)stream.Size;

                if (size <= 0)
                {
                    return default(T);
                }

                // Read data.
                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new DataReader(inputStream))
                    {
                        await dataReader.LoadAsync(size);

                        var dataString = dataReader.ReadString(size);

                        // Convert XML to model.
                        var xmlSerializer = new XmlSerializer(typeof(T));
                        var stringReader = new StringReader(dataString);
                        var data = (T)xmlSerializer.Deserialize(stringReader);

                        return data;
                    }
                }
            }
        }

        public void Save<T>(T data)
        {
            var fileName = this.GetFileName<T>();
            this.Save(fileName, data);
        }

        public async void Save<T>(string fileName, T data)
        {
            // Get local storage.
            var storageFolder = ApplicationData.Current.LocalFolder;

            // Create file.
            var dataFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            using (var stream = await dataFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new DataWriter(outputStream))
                    {
                        // Convert model to XML.
                        var xmlSerializer = new XmlSerializer(typeof(T));
                        var stringBuilder = new StringBuilder();
                        var stringWriter = new StringWriter(stringBuilder);
                        xmlSerializer.Serialize(stringWriter, data);

                        dataWriter.WriteString(stringBuilder.ToString());

                        await dataWriter.StoreAsync();
                    }
                }
            }
        }

        #endregion

        #region Methods

        private string GetFileName<T>()
        {
            return string.Format("{0}.xml", typeof(T).Name);
        }

        #endregion
    }
}