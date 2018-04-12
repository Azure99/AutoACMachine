using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OnlineJudgeClient;
using System.Threading;

namespace AutoACMachine
{
    public partial class MainForm : Form
    {
        public static MainForm mainForm = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            mainForm = this;
            comboBox_Crawler.SelectedIndex = 0;
            comboBox_OJ.SelectedIndex = 0;
            
        }
        
        struct WorkAgrs
        {
            public IOnlineJudgeClient client;
            public ICrawler crawler;
            public string username;
            public string password;
            public int startID;
            public int endID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WorkAgrs args;
            args.client = null;
            args.crawler = null;

            if (comboBox_OJ.SelectedIndex == 0)
            {
                args.client = new HDUClient();
            }

            if(comboBox_Crawler.SelectedIndex == 0)
            {
                args.crawler = new ACMSearchCrawler();
            }

            if (string.IsNullOrEmpty(args.username = textBox_Username.Text)) 
            {
                MessageBox.Show("用户名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(args.password = textBox_Password.Text))
            {
                MessageBox.Show("密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(!int.TryParse(textBox_StartID.Text, out int startID))
            {
                MessageBox.Show("题目ID只能为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(textBox_EndID.Text, out int endID))
            {
                MessageBox.Show("题目ID只能为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            args.startID = startID;
            args.endID = endID;

            button_Start.Enabled = false;
            Thread workThread = new Thread(new ParameterizedThreadStart(WorkThread));
            workThread.Start(args);
        }

        public void WorkThread(object args)
        {
            WorkAgrs workAgrs = (WorkAgrs)args;
            Controller controller = new Controller(workAgrs.client, workAgrs.crawler, workAgrs.username, workAgrs.password);
            controller.AutoAC(workAgrs.startID, workAgrs.endID);
            button_Start.Enabled = true;
        }
        public void AppendMessage(string message)
        {
            textBox_Msg.AppendText(message + "\r\n");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void linkLabel_Blog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.rainng.com/");
        }

        private void linkLabel_SourceCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Azure99/AutoACMachine");
        }
    }
}
