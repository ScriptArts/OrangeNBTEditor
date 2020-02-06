using OrangeNBT.NBT;
using OrangeNBT.NBT.IO;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrangeNBTEditor.Models
{
    public class DataStore : BindableBase
    {
        /// <summary>
        /// ツリーデータ
        /// </summary>
        public static ReactiveCollection<TreeNode> TreeData { get; } = new ReactiveCollection<TreeNode>();

        /// <summary>
        /// NBTファイルを開く
        /// </summary>
        /// <param name="paths">ファイルパス(可変長)</param>
        public static void OpenFile(params string[] paths)
        {
            Task.Run(() =>
            {
                TreeData.ClearOnScheduler();
                foreach (var path in paths)
                {
                    // TODO: 読み込みが遅すぎる
                    // ユーザーがツリーを展開した時にその階層だけ読み込むような処理にすべきか
                    var node = RecursiveLoad(path);
                    TreeData.AddOnScheduler(node);
                }
            });
        }

        /// <summary>
        /// パスからフォルダを再帰的に読み込み、ツリーを生成
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="node">ノード</param>
        /// <returns></returns>
        private static TreeNode RecursiveLoad(string path, TreeNode node = null)
        {
            var addNode = new TreeNode();
            addNode.Text.Value = Path.GetFileName(path);

            // ディレクトリならそのまま再帰読込
            // それ以外ならNBTファイルとして読込を試みる
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                addNode.Icon.Value = "/Assets/Icons/Folder.png";

                try
                {
                    foreach (var child in Directory.GetFileSystemEntries(path))
                    {
                        RecursiveLoad(child, addNode);
                    }
                }
                catch (DirectoryNotFoundException) { }
            }
            else
            {
                try
                {
                    var tag = NBTFile.FromFile(path);
                    addNode.Icon.Value = "/Assets/Icons/Container.png";
                    addNode.Text.Value += $" [{tag.Count} entries]";
                    foreach (var child in tag)
                    {
                        addNode.Add(AddTag(child));
                    }
                }
                catch
                {
                    // 例外を吐いた=NBTファイルではないという安直な考え
                    return node;
                }
            }

            if (node == null && !string.IsNullOrEmpty(addNode.Icon.Value)) return addNode;

            node.Add(addNode);
            return node;
        }

        /// <summary>
        /// NBTを再帰的に読み込み、ツリーを生成
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeNode AddTag(TagBase tag, TreeNode node = null)
        {
            var addnode = new TreeNode();
            addnode.NBT.Value = tag;

            switch (tag.TagType)
            {
                case TagType.Compound:
                    var compound = (TagCompound)tag;
                    foreach (var child in compound)
                    {
                        addnode = AddTag(child, addnode);
                    }
                    break;
                case TagType.List:
                    var list = (TagList)tag;
                    foreach (TagBase child in list)
                    {
                        addnode = AddTag(child, addnode);
                    }
                    break;
            }

            if (node == null) return addnode;

            node.Add(addnode);
            return node;
        }
    }
}
