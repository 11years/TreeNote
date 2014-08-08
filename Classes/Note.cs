﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TreeNote.Classes
{
    public class Note
    {
        protected int maxId;

        public Classes.Note parent { get; protected set; }
        public Classes.Note nextItem { get; protected set; }
        public Classes.Note prevItem { get; protected set; }
        public List<Note> children { get; protected set; }

        public int id { get; internal set; }
        public string title { get; set;}
        public string body { get; set;}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Note()
        {
            this.id = 1;
            this.title = "";
            this.body = "";

            this.parent = null;
            this.nextItem = null;
            this.prevItem = null;

            this.maxId = 1;

            children = new List<Note>();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public Note(string title, string body)
        {
            this.id = -1;
            this.title = title;
            this.body = body;

            children = new List<Note>();
        }

        /// <summary>
        /// 子要素の追加
        /// </summary>
        /// <param name="note"></param>
        public void Add(Classes.Note note)
        {
            this.children.Add(note);
            initSetting(children.Count - 1);
        }
        /// <summary>
        /// 子要素の挿入
        /// </summary>
        /// <param name="index">挿入位置</param>
        /// <param name="note"></param>
        public void Insert(int index, Classes.Note note)
        {
            this.children.Insert(index, note);
            initSetting(index);
        }
        /// <summary>
        /// ID,Parent,Next,Prevの設定
        /// </summary>
        /// <param name="childIndex"></param>
        protected void initSetting(int childIndex)
        {
            this.children[childIndex].parent = this;

            if (childIndex > 0)
            {
                this.children[childIndex].prevItem = this.children[childIndex - 1];
            }
            if (childIndex < this.children.Count - 1)
            {
                this.children[childIndex].nextItem = this.children[childIndex + 1];
            }

            SetNewID(this.children[childIndex]);

        }
        /// <summary>
        /// IDを再採番
        /// </summary>
        /// <param name="note"></param>
        protected void SetNewID(Classes.Note note)
        {
            Classes.Note root = getRoot();
            int mid = root.getMaxId();
            setID(note, mid + 1);
        }
        protected int getMaxId()
        {
            int result = -1;

            if (this.HasChild())
            {
                foreach (Classes.Note tn in this.children)
                {
                    int t = tn.getMaxId();
                    result = result < t ? t : result;
                }
            }
            else
            {
                return this.id;
            }

            return result;
        }
        protected void setID(Classes.Note note, int id)
        {
            note.id = id;

            for (int i = 0; i < this.children.Count; i++ )
            {
                setID(this.children[i], id + i);
            }
        }
        protected Classes.Note getRoot()
        {
            if (this.parent != null)
            {
                return this.parent.getRoot();
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 未実装
        /// </summary>
        public void RemoveAll()
        {

        }
        public void Remove(Note note)
        {
            int index = this.children.IndexOf(note);

            Note nextItem = note.HasNext() ? note.nextItem : null;
            Note prevItem = note.HasPrev() ? note.prevItem : null;

            if (nextItem != null)
            {
                nextItem.prevItem = prevItem;
            }
            if (prevItem != null)
            {
                prevItem.nextItem = nextItem;
            }

            this.children.RemoveAt(index);

        }

        //なんでメソッドなのか
        public bool HasParent() { return !(this.parent == null); }
        public bool HasNext() { return !(this.nextItem == null); }
        public bool HasPrev() { return !(this.prevItem == null); }
        public bool HasChild() { return this.children.Count != 0; }

        /// <summary>
        /// 子要素のID検索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note Search(int id) 
        {
            return _Find(id);
        }
        protected Note _Find(int id)
        {
            Note result = null;
            if (this.id == id)
            {
                result = this;
            }
            else if (this.children.Count != 0)
            {
                foreach (Note tNote in this.children)
                {
                    Note ret = tNote._Find(id);
                    if (ret != null) 
                    {
                        result = ret;
                        break; 
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 子要素の文字列検索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Note Search(string keyword)
        {
            return null;
        }



        public bool Save(string xmlPath)
        {
            bool result = false;
            // XML設定
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.IndentChars = "    ";   // スペース４つ

            using (XmlWriter w = XmlWriter.Create(xmlPath, setting))
            {
                w.WriteStartElement("root");

                result = this._save(root, w);

                w.WriteEndElement(); // </Books>
            }

            return result;
        }
        protected bool _save(Note targetNote, XmlWriter w)
        {
            if (targetNote != root)
            {
                w.WriteStartElement("Note");

                w.WriteElementString("Id", targetNote.id.ToString());
                w.WriteElementString("Title", targetNote.title);
                w.WriteElementString("Body", targetNote.body);

                if (targetNote.HasPrev())
                {
                    w.WriteElementString("PrevId", targetNote.prevId.ToString());
                }

                w.WriteElementString("ParentId", targetNote.parentId.ToString());

                w.WriteEndElement(); // </Note>

            }

            if (targetNote.HasChild())
            {
                foreach (Note tn in targetNote.children)
                {
                    this._save(tn, w);
                }
            }
            return true;
        }
    }

    class NoteReader
    {
        public static Note Read(string xmlPath)
        {
            List<Tuple<int, string, string, int, int>> itemList = new List<Tuple<int, string, string, int, int>>();

            string fn = xmlPath;
            if (File.Exists(fn))
            {
                XmlTextReader reader = null;
                try
                {
                    reader = new XmlTextReader(fn);

                    int tId = -1;
                    string tTitle = "";
                    string tBody = "";
                    int tPrevId = -1;
                    int tParentId = -1;
                    
                    
                    //ストリームからノードを読み取る
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.LocalName)
                            {
                                case "Id":
                                    tId = int.Parse(reader.ReadString());
                                    break;
                                case "Title":
                                    tTitle = reader.ReadString();
                                    break;
                                case "Body":
                                    tBody = reader.ReadString();
                                    break;
                                case "PrevId":
                                    tPrevId = int.Parse(reader.ReadString());
                                    break;
                                case "ParentId":
                                    tParentId = int.Parse(reader.ReadString());

                                    itemList.Add(Tuple.Create<int, string, string, int, int>(tId, tTitle, tBody, tParentId, tPrevId));

                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
                finally
                {
                    reader.Close();
                }
            }

            Note ret = ConstructionNotes(itemList);
            



            return ret;

        }

        protected static Note ConstructionNotes(List<Tuple<int, string, string, int, int>> paramList)
        {
            int targetId;
            int prevId;

            if (outList.Count == 0)
            {
                targetId = -1;
            }
            else
            {
            }
        }
        protected static Tuple<int, string, string, int, int> ConstructionNote(List<Tuple<int, string, string, int, int>> paramList, int targetId, int prevId)
        {
            var obj = 
                from q in paramList 
                where q.Item4 == targetId && q.Item5 == prevId 
                select q;

            List<Tuple<int, string, string, int, int>> t = obj.ToList();

            Note ret = new Note();
            ret.id = t[0].Item1;
            ret.title = t[0].Item2;
            ret.body = t[0].Item3;
            

            return t[0];
        }
    }

    class NoteWriter
    {
    }
}
