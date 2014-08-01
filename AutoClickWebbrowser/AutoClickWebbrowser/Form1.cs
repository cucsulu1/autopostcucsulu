﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClickWebbrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Process CurrentBrower { get; set; }
        WinUtilities win = new WinUtilities();

        private void btnStart_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(textBox1.Text); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClickButton("div", "jscontroller", "qG1h8c");
        }
        void ClickButton(string tagName,string attribute, string attName)
        {
            var x = webBrowser1.ProductName;
            HtmlElementCollection col = webBrowser1.Document.GetElementsByTagName(tagName);
            foreach (HtmlElement element in col)
            {
                if (element.GetAttribute(attribute).Equals(attName))
                {
                    // Invoke the "Click" member of the button
                    element.InvokeMember("click");
                }
            }
        }
        private void btnStartGP_Click(object sender, EventArgs e)
        {
            var x = GetListGroup();
            win.LeftClick(new Point(294, 447));
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //CurrentChrome = Process.Start("firefox.exe", "http:\\www.google.com");
            Process[] processesByName = Process.GetProcesses();
            CurrentBrower = processesByName.FirstOrDefault(p => p.ProcessName.Contains("firefox"));
            if (CurrentBrower != null)
            {
                WinUtilities.SetParent(CurrentBrower.MainWindowHandle, pnClient.Handle);
                WinUtilities.MoveWindow(CurrentBrower.MainWindowHandle, 0, 0, pnClient.Width, pnClient.Height, false);
                win.WndHandle = CurrentBrower.Handle;
            }
        }


        public List<string> GetListGroup()
        {
            var result = new List<string>();
            if (webBrowserGroup.Document != null)
            {
                HtmlElementCollection col = webBrowserGroup.Document.GetElementsByTagName("a");
                foreach (HtmlElement element in col)
                {
                    var href=element.GetAttribute("href");
                    if (href.Contains("communities") && element.GetAttribute("hc").Contains("off") && !result.Contains(href))
                    {
                        result.Add(href);
                    }
                }
            }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowserGroup.Url = new Uri("https://plus.google.com/communities");
        } 
    }
}