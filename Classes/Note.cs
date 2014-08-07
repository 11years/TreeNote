using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace TreeNote.Classes
{
    public class Note
    {
        static protected int maxId;
        static public Note root;

        public int parentId { get; protected set; }
        public int nextId { get; protected set; }
        public int prevId { get; protected set; }
        public List<Note> children { get; protected set; }

        public int id { get; protected set; }
        public string title { get; protected set;}
        public string body { get; protected set;}

        public static Note GetRootInstance()
        {
            if (root == null)
            {
                root = new Note(0, "TreeNote", "Auther Goto");
            }
            return root;
        }

        protected Note()
        {
            this.id = -1;
            this.title = "";
            this.body = "";

            this.parentId = -1;
            this.nextId = -1;
            this.prevId = -1;

            children = new List<Note>();
        }

        protected Note(int id, string title, string body, int parentId = -1, int prevId = -1, int nextId = -1)
        {
            this.id = id;
            this.title = title;
            this.body = body;

            this.parentId = parentId;
            this.nextId = nextId;
            this.prevId = prevId;

            children = new List<Note>();
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }

        public void SetBody(string body)
        {
            this.body = body;
        }

        public int AddChild(string title, string body, int prevNoteID = 1)
        {
            maxId++;
            this._add(maxId, title, body, prevId);
            return maxId;
        }

        protected int _add(int id, string title, string body, int prevNoteID = -1)
        {
            int nextNoteId = -1;
            if (prevNoteID != -1)
            {
                nextNoteId = this.Find(prevNoteID).nextId;
            }
            else
            {
                if (this.children.Count != 0)
                {
                    nextNoteId = this.children[0].id;
                }
            }

            maxId++;
            children.Add(new Note(id, title, body, this.id, prevNoteID, nextNoteId));
            return maxId;
        }

        public Note GetNext() { return this.Find(this.nextId); }
        public Note GetPrev() { return this.Find(this.prevId); }
        public bool HasParent() { return this.parentId >= 0; }
        public bool HasNext() { return this.nextId != -1; }
        public bool HasPrev() { return this.prevId != -1; }
        public bool HasChild() { return this.children.Count != 0; }

        public Note Find(int id) 
        {
            return root._Find(id);
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

        public bool Read(string xmlPath)
        {
            GetRootInstance();

            string fn = xmlPath;
            if (File.Exists(fn))
            {
                XmlTextReader reader = null;
                try
                {
                    reader = new XmlTextReader(fn);
                    Note tNote = null;
                    //ストリームからノードを読み取る
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.LocalName)
                            {
                                case "Id":
                                    tNote = new Note();
                                    tNote.id = int.Parse(reader.ReadString());
                                    break;
                                case "Title":
                                    tNote.title = reader.ReadString();
                                    break;
                                case "Body":
                                    tNote.body = reader.ReadString();
                                    break;
                                case "PrevId":
                                    tNote.prevId = int.Parse(reader.ReadString());
                                    if (tNote.prevId != -1)
                                    {
                                        this.Find(tNote.prevId).nextId = tNote.id;
                                    }
                                    break;
                                case "ParentId":
                                    this.Find(int.Parse(reader.ReadString()))._add(tNote.id, tNote.title, tNote.body, tNote.prevId);
                                    tNote = null;
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

            return true;

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
}
