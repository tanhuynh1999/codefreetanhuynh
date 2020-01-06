using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiaSeCode_TH.Models
{
    public class QuenMatKhau
    {
        public int MaQuenMatKhau { get; set; }
        public string EmailNhan { get; set; }
        public string ChuDe { get; set; }
        public string NoiDung { get; set; }
        public string Bcc { get; set; }
        public string Cc { get; set; }
    }
}