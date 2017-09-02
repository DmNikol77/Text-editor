using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TextEditor.BL;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IMainForm view;
        private readonly IFileManager manager;
        private readonly IMessageService messageService;

        private string currentFilePath;

        public MainPresenter(IMainForm view, IFileManager manager, IMessageService messageService)
        {
            this.view = view;
            this.manager = manager;
            this.messageService = messageService;

            view.SetSymbolCount(0);

            view.ContentChanged += new EventHandler(view_ContentChanged);
            view.FileOpenClick += new EventHandler(view_FileOpenClick);
            view.FileSaveClick += new EventHandler(view_FileSaveClick);
        }

        void view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = view.Content;
                manager.SaveContent(content, currentFilePath);
                messageService.ShowMessage("Файл успешно сохранён.");
            }
            catch (Exception ex)
            {
                messageService.ShowError(ex.Message);
            }
        }

        void view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = view.FilePath;
                bool isExist = manager.IsExist(filePath);

                if (!isExist)
                {
                    messageService.ShowExclamation("Выбранный файл не существует.");
                    return;
                }

                currentFilePath = filePath;

                string content = manager.GetContent(filePath);
                int count = manager.GetSymbolCount(content);
                view.Content = content;
                view.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                messageService.ShowError(ex.Message);
            }
        }

        void view_ContentChanged(object sender, EventArgs e)
        {
            string content = view.Content;
            int count = manager.GetSymbolCount(content);
            view.SetSymbolCount(count);
        }
    }
}
