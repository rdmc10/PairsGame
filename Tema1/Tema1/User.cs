using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Tema1
{
    internal class User
    {
        public string UserName { get; set; }
        public UInt16 UserImageIndex { get; set; }

        public User(string username, UInt16 userimgindex)
        {
            this.UserName = username;
            this.UserImageIndex = userimgindex;
        }

    }
}
