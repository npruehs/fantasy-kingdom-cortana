// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreasurySerializer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CortanaGameSample.IO
{
    using System;
    using System.Threading.Tasks;

    using Windows.Storage;

    using CortanaGameSample.Model;

    public class TreasurySerializer
    {
        private const string FileName = "Treasury.dat";

        public async void Save(Treasury treasury)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var sampleFile =
                await
                    storageFolder.CreateFileAsync(FileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            using (var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                    {
                        dataWriter.WriteInt32(treasury.Gold);

                        await dataWriter.StoreAsync();
                    }
                }
            }
        }

        public async Task<Treasury> Load()
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var sampleFile = await storageFolder.TryGetItemAsync(FileName) as StorageFile;

            if (sampleFile == null)
            {
                return null;
            }

            using (var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                var size = stream.Size;

                if (size <= 0)
                {
                    return null;
                }

                var treasury = new Treasury();

                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                    {
                        await dataReader.LoadAsync((uint)size);

                        treasury.Gold = dataReader.ReadInt32();

                        return treasury;
                    }
                }
            }
        }
    }
}