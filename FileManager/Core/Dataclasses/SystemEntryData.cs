using Core.Collections;
using Core.Enumerates;

namespace Core.DataClasses
{
    public class SystemEntryData
    {
        public SystemEntryData(string name, bool isHidden, SystemEntryType type)
        {
            this.Name = name;
            this.IsHidden = isHidden;
            this.Type = type;
            this.ShowFullInfo = false;
            this.Fields = new CustomDictionary<string, string>();
        }

        public string Name { get; set; }

        public bool IsHidden { get; set; }

        public bool ShowFullInfo { get; set; }

        public SystemEntryType Type { get; }

        public CustomDictionary<string, string> Fields { get; }

        public void AddField(string key, string value)
        {
            this.Fields[key] = value;
        }
    }
}
