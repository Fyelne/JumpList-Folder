using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace CSPowerShellApp
{
    public partial class Form1 : Form
    {
        List<string> listFiles = new List<string>();
        JumpList jumpList;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            // get where the app is running from
            // create a jump list
            JumpListCustomCategory category = new JumpListCustomCategory("Food");
            category.AddJumpListItems(
                new JumpListLink("calc.exe", "Crabs") { Arguments = "Crabs" },
                new JumpListLink("notepad.exe", "Prawns") { Arguments = "Prawns" });
            jumpList.AddCustomCategories(category);

            jumpList.Refresh();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listFiles.Clear();
            listView.Items.Clear();
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "All Files (*.*)|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = ofd.FileName;
                    imageList.Images.Add(Icon.ExtractAssociatedIcon(ofd.FileName));
                    FileInfo fi = new FileInfo(ofd.FileName);
                    listFiles.Add(fi.FullName);
                    listView.Items.Add(fi.Name, imageList.Images.Count - 1);
                    
                    JumpListCustomCategory manger = new JumpListCustomCategory("Manger");
                    manger.AddJumpListItems(new JumpListLink(ofd.FileName, "Shrimps"));
                    jumpList.AddCustomCategories(manger);
                    jumpList.Refresh();
                    
                }
            }
        }
    }
}
