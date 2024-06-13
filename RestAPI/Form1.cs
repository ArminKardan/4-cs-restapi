using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //https://irmapserver.ir/ex/users.json

        private void btnGo_Click(object sender, EventArgs e)
        {
            var b = Utilities.HttpPost("https://irmapserver.ir/ex/users.json", new byte[] { });
            var s = Encoding.UTF8.GetString(b);
            var arr = JSONResearch.Parse(s);

            List<dynamic> list = new List<dynamic>();

            foreach(var item in arr)
            {
                if(item["score"] >= 19)
                {
                    list.Add(item);
                }
            }

            list.Sort((x, y) => (int)(x["score"]*10 - y["score"]*10));

            var dic = new Dictionary<string, dynamic>();
            dic.Add("name", "Armin");
            dic.Add("id", "1500");
            dic.Add("obj", list);

            var objsend = new Dictionary<string, dynamic>();
            objsend.Add("student", dic);

            var ss = JSONResearch.Stringify(objsend,true);

            var bb = Encoding.UTF8.GetBytes(ss);

            var res = Utilities.HttpPost("https://xtal.ir/api/cs/ex", bb);

            MessageBox.Show(Encoding.UTF8.GetString(res));

        }
    }
}
