using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE310_Lab3
{
    [Serializable]
    class SerializableSave
    {
        public string Length { get; set; }
        public string Width { get; set; }
        public SerializableSave() : this("0", "0")
        {
        }//End RecordSerializable
        public SerializableSave (string length, string width)
        {
            Length = length;
            Width = width;
        }//End SerializableSave
    }
}
