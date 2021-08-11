using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Faust
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string _DESCTOPPATH = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static readonly string ExecutablePath = Assembly.GetEntryAssembly().Location;
        private static readonly string StartupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        private static readonly string InstallLocation = Path.Combine(StartupDirectory, Path.GetFileName(ExecutablePath));

        private void Form1_Load(object sender, EventArgs e)
        {
            InstallToStartup();
            notifyIcon1.BalloonTipTitle = "Faust";
            notifyIcon1.BalloonTipText = "The program is minimized in the tray ";
            notifyIcon1.Text = "Faust";
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            // DIRECTORIES

            string[] _filesNormal = Directory.GetFiles(_DESCTOPPATH, "*");
            string[] _directoriesNormal = Directory.GetDirectories(_DESCTOPPATH, "*", SearchOption.AllDirectories);

            // REMOVE desktop.ini

            for (int i = 0; i < _filesNormal.Length; i++)
            {
                if (_filesNormal[i] == _DESCTOPPATH + "\\desktop.ini")
                {
                    Remove_desctop_ini(ref _filesNormal, i);
                }
            }

            // FILES

            foreach (string _filesConvert in _filesNormal)
            {

                FileInfo _thisFile = new FileInfo(_filesConvert);

                if (_thisFile.Attributes.HasFlag(FileAttributes.Normal))
                {
                    _thisFile.Attributes = FileAttributes.Hidden;
                }
                else
                {
                    _thisFile.Attributes = FileAttributes.Normal;
                }

            }

            // FOLDERS

            foreach (string _directoriesConvert in _directoriesNormal)
            {

                DirectoryInfo _thisDirectory = new DirectoryInfo(_directoriesConvert);

                if (!_thisDirectory.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    _thisDirectory.Attributes = FileAttributes.Hidden;
                }
                else
                {
                    _thisDirectory.Attributes = FileAttributes.Normal;
                }
            }
        }

        public static void InstallToStartup()
        {
            if (!File.Exists(InstallLocation))
            {
                File.Copy(ExecutablePath, InstallLocation);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hiddenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string[] _filesNormal = Directory.GetFiles(_DESCTOPPATH, "*");
            string[] _directoriesNormal = Directory.GetDirectories(_DESCTOPPATH, "*", SearchOption.AllDirectories);

            // REMOVE desktop.ini

            for (int i = 0; i < _filesNormal.Length; i++)
            {
                if (_filesNormal[i] == _DESCTOPPATH + "\\desktop.ini")
                {
                    Remove_desctop_ini(ref _filesNormal, i);
                }
            }

            // HIDE FILES

            foreach (string _filesConvert in _filesNormal)
            {
                FileInfo _thisFile = new FileInfo(_filesConvert);
                _thisFile.Attributes = FileAttributes.Hidden;
            }

            // HIDE FOLDERS

            foreach (string _directoriesConvert in _directoriesNormal)
            {
                DirectoryInfo _thisDirectory = new DirectoryInfo(_directoriesConvert);
                _thisDirectory.Attributes = FileAttributes.Hidden;
            }
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string[] _filesNormal = Directory.GetFiles(_DESCTOPPATH, "*");
            string[] _directoriesNormal = Directory.GetDirectories(_DESCTOPPATH, "*", SearchOption.AllDirectories);

            // REMOVE desktop.ini

            for (int i = 0; i < _filesNormal.Length; i++)
            {
                if (_filesNormal[i] == _DESCTOPPATH + "\\desktop.ini")
                {
                    Remove_desctop_ini(ref _filesNormal, i);
                }
            }

            // NORMAL FILES

            foreach (string _filesConvert in _filesNormal)
            {
                FileInfo _thisFile = new FileInfo(_filesConvert);
                _thisFile.Attributes = FileAttributes.Normal;
            }

            // NORMAL FOLDERS

            foreach (string _directoriesConvert in _directoriesNormal)
            {
                DirectoryInfo _thisDirectory = new DirectoryInfo(_directoriesConvert);
                _thisDirectory.Attributes = FileAttributes.Normal;
            }
        }

        private static void Remove_desctop_ini(ref string[] array, int index)
        {
            string[] newArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }

            for (int i = index + 1; i < array.Length; i++)
            {
                newArray[i - 1] = array[i];
            }

            array = newArray;
        }
    }
}
