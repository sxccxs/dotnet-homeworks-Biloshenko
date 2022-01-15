using System.Drawing;
using Core.Collections;
using Core.DataClasses;
using Core.Enumerates;

namespace BLL.Utilities
{
    internal class SystemEntriesFormatter
    {
        public SystemEntryData[] GetSystemEntryDataForAllEntries()
        {
            var file = this.GetSystemEntryDataForFiles();
            var directories = this.GetSystemEntryDataForDirectories();

            return file.Concat(directories).ToArray();
        }

        public SystemEntryData[] FilterSystemEntryDataByFlag(SystemEntryData[] data, SystemEntryType flag)
        {
            return data.Where(x => x.Type == flag)
                       .ToArray();
        }

        private SystemEntryData[] GetSystemEntryDataForDirectories()
        {
            DirectoryInfo[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory())
                                                   .Select(x => new DirectoryInfo(x))
                                                   .ToArray();
            SystemEntryData[] directoriesData = directories
                .Select(x => this.ConvertDirectoryInfoToSystemEntryData(x))
                .ToArray();

            return directoriesData;
        }

        private SystemEntryData[] GetSystemEntryDataForFiles()
        {
            FileInfo[] files = Directory.GetFiles(Directory.GetCurrentDirectory())
                               .Select(x => new FileInfo(x))
                               .ToArray();
            SystemEntryData[] directoriesData = files
                .Select(x => this.ConvertFileInfoToSystemEntryData(x))
                .ToArray();

            return directoriesData;
        }

        private SystemEntryData ConvertFileInfoToSystemEntryData(FileInfo file)
        {
            var data = new SystemEntryData(
                file.Name,
                file.Attributes.HasFlag(FileAttributes.Hidden),
                SystemEntryType.File);

            this.AddFullInfoToSystemEntryDataFromFileInfo(data, file);

            return data;
        }

        private SystemEntryData ConvertDirectoryInfoToSystemEntryData(DirectoryInfo dir)
        {
            var data = new SystemEntryData(
                dir.Name,
                dir.Attributes.HasFlag(FileAttributes.Hidden),
                SystemEntryType.Directory);
            this.AddFullInfoToSystemEntryDataFromDirectoryInfo(data, dir);

            return data;
        }

        private void AddFullInfoToSystemEntryDataFromFileInfo(SystemEntryData data, FileInfo file)
        {
            data.AddField("size", this.FormatFileSize(file.Length));
            data.AddField("created", file.CreationTime.ToString());
            var fileExtension = file.Extension.Length != 0 ? file.Extension[1..] : file.Extension;
            data.AddField("type", fileExtension);
            foreach (var (k, v) in this.GetExtraInfoForFileType(file))
            {
                data.AddField(k, v);
            }
        }

        private void AddFullInfoToSystemEntryDataFromDirectoryInfo(SystemEntryData data, DirectoryInfo dir)
        {
            data.AddField("Created at", dir.CreationTime.ToString());
        }

        private CustomLinkedList<ValueTuple<string, string>> GetExtraInfoForFileType(FileInfo file)
        {
            string[] imageFileTypes = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };

            string ext = file.Extension;
            var res = new CustomLinkedList<ValueTuple<string, string>>();
            if (imageFileTypes.Contains(ext))
            {
                using (var image = Image.FromFile(file.FullName))
                {
                    res.Add(("Resolution", $"{image.Width}x{image.Height}"));
                }
            }

            return res;
        }

        private string FormatFileSize(long len)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len} {sizes[order]}";
        }
    }
}
