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

        public Classes.Note parent { get; protected set; }
        public Classes.Note nextItem { get; protected set; }
        public Classes.Note prevItem { get; protected set; }
        public List<Note> children { get; protected set; }

        public int id { get; protected set; }
        public string title { get; set;}
        public string body { get; set;}

        public Note()
        {
            this.id = -1;
            this.title = "";
            this.body = "";

            this.parent = null;
            this.nextItem = null;
            this.prevItem = null;

            children = new List<Note>();
        }

        protected Note(string title, string body)
        {
            this.id = -1;
            this.title = title;
            this.body = body;

            children = new List<Note>();
        }

        public int Add(string title, string body, int prevNoteID = 1)
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
