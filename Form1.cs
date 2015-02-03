using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GlobusLib;
using System.Net;
using System.IO;
using System.Drawing.Imaging;


namespace ImageCrawler
{
    public partial class Form1 : Form
    {
        GlobusHttpHelper httpHelper;
        GlobusRegex globusRegex;

        List<string> lst = new List<string>();
        List<string> lst1 = new List<string>();

        public Form1()
        {
            httpHelper = new GlobusHttpHelper();
            globusRegex = new GlobusRegex();
            InitializeComponent();
        }
           private string imageUrl;
          private Bitmap bitmap;
          public Form1(string imageUrl)
          {
              this.imageUrl = "http://lurieunaward.com/1st_Alfredo_Sabat_cartoon2006.jpg";
          }
          public void Download() {
            try {
              WebClient client = new WebClient();
              Stream stream = client.OpenRead("http://lurieunaward.com/1st_Alfredo_Sabat_cartoon2006.jpg");
              bitmap = new Bitmap(stream);
              stream.Flush();
              stream.Close();
            }
            catch (Exception e) {
              Console.WriteLine(e.Message);
            }
          }
          public Bitmap GetImage() {
            return bitmap;
          }

          public void SaveImage() {
            if (bitmap != null) {
                bitmap.Save(@"F:\E\Curren_Running_Projects\WindowApplication\", ImageFormat.Jpeg);
            }
          }


        private void button1_Click(object sender, EventArgs e)
        {
            WebClient Client = new WebClient();
            Client.DownloadFile("http://lurieunaward.com/1st_Alfredo_Sabat_cartoon2006.jpg", "F:\\1st_Alfredo_Sabat_cartoon2006.jpg");

            Download();
            GetImage();
            SaveImage();
            //string str = "http://www.wallcoo.net/cartoon/index4.htm";

            //string rs = httpHelper.getHtmlfromUrl(new Uri(str));

            //lst = GetSourceTags(rs);
            //lst1 = globusRegex.GetUrlsFromString(rs);

        }

        public List<string> GetSourceTags(string HtmlData)
        {
            List<string> lstAnchorUrl = new List<string>();

            Regex anchorTextExtractor = new Regex(@"src=[""'](?<url>[^""^']+[.]*)[""'].*>");
            foreach (Match url in anchorTextExtractor.Matches(HtmlData))
            {
                if (url.Value.Contains(".jpg"))
                {
                    lstAnchorUrl.Add(url.Value);
                }
            }

            //String url = @"<img src=""angry.gif"" alt=""Angry face"" />";
            //String pattern = @"<img\ssrc=""(?<pic>[^""]*)""\salt=""(?<alt>[^""]*)";
            //Regex rx = new Regex(pattern);
            //Match m = rx.Match(HtmlData);
            //if (m.Success)
            //{
            //    String newUrl = @"<a ref=""${pic}"" rel=""${alt}";
            //    String output = rx.Replace(url, newUrl);
            //    Console.WriteLine(output);
            //}

            return lstAnchorUrl;
        }
    }
}
