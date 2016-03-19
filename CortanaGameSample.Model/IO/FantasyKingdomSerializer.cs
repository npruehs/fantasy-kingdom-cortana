// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FantasyKingdomSerializer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CortanaGameSample.IO
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Windows.Storage;
    using Windows.Storage.Streams;

    using CortanaGameSample.Model;

    public class FantasyKingdomSerializer
    {
        public void Save<T>(T data)
        {
            var fileName = this.GetFileName<T>();
            this.Save(fileName, data);
        }

        public async void Save<T>(string fileName, T data)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var sampleFile =
                await
                    storageFolder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            using (var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        StringBuilder stringBuilder = new StringBuilder();
                        StringWriter stringWriter = new StringWriter(stringBuilder);
                        xmlSerializer.Serialize(stringWriter, data);

                        dataWriter.WriteString(stringBuilder.ToString());

                        await dataWriter.StoreAsync();
                    }
                }
            }
        }

        private string GetFileName<T>()
        {
            return string.Format("{0}.xml", typeof(T).Name);
        }

        public async Task<T> Load<T>()
        {
            var fileName = this.GetFileName<T>();
            return await this.Load<T>(fileName);
        }

        public async Task<T> Load<T>(string filename)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var sampleFile = await storageFolder.TryGetItemAsync(filename) as StorageFile;

            if (sampleFile == null)
            {
                return default(T);
            }

            using (var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                uint size = (uint)stream.Size;

                if (size <= 0)
                {
                    return default(T);
                }

                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                    {
                        await dataReader.LoadAsync(size);

                        var dataString = dataReader.ReadString(size);

                        var xmlSerializer = new XmlSerializer(typeof(T));
                        var stringReader = new StringReader(dataString);
                        var data = (T)xmlSerializer.Deserialize(stringReader);

                        return data;
                    }
                }
            }
        }
    }
}