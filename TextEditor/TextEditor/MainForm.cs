﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextEditor
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }

    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            btnOpenFile.Click += new EventHandler(btnOpenFile_Click);
            btnSaveFile.Click += new EventHandler(btnSaveFile_Click);
            fldContent.TextChanged += new EventHandler(fldContent_TextChanged);
            btnSelectFile.Click += btnSelectFile_Click;
            numFont.ValueChanged += numFont_ValueChanged;
        }

        #region События
        void fldContent_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null)
            {
                ContentChanged(this, EventArgs.Empty);
            }
        }

        void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null)
            {
                FileSaveClick(this, EventArgs.Empty);
            }
        }

        void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null)
            {
                FileOpenClick(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Реализация интерфейса IMainForm
        public string FilePath
        {
            get { return fldFilePath.Text; }
        }

        public string Content
        {
            get
            {
                return fldContent.Text;
            }
            set
            {
                fldContent.Text = value;
            }
        }

        public void SetSymbolCount(int count)
        {
            lblSymbolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;
        #endregion

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = ofd.FileName;

                if (FileOpenClick != null)
                {
                    FileOpenClick(this, EventArgs.Empty);
                }
            }
        }

        private void numFont_ValueChanged(object sender, EventArgs e)
        {
            fldContent.Font = new Font("Calibri", (float)numFont.Value);
        }
    }
}
