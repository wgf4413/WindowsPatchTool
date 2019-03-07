using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using WUApiLib;

namespace WindowsPatchTool
{
    public partial class frmPatch : Form
    {
        private Dictionary<string, string> _dictCab;
        private StringBuilder _sbSame;
        public frmPatch()
        {
            InitializeComponent();
            _dictCab = new Dictionary<string, string>();
            _sbSame = new StringBuilder();
            _hStop = new EventWaitHandle(false, EventResetMode.ManualReset);
            _hProc = new EventWaitHandle(false, EventResetMode.ManualReset);
            _queue = new Queue<FeProc>();
            threadBaseStart();
            tbCondition.Text = "IsInstalled = 1";
            tbPatchPath.Text = Path.Combine(Application.StartupPath, "patch");
            tbImagePath.Text = "d:\\win";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string file_updates = DateTime.Now.ToString("yyyyMMddHHmmss")+"_updates.txt";
            /*
            _dictCab.Clear();
            _sbSame = new StringBuilder();
            string pre = DateTime.Now.ToString("yyyyMMddHHmmss");
            string file_hotfix_dism = pre + "_query_hotfix_dism.txt";
            string file_hotfix_Info = pre + "_query_hotfix_info.txt";
            string file_hotfix_Same = pre + "_query_hotfix_same.txt";

            StringBuilder sbDism = new StringBuilder();
            StringBuilder sbInfo = new StringBuilder();
            string hotPath = "C:\\test\\hot";
            string imgPath = "D:\\win7";
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string condition = "IsInstalled = 0";
            //string condition = "CategoryIDs contains 'bfe5b177-a086-47a0-b102-097e4fa1f807'";
            
            sbInfo.AppendFormat("{0} {1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), condition);
            textBox1.Text = sbInfo.ToString();

            IUpdateSearcher uSearcher = new UpdateSearcherClass();
            ISearchResult sr = uSearcher.Search(condition);
            TimeSpan ts1 = sw.Elapsed;
            sbInfo.AppendFormat("{0} searcher:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), ts1.TotalSeconds);
            textBox1.Text = sbInfo.ToString();
            */

            string condition = "IsInstalled = 0";
            //string condition = "CategoryIDs contains 'bfe5b177-a086-47a0-b102-097e4fa1f807'";
            IUpdateSearcher uSearcher = new UpdateSearcherClass();
            ISearchResult sr = uSearcher.Search(condition);
            if (uSearcher.Online)
            {
                int cnt = 0;
                List<FeUpdate> updateList = new List<FeUpdate>();
                foreach (IUpdate item in sr.Updates)
                {
                    FeUpdate update = new FeUpdate();
                    update.PutIUpdate(item);
                    updateList.Add(update);

                    /*
                    string updateSize = GetUpdateSize(item.MinDownloadSize, item.MaxDownloadSize);

                    string tmpDir = item.LastDeploymentChangeTime.ToString("yyyyMMdd") + "_" + getTitleKbNumber(item.Title);
                    string rootDir = Path.Combine(hotPath, tmpDir);
                    try
                    {
                        RecursionUpdate("", sbInfo, sbDism, item, rootDir);
                    }
                    catch (Exception ex)
                    {
                        sbInfo.AppendFormat("RecursionUpdate is exception:{0}\r\n", ex.ToString());
                        break;
                    }

                    sbInfo.AppendLine("======================================================================================");

                    cnt++;
                    this.Text = cnt.ToString();
                    */
                }
                //ShowUpdate(tvUpdates, updateList);
                //JsonConvert.SerializeObject(updateList);
                /*
                sw.Stop();
                sbInfo.AppendFormat("{0} TotalSeconds:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.TotalSeconds);

                SaveFile(sbDism, file_hotfix_dism);
                SaveFile(sbInfo, file_hotfix_Info);
                SaveFile(_sbSame, file_hotfix_Same);

                textBox1.Text = sbInfo.ToString();
                textBox2.Text = sbDism.ToString();
                */
                return;
            }
            textBox1.Text = "offline";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            IUpdateSearcher uSearcher = new UpdateSearcherClass();

            StringBuilder sb = new StringBuilder();
            if (uSearcher.Online)
            {
                int hisCount = uSearcher.GetTotalHistoryCount();
                sb.AppendFormat("Total History Count:{0}\r\n", hisCount.ToString());
                IUpdateHistoryEntryCollection uphEntires = uSearcher.QueryHistory(0, hisCount);
                int cnt = 0;
                foreach (IUpdateHistoryEntry2 entry2 in uphEntires)
                {
                    //if (entry2.Title.Contains("KB2267602")) continue; //Windows Defender Antivirus 定义更新
                    //if (entry2.Title.Contains("KB890830")) continue;  //Windows 恶意软件删除工具 x64
                    
                    foreach (ICategory category in entry2.Categories)
                    {
                        sb.AppendFormat("Category CategoryID:{0}\r\n", category.CategoryID);
                        sb.AppendFormat("Category Name:{0}\r\n", category.Name);
                        sb.AppendFormat("Category Parent:{0}\r\n", category.Parent);
                        sb.AppendFormat("Category Description:{0}\r\n", category.Description);
                        sb.AppendFormat("Category Type:{0}\r\n", category.Type);
                    }
                    
                    sb.AppendFormat("ClientApplicationID:{0}\r\n", entry2.ClientApplicationID);
                    sb.AppendFormat("Operation:{0}\r\n", entry2.Operation.ToString());
                    sb.AppendFormat("HResult:{0}\r\n", entry2.HResult.ToString());
                    sb.AppendFormat("ResultCode:{0}\r\n", entry2.ResultCode);
                    sb.AppendFormat("ServerSelection:{0}\r\n", entry2.ServerSelection);
                    sb.AppendFormat("ServiceID:{0}\r\n", entry2.ServiceID);
                    sb.AppendFormat("UninstallationNotes:{0}\r\n", entry2.UninstallationNotes);
                    sb.AppendFormat("UpdateIdentity.RevisionNumber:{0}\r\n", entry2.UpdateIdentity.RevisionNumber.ToString());
                    sb.AppendFormat("UpdateIdentity.UpdateID:{0}\r\n", entry2.UpdateIdentity.UpdateID);
                    sb.AppendFormat("Title:{0}\r\n", entry2.Title);
                    sb.AppendFormat("Description:{0}\r\n", entry2.Description);
                    sb.AppendFormat("SupportUrl:{0}\r\n", entry2.SupportUrl);
                    sb.AppendFormat("Date:{0}\r\n", entry2.Date.ToShortDateString());
                    sb.AppendLine("");
                    cnt++;
                }
                sb.AppendLine("cnt=" + cnt.ToString());

                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                sb.AppendFormat("TotalSeconds:{0}\r\n", ts.TotalSeconds.ToString());
                sb.AppendLine("");

                textBox1.Text = sb.ToString();
                return;
            }
            textBox1.Text = "offline";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pre = DateTime.Now.ToString("yyyyMMddHHmmss");
            string file_hotfix_dism = pre + "_" + tbUpdateID.Text + "_hotfix_dism.txt";
            string file_hotfix_Info = pre + "_" + tbUpdateID.Text + "_hotfix_info.txt";

            StringBuilder sbDism = new StringBuilder();
            StringBuilder sbInfo = new StringBuilder();
            string hotPath = "D:\\hot";
            string imgPath = "D:\\win7";
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string condition = string.Format("UpdateID='{0}'", tbUpdateID.Text);
            sbInfo.AppendFormat("{0} {1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), condition);
            textBox1.Text = sbInfo.ToString();

            IUpdateSearcher uSearcher = new UpdateSearcherClass();
            ISearchResult sr = uSearcher.Search(condition);
            TimeSpan ts1 = sw.Elapsed;
            sbInfo.AppendFormat("{0} searcher:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), ts1.TotalSeconds);
            textBox1.Text = sbInfo.ToString();

            
            if (uSearcher.Online)
            {
                IUpdateDownloader ud = new UpdateDownloaderClass();
                ud.Updates = sr.Updates;
                IDownloadResult downRst = ud.Download();
                TimeSpan ts2 = sw.Elapsed;
                sbInfo.AppendFormat("{0} download:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.Subtract(ts1).TotalSeconds);
                sbInfo.AppendLine("");

                string prefix;
                int updateIdx = 0;
                foreach (IUpdate item in sr.Updates)
                {
                    string updateSize = GetUpdateSize(item.MinDownloadSize, item.MaxDownloadSize);

                    string tmpDir = item.LastDeploymentChangeTime.ToString("yyyyMMdd") + "_" + getTitleKbNumber(item.Title);

                    UpdateInfo("", sbInfo, sbDism, item,null);

                    TimeSpan ts3 = sw.Elapsed;
                    int bundIdx = 0;
                    foreach (IUpdate2 bund in item.BundledUpdates)
                    {
                        string strIdx = string.Format("[IDX:{0:000}-{1:00}]", updateIdx, bundIdx);

                        string tmpPath = Path.Combine(hotPath, tmpDir + "_" + bundIdx.ToString());

                        sbInfo.AppendFormat("{0}path:{1}\r\n", strIdx, tmpPath);

                        prefix = string.Format("  Bundled[{0}] ", bundIdx);
                        UpdateInfo(prefix, sbInfo, sbDism, bund, null);

                        if (Directory.Exists(tmpPath)) 
                        {
                            sbInfo.AppendFormat("{0}Directory is exists:{1}\r\n", prefix, tmpPath);
                            continue;
                        }
                        Directory.CreateDirectory(tmpPath);
                        try
                        { 
                            bund.CopyFromCache(tmpPath, false);
                        }
                        catch (Exception ex)
                        {
                            sbInfo.AppendFormat("{0}CopyFromCache is exception:{1}\r\n", prefix, ex.ToString());
                        }

                        List<string> files = GetHotfixFileList(tmpPath);
                        sbDism.AppendFormat("rem {0} {1} {2} {3}\r\n", strIdx, item.LastDeploymentChangeTime.ToLongDateString(), updateSize, item.Title);
                        foreach (var file in files)
                        {
                            sbDism.AppendFormat("dism /image:{0} /add-package /packagepath:{1}\r\n", imgPath, Path.Combine(tmpPath, file));
                        }
                        sbDism.AppendFormat("dism /image:{0} /add-package /packagepath:{1}\r\n", imgPath, tmpPath);
                        
                        bundIdx++;
                    }
                    
                    sbInfo.AppendLine("");
                    sbInfo.AppendFormat("{0} CopyFromCache:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.Subtract(ts3).TotalSeconds);
                    sbInfo.AppendLine("======================================================================================");


                    SaveFile(sbDism, file_hotfix_dism);
                    SaveFile(sbInfo, file_hotfix_Info);
                    updateIdx++;
                }
                sw.Stop();
                sbInfo.AppendFormat("{0} TotalSeconds:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.TotalSeconds);

                textBox1.Text = sbInfo.ToString();
                tbDetail.Text = sbDism.ToString();
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string pre = DateTime.Now.ToString("yyyyMMddHHmmss");
            string file_hotfix_dism = pre + "_all_hotfix_dism.txt";
            string file_hotfix_Info = pre + "_all_hotfix_info.txt";

            StringBuilder sbDism = new StringBuilder();
            StringBuilder sbInfo = new StringBuilder();
            string hotPath = "D:\\hot";
            string imgPath = "D:\\win7";
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string condition = "IsInstalled = 0";
            sbInfo.AppendFormat("{0} {1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), condition);
            textBox1.Text = sbInfo.ToString();

            IUpdateSearcher uSearcher = new UpdateSearcherClass();
            ISearchResult sr = uSearcher.Search(condition);
            TimeSpan ts1 = sw.Elapsed;
            sbInfo.AppendFormat("{0} searcher:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), ts1.TotalSeconds);
            textBox1.Text = sbInfo.ToString();


            if (uSearcher.Online)
            {
                IUpdateDownloader ud = new UpdateDownloaderClass();
                ud.Updates = sr.Updates;
                IDownloadResult downRst = ud.Download();
                TimeSpan ts2 = sw.Elapsed;
                sbInfo.AppendFormat("{0} download:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.Subtract(ts1).TotalSeconds);
                sbInfo.AppendLine("");

                string prefix;
                int updateIdx = 0;
                foreach (IUpdate item in sr.Updates)
                {
                    textBox1.Text = sbInfo.ToString();
                    tbDetail.Text = sbDism.ToString();

                    string updateSize = GetUpdateSize(item.MinDownloadSize, item.MaxDownloadSize);

                    string tmpDir = item.LastDeploymentChangeTime.ToString("yyyyMMdd") + "_" + getTitleKbNumber(item.Title);

                    UpdateInfo("", sbInfo, sbDism, item, null);

                    TimeSpan ts3 = sw.Elapsed;
                    int bundIdx = 0;
                    foreach (IUpdate2 bund in item.BundledUpdates)
                    {
                        string strIdx = string.Format("[IDX:{0:000}-{1:00}]", updateIdx, bundIdx);

                        string tmpPath = Path.Combine(hotPath, tmpDir + "_" + bundIdx.ToString());

                        sbInfo.AppendFormat("{0}path:{1}\r\n", strIdx, tmpPath);

                        prefix = string.Format("  Bundled[{0}] ", bundIdx);
                        UpdateInfo(prefix, sbInfo, sbDism, bund, null);

                        if (Directory.Exists(tmpPath))
                        {
                            sbInfo.AppendFormat("{0}Directory is exists:{1}\r\n", prefix, tmpPath);
                            continue;
                        }
                        Directory.CreateDirectory(tmpPath);
                        try
                        {
                            bund.CopyFromCache(tmpPath, false);
                        }
                        catch (Exception ex)
                        {
                            sbInfo.AppendFormat("{0}CopyFromCache is exception:{1}\r\n", prefix, ex.ToString());
                        }

                        List<string> files = GetHotfixFileList(tmpPath);
                        sbDism.AppendFormat("rem {0} {1} {2} {3}\r\n", strIdx, item.LastDeploymentChangeTime.ToLongDateString(), updateSize, item.Title);
                        foreach (var file in files)
                        {
                            sbDism.AppendFormat("dism /image:{0} /add-package /packagepath:{1}\r\n", imgPath, Path.Combine(tmpPath, file));
                        }
                        sbDism.AppendFormat("dism /image:{0} /add-package /packagepath:{1}\r\n", imgPath, tmpPath);

                        bundIdx++;
                    }

                    sbInfo.AppendLine("");
                    sbInfo.AppendFormat("{0} CopyFromCache:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.Subtract(ts3).TotalSeconds);
                    sbInfo.AppendLine("======================================================================================");


                    SaveFile(sbDism, file_hotfix_dism);
                    SaveFile(sbInfo, file_hotfix_Info);
                    updateIdx++;
                }
                sw.Stop();
                sbInfo.AppendFormat("{0} TotalSeconds:{1:0000.000}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), sw.Elapsed.TotalSeconds);

                textBox1.Text = sbInfo.ToString();
                tbDetail.Text = sbDism.ToString();
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Student st = new Student("wgf");
            st.ID = 1001;
            st.Sex = "男";
            st.Age = 33;
            List<Student> stList = new List<Student>();
            stList.Add(st);
            st = new Student("222");
            st.ID = 1002;
            st.Sex = "男";
            st.Age = 34;
            stList.Add(st);
            FeUpdate fu = new FeUpdate();
            string sJson = JsonConvert.SerializeObject(stList, Formatting.Indented);
            List<Student> st1 = JsonConvert.DeserializeObject<List<Student>>(sJson);
            textBox1.Text = sJson;
            
        }

        private string GetFileKbNumber(string value)
        { //windows6.1-kb2852386-x64-express.cab
            value = value.ToUpper();
            int p1 = value.IndexOf("KB");
            if (p1 == -1) return "";
            value = value.Substring(p1, value.Length - p1);
            p1 = value.IndexOf('-');
            if (p1 == -1) return "";
            value = value.Substring(0, p1);
            return value;
        }
        private string getTitleKbNumber(string value)
        {
            int p1 = value.IndexOf('(');
            int p2 = value.IndexOf(')');
            if ((p1 != -1) && (p2 != -1))
            {
                return value.Substring(p1 + 1, p2 - p1 - 1);
            }
            return "KB00";
        }

        private string GetUpdateSize(decimal minDownloadSize, decimal maxDownloadSize)
        {
            decimal den = 1024;
            string ustr = "KB";
            if (maxDownloadSize > 1024 * 1024)
            {
                den = 1024 * 1024;
                ustr = "MB";
            }
            string updateSize;
            if ((maxDownloadSize == minDownloadSize) || (string.Format("{0:N2}", minDownloadSize) == "0.00"))
            {
                updateSize = string.Format("{0:N2}{1}", maxDownloadSize / den, ustr);
            }
            else
            {
                updateSize = string.Format("{0:N2}-{1:N2}{2}", minDownloadSize / den, maxDownloadSize / den, ustr);
            }
            return updateSize;
        }
        private List<string> GetHotfixFileList(string path)
        {
            List<string> retList = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo nFile in dir.GetFiles())
            {
                string ext = nFile.Extension.ToLower();
                if (ext == ".cab")
                {
                    retList.Add(nFile.Name);
                }
                if (ext == ".exe")
                {
                    retList.Add(nFile.Name);
                }
            }
            return retList;
        }
        private void SaveFile(StringBuilder sb, string fileName)
        {
            FileStream fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            fs.Seek(0, SeekOrigin.Begin);
            byte[] data = Encoding.Default.GetBytes(sb.ToString());
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }
        private void SaveFile(string data, string fileName)
        {
            FileStream fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            fs.Seek(0, SeekOrigin.Begin);
            byte[] bytes = Encoding.Default.GetBytes(data);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }
        private void LoadFile(out string data, string fileName)
        {
            FileStream fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            fs.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            data = Encoding.Default.GetString(bytes);
            fs.Close();
        }


        private string GetUrlFile(string url)
        {
            int p = url.LastIndexOf('/') + 1;
            return url.Substring(p, url.Length - p);
        }
        private void RecursionUpdate(string prefix, StringBuilder sbInfo, StringBuilder sbDism, IUpdate item, string downPath)
        {
            UpdateInfo(prefix, sbInfo, sbDism, item, downPath);
            string subPath = Path.Combine(downPath, item.LastDeploymentChangeTime.ToString("yyyyMMdd") + "_" + GetUpdateSize(item.MinDownloadSize, item.MaxDownloadSize));
            foreach (IUpdate bund in item.BundledUpdates)
            {
                prefix = "  " + prefix;
                RecursionUpdate(prefix, sbInfo, sbDism, bund, subPath);
            }
        }
        private void UpdateInfo(string prefix, StringBuilder sbInfo, StringBuilder sbDism, IUpdate item, string downPath)
        {
            string updateSize = GetUpdateSize(item.MinDownloadSize, item.MaxDownloadSize);
            sbInfo.AppendFormat("{0}LastDeploymentChangeTime:{1} Size:{2}\r\n", prefix, item.LastDeploymentChangeTime.ToLongDateString(), updateSize);
            sbInfo.AppendFormat("{0}Title:{1}\r\n", prefix, item.Title);
            sbInfo.AppendFormat("{0}Description:{1}\r\n", prefix, item.Description);
            sbInfo.AppendFormat("{0}UpdateIdentity.RevisionNumber:{1}\r\n", prefix, item.Identity.RevisionNumber.ToString());
            sbInfo.AppendFormat("{0}UpdateIdentity.UpdateID:{1}\r\n", prefix, item.Identity.UpdateID);
            sbInfo.AppendFormat("{0}HandlerID:{1}\r\n", prefix, item.HandlerID);
            sbInfo.AppendFormat("{0}IsBeta:{1}\r\n", prefix, item.IsBeta);
            sbInfo.AppendFormat("{0}IsDownloaded:{1}\r\n", prefix, item.IsDownloaded);
            sbInfo.AppendFormat("{0}IsHidden:{1}\r\n", prefix, item.IsHidden);
            sbInfo.AppendFormat("{0}IsInstalled:{1}\r\n", prefix, item.IsInstalled);
            sbInfo.AppendFormat("{0}IsMandatory:{1}\r\n", prefix, item.IsMandatory);
            sbInfo.AppendFormat("{0}IsUninstallable:{1}\r\n", prefix, item.IsUninstallable);
            sbInfo.AppendFormat("{0}ReleaseNotes:{1}\r\n", prefix, item.ReleaseNotes);
            sbInfo.AppendFormat("{0}Type:{1}\r\n", prefix, item.Type);
            foreach (ICategory category in item.Categories)
            {
                sbInfo.AppendFormat("{0}Category CategoryID:{1}\r\n", prefix, category.CategoryID);
                sbInfo.AppendFormat("{0}Category Name:{1}\r\n", prefix, category.Name);
                sbInfo.AppendFormat("{0}Category Parent:{1}\r\n", prefix, category.Parent);
                sbInfo.AppendFormat("{0}Category Description:{1}\r\n", prefix, category.Description);
                sbInfo.AppendFormat("{0}Category Type:{1}\r\n", prefix, category.Type);
            }
            for (int i = 0; i < item.SupersededUpdateIDs.Count; i++)
            {
                sbInfo.AppendFormat("{0}SupersededUpdateID[{1}]:{2}\r\n", prefix, i, item.SupersededUpdateIDs[i]);
            }
            for (int i = 0; i < item.SecurityBulletinIDs.Count; i++)
            {
                sbInfo.AppendFormat("{0}SecurityBulletinID[{1}]:{2}\r\n", prefix, i, item.SecurityBulletinIDs[i]);
            }
            for (int i = 0; i < item.Languages.Count; i++)
            {
                sbInfo.AppendFormat("{0}Language[{1}]:{2}\r\n", prefix, i, item.Languages[i]);
            }
            for (int i = 0; i < item.KBArticleIDs.Count; i++)
            {
                sbInfo.AppendFormat("{0}kbArticleId[{1}]:{2}\r\n", prefix, i, item.KBArticleIDs[i]);
            }
            for (int i = 0; i < item.DownloadContents.Count; i++)
            {
                string url = item.DownloadContents[i].DownloadUrl;
                sbInfo.AppendFormat("{0}DownloadContentsUrl[{1}]:{2}\r\n", prefix, i, url);
                if (!Directory.Exists(downPath)) Directory.CreateDirectory(downPath);

                string imgPath = "D:\\win7";
                string urlFile = GetUrlFile(url);
                if (_dictCab.ContainsKey(urlFile))
                {
                    _sbSame.AppendFormat("{0}  {1}\r\n", item.Identity.UpdateID, url);
                }
                else
                {
                    _dictCab.Add(urlFile, "");
                }
                string fullFile = Path.Combine(downPath, urlFile);
                string ext = Path.GetExtension(urlFile).ToLower();
                if (!File.Exists(fullFile))
                {
                    if (ext != ".psf")
                    {
                        DownloadUrl(item.DownloadContents[i].DownloadUrl, downPath);
                    }
                }
                if (File.Exists(fullFile))
                {
                    if (ext == ".cab")
                    {
                        FileInfo fi = new FileInfo(fullFile);
                        if (updateSize == GetUpdateSize(0, fi.Length))
                            sbDism.AppendFormat("{0}dism /image:{1} /add-package /packagepath:{2}\r\n", prefix, imgPath, fullFile);
                    }
                }
                this.Text = item.DownloadContents[i].DownloadUrl;
            }
        }
        private string DownloadUrl(string url, string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string file = GetUrlFile(url);
            string full = Path.Combine(path, file);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            Stream stream = new FileStream(full, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            return file;       
        }
        private string GetSizeText(long size)
        { 
            const int kb = 1024;
            const int mb = 1024 * kb;
            if (size<kb) return string.Format("{0} B", size);
            if ((size >= kb) && (size < mb)) return string.Format("{0:N2} K", size * 1.0 / 1024);
            return string.Format("{0:N2} M", size * 1.0 / mb);
        }
        private void SyncDownloadUrl(object obj)
        {
            string url = (string)(((object[])obj)[0]);
            string path = (string)(((object[])obj)[1]);
            FeDownloadContent download = (FeDownloadContent)(((object[])obj)[2]);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string file = GetUrlFile(url);
            string full = Path.Combine(path, file);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            //long fileSize = responseStream.Length;
            long fileSize = 0;
            Stream stream = new FileStream(full, FileMode.Create);
            byte[] bArr = new byte[1024];
            int downSize = 0;
            int diff = 0;
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            string msg = "";
            ShowStart(download);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
                downSize += size;
                diff += size;
                if (diff > 1024 * 1024)
                {
                    ShowInfo(downSize, download);
                    diff = 0;
                }
                fileSize += size;
            }
            ShowInfo(downSize, download);
            ShowEnd(download);
            download.FileSize = fileSize;
            stream.Close();
            responseStream.Close();
        }

        public delegate void ShowInfoDelegate(long downSize, FeDownloadContent download);
        private void ShowStart(FeDownloadContent download)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<FeDownloadContent>(ShowStart), new object[] { download });
                return;
            }
            TreeNode node = download.Node;
            node.ForeColor = Color.RoyalBlue;
        }
        private void ShowInfo(long downSize, FeDownloadContent download)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { downSize, download });
                return;
            }
            TreeNode node = download.Node;
            node.Text = string.Format("{0} {1}", GetSizeText(downSize), download.DownloadUrl);
        }
        private void ShowEnd(FeDownloadContent download)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<FeDownloadContent>(ShowEnd), new object[] { download });
                return;
            }
            TreeNode node = download.Node;
            node.ForeColor = Color.Green;
        }

        private FeUpdates _updates;
        private EventWaitHandle _hStop;
        private EventWaitHandle _hProc;
        private Queue<FeProc> _queue;
        private Thread _threadBase;
        private void threadBaseStart()
        {
            _threadBase = new Thread(delegate() { threadBaseProc(); });
            _threadBase.IsBackground = true;
            _threadBase.Name = "threadProc";
            _threadBase.Start();
        }
        private void threadBaseProc()
        {
            WaitHandle[] handls = { _hStop, _hProc };
            while (true)
            {
                int iret = WaitHandle.WaitAny(handls);
                if (iret == 0) { break; }
                if (iret != 1) { break; }
                while(true)
                {
                    FeProc proc;
                    lock (_queue)
                    {
                        if (_queue.Count == 0) break;
                        proc = _queue.Dequeue();
                    }
                    proc.Do(proc.obj);
                }
                _hProc.Reset();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _updates = new FeUpdates();
            string condition = "IsInstalled = 1";
            if (tbCondition.Text.Trim() != "") condition = tbCondition.Text.Trim();
            IUpdateSearcher uSearcher = new UpdateSearcherClass();
            ISearchResult sr = uSearcher.Search(condition);
            if (!uSearcher.Online) return;

            foreach (IUpdate item in sr.Updates)
            {
                _updates.PutIUpdate(item);
            }
            _updates.ShowUpdate(tvUpdates, tbPatchPath.Text);

            //保存
            string json = JsonConvert.SerializeObject(_updates, Formatting.Indented);
            string updateFile = "updates_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            this.SaveFile(json, updateFile);
            tbUpdateFile.Text = updateFile;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt文件(*.txt)|*.txt|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                string data;
                LoadFile(out data, fileName);
                _updates = JsonConvert.DeserializeObject<FeUpdates>(data);
                _updates.RelationObject();
                _updates.ShowUpdate(tvUpdates, tbPatchPath.Text);

                tbUpdateFile.Text = Path.GetFileName(ofd.FileName);
            }
        }

        private void menuShowDetail_Click(object sender, EventArgs e)
        {
            TreeNode node = tvUpdates.SelectedNode;
            if (node == null) return;
            Type tp = node.Tag.GetType();
            if (tp.Name != "FeUpdate") return;
            FeUpdate update = (FeUpdate)node.Tag;
            StringBuilder sb = new StringBuilder();
            update.InfoToStringBuilder(sb);
            tbDetail.Text = sb.ToString();
        }

        private void menuDownload_Click(object sender, EventArgs e)
        {
            TreeNode node = tvUpdates.SelectedNode;
            if (node == null) return;
            Type tp = node.Tag.GetType();
            if (tp.Name != "FeDownloadContent") return;
            FeDownloadContent item = (FeDownloadContent)(node.Tag);
            string tmp1 = item.Update.GetUpdateDir();
            string tmp2 = tbPatchPath.Text.Trim();
            string patchPath = Path.Combine(tmp2, tmp1);
            string url = item.DownloadUrl;
            object[] obj = new object[3];
            obj[0] = (object)(url);
            obj[1] = (object)(patchPath);
            obj[2] = (object)item;
            lock (_queue)
            {
                FeProc proc = new FeProc();
                proc.Do = SyncDownloadUrl;
                proc.obj = obj;
                _queue.Enqueue(proc);
                _hProc.Set();
            }
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(_updates,Formatting.Indented);
            string updateFile = tbUpdateFile.Text;
            this.SaveFile(json, updateFile);
        }

        private void tvUpdates_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Type tp = e.Node.Tag.GetType();
            if (tp.Name != "FeDownloadContent") return;
            FeDownloadContent item = (FeDownloadContent)(e.Node.Tag);
            item.IsSelect = e.Node.Checked;
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvUpdates.ExpandAll();
        }

        private void reduceAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvUpdates.CollapseAll();
        }

        private void buildDismToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patch = tbPatchPath.Text.Trim();
            string image = tbImagePath.Text.Trim();
            StringBuilder sbDism = new StringBuilder();
            foreach (var item in _updates.DownDict)
            {
                FeDownloadContent down = item.Value;
                if ((down.FiExt == ".cab")&&(down.IsSelect))
                {
                    string tmpPath = Path.Combine(patch, down.Update.GetUpdateDir());
                    string tmpFull = Path.Combine(tmpPath, down.FileName);
                    sbDism.AppendFormat("dism /image:{0} /add-package /packagepath:{1}\r\n", image, tmpFull);
                }
            }
            string dismFile = "dism_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            this.SaveFile(sbDism, dismFile);
        }

        private void downloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in _updates.DownDict)
            {
                FeDownloadContent down = item.Value;
                if (down.IsDown) continue;
                if (!down.IsSelect) continue;
                string tmp1 = down.Update.GetUpdateDir();
                string tmp2 = tbPatchPath.Text.Trim();
                string patchPath = Path.Combine(tmp2, tmp1);
                string url = down.DownloadUrl;
                object[] obj = new object[3];
                obj[0] = (object)(url);
                obj[1] = (object)(patchPath);
                obj[2] = (object)down;
                lock (_queue)
                {
                    FeProc proc = new FeProc();
                    proc.Do = SyncDownloadUrl;
                    proc.obj = obj;
                    _queue.Enqueue(proc);
                    _hProc.Set();
                }
            }
        }

        private void dismSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string root = tbPatchPath.Text;
            _updates.DimsSelect(root);
        }
    }
    /*
    public class BoolConverter:JsonConverter 
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? "1" : "0");
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        { 
            return reader.Value.ToString()=="1"; 
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
  */
    public class FeProc
    {
        public Action<object> Do;
        public object obj;
    }
    public class FeCategory
    {
        public string CategoryID;
        public string Description;
        public string Name;
        public int Order;
        public string Type;
    }
    public class FeDownloadContent
    {
        private string _downloadUrl;
        private string _fileName;
        private string _fiName;
        private string _fiExt;
        private TreeNode _node;
        private FeUpdate _update;
        private bool _isDown;

        //[JsonConverter(typeof(BoolConverter))] 
        public bool IsSelect{get;set;}
        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set
            {
                _downloadUrl = value;
                _fileName = GetUrlFile(_downloadUrl);
                _fiName = Path.GetFileNameWithoutExtension(_fileName);
                _fiExt = Path.GetExtension(_fileName).ToLower();
            }
        }
        public long FileSize;
        [JsonIgnoreAttribute]
        public string FileName
        {
            get { return _fileName; }
        }
        [JsonIgnoreAttribute]
        public string FiName
        {
            get { return _fiName; }
        }
        [JsonIgnoreAttribute]
        public string FiExt
        {
            get { return _fiExt; }
        }
        [JsonIgnoreAttribute]
        public TreeNode Node
        {
            get { return _node; }
            set { _node = value; }
        }
        [JsonIgnoreAttribute]
        public FeUpdate Update
        {
            get { return _update; }
            set { _update = value; }
        }
        [JsonIgnoreAttribute]
        public bool IsDown
        {
            get { return _isDown; }
        }

        public void SetPatchRoot(string root)
        {
            string tmpPath = Path.Combine(root, _update.GetUpdateDir());
            //if (!Directory.Exists(tmpPath)) Directory.CreateDirectory(tmpPath);
            string tmpFile = Path.Combine(tmpPath, _fileName);
            _isDown = File.Exists(tmpFile);
            if (_isDown)
            {
                _node.ForeColor = Color.Black;
                FileInfo fi = new FileInfo(tmpFile);
                _node.Text = _update.GetUpdateSize(0, fi.Length) + " " + this.DownloadUrl;
            }
            else
                _node.ForeColor = Color.Crimson;

        }
        public void DimsSelect(string root)
        {
            string st1 = _update.UpdateSize;
            string tmpPath = Path.Combine(root, _update.GetUpdateDir());
            string tmpFile = Path.Combine(tmpPath, _fileName);
            if (!File.Exists(tmpFile)) return;

            FileInfo fi = new FileInfo(tmpFile);
            string st2 = _update.GetUpdateSize(0, fi.Length);
            this.IsSelect = st1 == st2;
            this.Node.Checked = this.IsSelect;
        }
        private string GetUrlFile(string url)
        {
            int p = url.LastIndexOf('/') + 1;
            return url.Substring(p, url.Length - p);
        }
    }
    public class FeInstallationBehavior
    {
        public bool CanRequestUserInput;
        public InstallationImpact Impact;
        public InstallationRebootBehavior RebootBehavior;
        public bool RequiresNetworkConnectivity;

        public void PutBehavior(IInstallationBehavior iv)
        {
            if (iv == null) return;
            CanRequestUserInput = iv.CanRequestUserInput;
            Impact = iv.Impact;
            RebootBehavior = iv.RebootBehavior;
            RequiresNetworkConnectivity = iv.RequiresNetworkConnectivity;
        }
    }
    public class FeUpdates
    {
        private List<FeUpdate> _updateList;
        private Dictionary<string, FeDownloadContent> _downDict;
        public FeUpdates()
        {
            _updateList = new List<FeUpdate>();
            _downDict = new Dictionary<string, FeDownloadContent>();
        }

        public List<FeUpdate> List
        {
            get { return _updateList; }
        }

        [JsonIgnoreAttribute]
        public Dictionary<string, FeDownloadContent> DownDict
        {
            get { return _downDict; }
        }

        public void PutIUpdate(IUpdate item)
        {
            FeUpdate update = new FeUpdate();
            update.PutIUpdate(item);
            _updateList.Add(update);
        }
        public void ShowUpdate(TreeView tv, string PatchPath)
        {
            tv.Nodes.Clear(); 
            foreach (FeUpdate update in _updateList)
            {
                update.ShowToNodes(tv.Nodes);
                update.SetPatchRoot(PatchPath);
                update.BuildDictDown(_downDict);
            }
        }
        public void RelationObject()
        {
            foreach (var item in _updateList)
            {
                item.RelationObject();
            }
        }
        public void DimsSelect(string root)
        {
            foreach (var item in _updateList)
            {
                item.DimsSelect(root);
            }
        }
    }
    public class FeUpdate
    {
        public FeUpdate()
        {
            Updates = new List<FeUpdate>();
            Categories = new List<FeCategory>();
            DownloadContents = new List<FeDownloadContent>();
            InstallationBehavior = new FeInstallationBehavior();
            KBArticleIDs = new List<string>();
            Languages = new List<string>();
            MoreInfoUrls = new List<string>();
            SecurityBulletinIDs = new List<string>();
            SupersededUpdateIDs = new List<string>();
            UninstallationSteps = new List<string>();
            UninstallationBehavior = new FeInstallationBehavior();
        }
        public bool AutoSelectOnWebSites;
        public List<FeUpdate> Updates;
        public bool CanRequireSource;
        public List<FeCategory> Categories;
        public object Deadline;
        public bool DeltaCompressedContentAvailable;
        public bool DeltaCompressedContentPreferred;
        public DeploymentAction DeploymentAction;
        public string Description;
        public List<FeDownloadContent> DownloadContents;
        public DownloadPriority DownloadPriority;
        public bool EulaAccepted;
        public string EulaText;
        public string HandlerID;
        public int IdentityRevisionNumber;
        public string IdentityUpdateID;
        public FeInstallationBehavior InstallationBehavior;
        public bool IsBeta;
        public bool IsDownloaded;
        public bool IsHidden;
        public bool IsInstalled;
        public bool IsMandatory;
        public bool IsUninstallable;
        public List<string> KBArticleIDs;
        public List<string> Languages;
        public DateTime LastDeploymentChangeTime;
        public decimal MaxDownloadSize;
        public decimal MinDownloadSize;
        public List<string> MoreInfoUrls;
        public string MsrcSeverity;
        public int RecommendedCpuSpeed;
        public int RecommendedHardDiskSpace;
        public int RecommendedMemory;
        public string ReleaseNotes;
        public List<string> SecurityBulletinIDs;
        public List<string> SupersededUpdateIDs;
        public string SupportUrl;
        public string Title;
        public UpdateType UpType;
        public FeInstallationBehavior UninstallationBehavior;
        public string UninstallationNotes;
        public List<string> UninstallationSteps;

        private TreeNode _node;

        [JsonIgnoreAttribute]
        public FeUpdate Parent;
        [JsonIgnoreAttribute]
        public string UpdateSize
        {
            get
            {
                return GetUpdateSize(this.MinDownloadSize, this.MaxDownloadSize);
            }
        }
        [JsonIgnoreAttribute]
        public string Text
        {
            get
            {
                return string.Format("{0} {1} {2} ", this.LastDeploymentChangeTime.ToLongDateString(), this.UpdateSize, this.Title);
            }
        }
        
        public void RelationObject()
        {
            foreach (var item in DownloadContents)
            {
                item.Update = this;
            }
            foreach (var item in this.Updates)
            {
                item.Parent = this;
                item.RelationObject();
            }
        }
        public void PutIUpdate(IUpdate item)
        {
            AutoSelectOnWebSites = item.AutoSelectOnWebSites;
            foreach (IUpdate bund in item.BundledUpdates)
            {
                FeUpdate update = new FeUpdate();
                update.PutIUpdate(bund);
                update.Parent = this;
                Updates.Add(update);
            }
            CanRequireSource = item.CanRequireSource;
            foreach (ICategory category in item.Categories)
            {
                FeCategory obj = new FeCategory();
                obj.CategoryID = category.CategoryID;
                obj.Name = category.Name;
                obj.Description = category.Description;
                obj.Type = category.Type;
                Categories.Add(obj);
            }
            //object Deadline;
            DeltaCompressedContentAvailable = item.DeltaCompressedContentAvailable;
            DeltaCompressedContentPreferred = item.DeltaCompressedContentPreferred;
            DeploymentAction = item.DeploymentAction;
            Description = item.Description;
            for (int i = 0; i < item.DownloadContents.Count; i++)
            {
                FeDownloadContent obj = new FeDownloadContent();
                obj.DownloadUrl = item.DownloadContents[i].DownloadUrl;
                if (obj.FiExt.Length != 4) continue;
                obj.IsSelect = obj.FiExt == ".cab";
                obj.Update = this;
                DownloadContents.Add(obj);
            }
            DownloadPriority = item.DownloadPriority;
            EulaAccepted = item.EulaAccepted;
            EulaText = item.EulaText;
            HandlerID = item.HandlerID;
            IdentityRevisionNumber = item.Identity.RevisionNumber;
            IdentityUpdateID = item.Identity.UpdateID;
            InstallationBehavior.PutBehavior(item.InstallationBehavior);
            IsBeta = item.IsBeta;
            IsDownloaded = item.IsDownloaded;
            IsHidden = item.IsHidden;
            IsInstalled = item.IsInstalled;
            IsMandatory = item.IsMandatory;
            IsUninstallable = item.IsUninstallable;
            for (int i = 0; i < item.KBArticleIDs.Count; i++)
            {
                KBArticleIDs.Add(item.KBArticleIDs[i]);
            }
            for (int i = 0; i < item.Languages.Count; i++)
            {
                Languages.Add(item.Languages[i]);
            }
            LastDeploymentChangeTime = item.LastDeploymentChangeTime;
            MaxDownloadSize = item.MaxDownloadSize;
            MinDownloadSize = item.MinDownloadSize;
            for (int i = 0; i < item.MoreInfoUrls.Count; i++)
            {
                MoreInfoUrls.Add(item.MoreInfoUrls[i]);
            }
            MsrcSeverity = item.MsrcSeverity;
            RecommendedCpuSpeed = item.RecommendedCpuSpeed;
            RecommendedHardDiskSpace = item.RecommendedHardDiskSpace;
            RecommendedMemory = item.RecommendedMemory;
            ReleaseNotes = item.ReleaseNotes;
            for (int i = 0; i < item.SecurityBulletinIDs.Count; i++)
            {
                SecurityBulletinIDs.Add(item.SecurityBulletinIDs[i]);
            }
            for (int i = 0; i < item.SupersededUpdateIDs.Count; i++)
            {
                SupersededUpdateIDs.Add(item.SupersededUpdateIDs[i]);
            }
            SupportUrl = item.SupportUrl;
            Title = item.Title;
            UpType = item.Type;
            UninstallationBehavior.PutBehavior(item.UninstallationBehavior);
            UninstallationNotes = item.UninstallationNotes;
            for (int i = 0; i < item.UninstallationSteps.Count; i++)
            {
                UninstallationSteps.Add(item.UninstallationSteps[i]);
            }
        }
        public void BuildDictDown(Dictionary<string, FeDownloadContent> downDict)
        {
            foreach (var item in this.DownloadContents)
            {
                if (!downDict.ContainsKey(item.FileName))
                {
                    downDict.Add(item.FileName, item);
                }
                else
                {
                    if (item.IsSelect)
                    {
                        item.IsSelect = false;
                        item.Node.Checked = false;
                    }
                }
            }
            foreach (var item in this.Updates)
            {
                item.BuildDictDown(downDict);
            }
        }
        public void ShowToNodes(TreeNodeCollection nodes)
        {
            string key = this.IdentityUpdateID;
            string text = this.Text;
            TreeNode node = nodes.Add(key, text);
            node.Tag = this;
            this._node = node;
            foreach (var item in this.DownloadContents)
            {
                TreeNode tmpNode = node.Nodes.Add(item.DownloadUrl);
                tmpNode.Tag = item;
                tmpNode.Checked = item.IsSelect;
                item.Node = tmpNode;
            }
            foreach (var item in this.Updates)
            {
                item.ShowToNodes(node.Nodes);
            }
        }
        public void InfoToStringBuilder(StringBuilder sb)
        {
            sb.AppendFormat("LastDeploymentChangeTime:{0} Size:{1}\r\n", LastDeploymentChangeTime.ToLongDateString(), this.UpdateSize);
            sb.AppendFormat("Title:{0}\r\n", Title);
            sb.AppendFormat("Description:{0}\r\n", Description);
            sb.AppendFormat("UpdateIdentity.RevisionNumber:{0}\r\n", IdentityRevisionNumber.ToString());
            sb.AppendFormat("UpdateIdentity.UpdateID:{0}\r\n", IdentityUpdateID);
            sb.AppendFormat("HandlerID:{0}\r\n", HandlerID);
            sb.AppendFormat("IsBeta:{0}\r\n", IsBeta);
            sb.AppendFormat("IsDownloaded:{0}\r\n", IsDownloaded);
            sb.AppendFormat("IsHidden:{0}\r\n", IsHidden);
            sb.AppendFormat("IsInstalled:{0}\r\n", IsInstalled);
            sb.AppendFormat("IsMandatory:{0}\r\n", IsMandatory);
            sb.AppendFormat("IsUninstallable:{0}\r\n", IsUninstallable);
            sb.AppendFormat("ReleaseNotes:{0}\r\n", ReleaseNotes);
            sb.AppendFormat("Type:{0}\r\n", UpType);
            foreach (FeCategory category in Categories)
            {
                sb.AppendFormat("Category CategoryID:{0}\r\n", category.CategoryID);
                sb.AppendFormat("Category Name:{0}\r\n", category.Name);
                sb.AppendFormat("Category Order:{0}\r\n", category.Order);
                sb.AppendFormat("Category Description:{0}\r\n", category.Description);
                sb.AppendFormat("Category Type:{0}\r\n", category.Type);
            }
            for (int i = 0; i < SupersededUpdateIDs.Count; i++)
            {
                sb.AppendFormat("SupersededUpdateID[{0}]:{1}\r\n", i, SupersededUpdateIDs[i]);
            }
            for (int i = 0; i < SecurityBulletinIDs.Count; i++)
            {
                sb.AppendFormat("SecurityBulletinID[{0}]:{1}\r\n", i, SecurityBulletinIDs[i]);
            }
            for (int i = 0; i < Languages.Count; i++)
            {
                sb.AppendFormat("Language[{0}]:{1}\r\n", i, Languages[i]);
            }
            for (int i = 0; i < KBArticleIDs.Count; i++)
            {
                sb.AppendFormat("kbArticleId[{0}]:{1}\r\n", i, KBArticleIDs[i]);
            }
            for (int i = 0; i < DownloadContents.Count; i++)
            {
                string url = DownloadContents[i].DownloadUrl;
                sb.AppendFormat("DownloadContentsUrl[{0}]:{1}\r\n", i, url);
            }
        }
        public string GetUpdateDir()
        {
            if (Parent != null)
            {
                string dir = Parent.GetUpdateDir();
                return Path.Combine(dir, LastDeploymentChangeTime.ToString("yyyyMMdd") + "_" + getTitleKbNumber(Title) + "_" + this.UpdateSize);
            }
            return LastDeploymentChangeTime.ToString("yyyyMMdd") + "_" + getTitleKbNumber(Title) + "_" + this.UpdateSize;
        }

        private string GetFileKbNumber(string value)
        { //windows6.1-kb2852386-x64-express.cab
            value = value.ToUpper();
            int p1 = value.IndexOf("KB");
            if (p1 == -1) return "";
            value = value.Substring(p1, value.Length - p1);
            p1 = value.IndexOf('-');
            if (p1 == -1) return "";
            value = value.Substring(0, p1);
            return value;
        }
        private string getTitleKbNumber(string value)
        {
            int p1 = value.IndexOf('(');
            int p2 = value.IndexOf(')');
            if ((p1 != -1) && (p2 != -1))
            {
                return value.Substring(p1 + 1, p2 - p1 - 1);
            }
            return "KB00";
        }
        public string GetUpdateSize(decimal minDownloadSize, decimal maxDownloadSize)
        {
            decimal den = 1024;
            string ustr = "KB";
            if (maxDownloadSize > 1024 * 1024)
            {
                den = 1024 * 1024;
                ustr = "MB";
            }
            string updateSize;
            if ((maxDownloadSize == minDownloadSize) || (string.Format("{0:N2}", minDownloadSize) == "0.00"))
            {
                updateSize = string.Format("{0:N2}{1}", maxDownloadSize / den, ustr);
            }
            else
            {
                updateSize = string.Format("{0:N2}-{1:N2}{2}", minDownloadSize / den, maxDownloadSize / den, ustr);
            }
            return updateSize;
        }
        public void SetPatchRoot(string root)
        {
            foreach (var item in this.DownloadContents)
            {
                item.SetPatchRoot(root);
            }
            foreach (var item in this.Updates)
            {
                item.SetPatchRoot(root);
            }
        }
        public void DimsSelect(string root)
        {
            foreach (var item in DownloadContents)
            {
                item.DimsSelect(root);
            }
            foreach (var item in Updates)
            {
                item.DimsSelect(root);
            }
        }
    }
    public class Student
    {
        private string _name;
        public Student(string name)
        {
            _name = name;
        }
        public int ID { get; set; }

        public string Name {
            get { return _name; }
        }

        public int Age { get; set; }

        public string Sex { get; set; }
    }
}

/*
Dictionary<string, string> downList = new Dictionary<string, string>(); // kb  dir
DirectoryInfo dir = new DirectoryInfo(@"C:\test\down");

int x = 1;
foreach (DirectoryInfo nDir in dir.GetDirectories())
{
    foreach (FileInfo nFile in nDir.GetFiles())
    {
        if (nFile.Extension.ToLower() != ".cab") continue;
        string kb = GetFileKbNumber(nFile.Name);
        if (kb != "")
        {
            if (downList.ContainsKey(kb))
            {
                downList.Add(kb+"_"+x.ToString(), nDir.Name);
                x++;
            }
            else
            {
                downList.Add(kb, nDir.Name);
            }
        }
        break;
    }
}
StringBuilder tmpSB = new StringBuilder();
foreach (var item in downList)
{
    string sDir = Path.Combine(@"C:\test\down", item.Value);
    DirectoryInfo di = new DirectoryInfo(sDir);
    di.MoveTo(Path.Combine(@"C:\test\down", item.Key + "_" + item.Value));
    tmpSB.AppendFormat("{0} : {1}\r\n", item.Key, item.Value);
}
textBox1.Text = tmpSB.ToString();
return;
*/