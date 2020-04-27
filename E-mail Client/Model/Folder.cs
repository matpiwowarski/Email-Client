using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace E_mail_Client.Model
{
    [Serializable()]
    public class Folder : List<Mail>, ISerializable
    {
        public FolderType Type;
        public Folder()
        {

        }
        public Folder(FolderType type)
        {
            Type = type;
        }

        public Folder(SerializationInfo info, StreamingContext context)
        {
            Type = (FolderType)info.GetValue("FolderType", typeof(FolderType));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FolderType", Type);
        }
    }
}
