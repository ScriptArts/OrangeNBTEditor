using Microsoft.WindowsAPICodePack.Dialogs;
using OrangeNBTEditor.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeNBTEditor.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region コマンド

        /// <summary>
        /// ｢開く｣コマンド
        /// </summary>
        public ReactiveCommand FileOpenCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// ｢フォルダを開く｣コマンド
        /// </summary>
        public ReactiveCommand FolderOpenCommand { get; } = new ReactiveCommand();

        #endregion

        public MainWindowViewModel()
        {
            FileOpenCommand.Subscribe(() =>
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.Multiselect = true;
                dialog.Filters.Add(new CommonFileDialogFilter("全てのファイル", "*.*"));
                dialog.Filters.Add(new CommonFileDialogFilter("NBTファイル", "*.dat;*.schematic"));
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok) DataStore.OpenFile(dialog.FileNames.ToArray());
            });

            FolderOpenCommand.Subscribe(() =>
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.Multiselect = true;
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok) DataStore.OpenFile(dialog.FileNames.ToArray());
            });
        }
    }
}
