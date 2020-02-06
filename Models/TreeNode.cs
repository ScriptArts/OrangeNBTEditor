using OrangeNBT.NBT;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeNBTEditor.Models
{
    public class TreeNode: BindableBase
    {
        /// <summary>
        /// ノードが展開されているかを取得、設定します
        /// </summary>
        public ReactiveProperty<bool> IsExpanded { get; } = new ReactiveProperty<bool>(false);

        /// <summary>
        /// ノードのアイコンを取得、設定します
        /// </summary>
        public ReactiveProperty<string> Icon { get; } = new ReactiveProperty<string>();

        /// <summary>
        /// ノードのテキストを取得、設定します
        /// </summary>
        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>("");

        /// <summary>
        /// ノードの親ノードを取得します
        /// </summary>
        public ReactiveProperty<TreeNode> Parent { get; } = new ReactiveProperty<TreeNode>();

        /// <summary>
        /// ノードの子ノードの取得、設定します
        /// </summary>
        public ReactiveCollection<TreeNode> Children { get; } = new ReactiveCollection<TreeNode>();

        /// <summary>
        /// ノードのNBTTagを取得、設定します
        /// </summary>
        public ReactiveProperty<TagBase> NBT { get; } = new ReactiveProperty<TagBase>();

        public TreeNode()
        {
            NBT.Subscribe(value =>
            {
                if (value == null) return;
                Icon.Value = getIcon(value.TagType);
                Text.Value = getText(value);
            });
        }

        /// <summary>
        /// ノードの子ノードを追加します
        /// </summary>
        /// <param name="child">子ノード</param>
        public void Add(TreeNode child)
        {
            child.Parent.Value = this;
            Children.Add(child);
        }

        /// <summary>
        /// タグタイプに対応したアイコンのパスを返します
        /// </summary>
        /// <param name="tagType">タグタイプ</param>
        /// <returns>アイコンのパス</returns>
        private string getIcon(TagType tagType)
        {
            string iconPath = "";

            switch (tagType)
            {
                case TagType.Compound:
                    iconPath = "/Assets/Icons/CompoundTag.png";
                    break;
                case TagType.List:
                    iconPath = "/Assets/Icons/ListTag.png";
                    break;
                case TagType.Byte:
                    iconPath = "/Assets/Icons/ByteTag.png";
                    break;
                case TagType.ByteArray:
                    iconPath = "/Assets/Icons/ByteArrayTag.png";
                    break;
                case TagType.Short:
                    iconPath = "/Assets/Icons/ShortTag.png";
                    break;
                case TagType.Int:
                    iconPath = "/Assets/Icons/IntTag.png";
                    break;
                case TagType.IntArray:
                    iconPath = "/Assets/Icons/IntArrayTag.png";
                    break;
                case TagType.Long:
                    iconPath = "/Assets/Icons/LongTag.png";
                    break;
                case TagType.LongArray:
                    iconPath = "/Assets/Icons/LongArrayTag.png";
                    break;
                case TagType.Float:
                    iconPath = "/Assets/Icons/FloatTag.png";
                    break;
                case TagType.Double:
                    iconPath = "/Assets/Icons/DoubleTag.png";
                    break;
                case TagType.String:
                    iconPath = "/Assets/Icons/StringTag.png";
                    break;
            }

            return iconPath;
        }

        /// <summary>
        /// タグの内容を文字列で返します
        /// </summary>
        /// <param name="tag">NBT</param>
        /// <returns>タグの内容(文字列)</returns>
        private string getText(TagBase tag)
        {
            string name = "";
            string val = "";

            switch (tag.TagType)
            {
                case TagType.Compound:
                    name = (tag as TagCompound).Name;
                    val = $"{(tag as TagCompound).Count} entries";
                    break;
                case TagType.List:
                    name = (tag as TagList).Name;
                    val = $"{(tag as TagList).Count} entries";
                    break;
                case TagType.Byte:
                    name = (tag as TagByte).Name;
                    val = $"{(tag as TagByte).Value}";
                    break;
                case TagType.ByteArray:
                    name = (tag as TagByteArray).Name;
                    val = $"{(tag as TagByteArray).Value.Length} bytes";
                    break;
                case TagType.Short:
                    name = (tag as TagShort).Name;
                    val = $"{(tag as TagShort).Value}";
                    break;
                case TagType.Int:
                    name = (tag as TagInt).Name;
                    val = $"{(tag as TagInt).Value}";
                    break;
                case TagType.IntArray:
                    name = (tag as TagIntArray).Name;
                    val = $"{(tag as TagIntArray).Value.Length} integers";
                    break;
                case TagType.Long:
                    name = (tag as TagLong).Name;
                    val = $"{(tag as TagLong).Value}";
                    break;
                case TagType.LongArray:
                    name = (tag as TagLongArray).Name;
                    val = $"{(tag as TagLongArray).Value.Length} longs";
                    break;
                case TagType.Float:
                    name = (tag as TagFloat).Name;
                    val = $"{(tag as TagFloat).Value}";
                    break;
                case TagType.Double:
                    name = (tag as TagDouble).Name;
                    val = $"{(tag as TagDouble).Value}";
                    break;
                case TagType.String:
                    name = (tag as TagString).Name;
                    val = (tag as TagString).Value;
                    break;
            }

            return (string.IsNullOrEmpty(name)) ? val : $"{name}: {val}";
        }
    }
}
