using Core.Collections;
using Core.Enumerates;

namespace Core.Dataclasses
{
    public class SystemEntryData
    {
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public bool ShowFullInfo { get; set; }
        public SystemEntryType Type { get; }
        public CustomDictionary<string, string> Fields { get; }

        public SystemEntryData(string name, bool isHidden, SystemEntryType type)
        {
            Name = name;
            IsHidden = isHidden;
            Type = type;
            ShowFullInfo = false;
            Fields = new CustomDictionary<string, string>();
        }

        public void AddField(string key, string value)
        {
            Fields[key] = value;
        }
    }
}
