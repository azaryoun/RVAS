using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Security
{
  public struct stcUserInfo
    {
        public int ID;
        public string Username;
        public string FName;
        public string LName;
        public bool? IsDataAdmin;
        public bool? IsItemAdmin;
        public bool? IsMale;
        public string Mobile;
        public byte[] UserPhoto;
        public bool? IsReal;
        public DateTime STime;

    }
}
