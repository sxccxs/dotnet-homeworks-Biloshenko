using Core.Collections;
using Core.Dataclasses;
using Core.Enumerates;
using System.Drawing;

namespace BLL.Utils
{
    internal class SystemEntriesFormater
    {
        public SystemEntryData[] GetSystemEntryDataForAllEntriess()
        {
            var file = GetSystemEntryDataForFiles();
            var directories = GetSystemEntryDataForDirectories();
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
                .Select(x => ConvertDirectoryInfoToSystemEntryData(x))
                .ToArray();
            return directoriesData;
        }
        private SystemEntryData[] GetSystemEntryDataForFiles()
        {
            FileInfo[] files = Directory.GetFiles(Directory.GetCurrentDirectory())
                               .Select(x => new FileInfo(x))
                               .ToArray();
            SystemEntryData[] directoriesData = files
                .Select(x => ConvertFileInfoToSystemEntryData(x))
                .ToArray();
            return directoriesData;
        }
        private SystemEntryData ConvertFileInfoToSystemEntryData(FileInfo file)
        {
            var data = new SystemEntryData(file.Name,
                                            file.Attributes.HasFlag(FileAttributes.Hidden),
                                            SystemEntryType.File);
            AddFullInfoToSystemEntryDataFromFileInfo(data, file);
            return data;
        }
        private SystemEntryData ConvertDirectoryInfoToSystemEntryData(DirectoryInfo dir)
        {
            var data = new SystemEntryData(dir.Name,
                                            dir.Attributes.HasFlag(FileAttributes.Hidden),
                                            SystemEntryType.Directory);
            AddFullInfoToSystemEntryDataFromDirectoryInfo(data, dir);
            return data;
        }
        private void AddFullInfoToSystemEntryDataFromFileInfo(SystemEntryData data, FileInfo file)
        {

            data.AddField("size", FormatFileSize(file.Length));
            data.AddField("created", file.CreationTime.ToString());
            var fileExtension = file.Extension.Length != 0 ? file.Extension[1..] : file.Extension;
            data.AddField("type", fileExtension);
            foreach (var (k, v) in GetExtraInfoForFileType(file))
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
            string[] imgFileTypes = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };

            string ext = file.Extension;
            var res = new CustomLinkedList<ValueTuple<string, string>>();
            if (imgFileTypes.Contains(ext))
            {
                using (var img = Image.FromFile(file.FullName))
                {
                    res.Add(("Resolution", $"{img.Width}x{img.Height}"));
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
