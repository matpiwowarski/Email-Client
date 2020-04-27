using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace E_mail_Client.Model
{
    public class Folder : List<Mail>
    {
        public FolderType Type;
        public Folder()
        {

        }
        public Folder(FolderType type)
        {
            Type = type;
        }
    }
}
