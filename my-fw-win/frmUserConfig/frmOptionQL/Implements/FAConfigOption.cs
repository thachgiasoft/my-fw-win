using System;
using System.Xml;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
	public class FAConfigOption : System.Configuration.AppSettingsReader
	{
		private XmlNode node;
		private string _cfgFile;

		public string cfgFile
		{
			get	{ return _cfgFile; }
			set	{ _cfgFile=value; }
		}

		public string GetValue (string key)
		{
			return Convert.ToString(GetValue(key, typeof(string)));
		}

		public new object GetValue (string key, System.Type sType)
		{
			XmlDocument doc = new XmlDocument();
			object ro = String.Empty;
			loadDoc(doc);
			string sNode = key.Substring(0, key.LastIndexOf("//"));
			// retrieve the selected node
			try
			{
				node =  doc.SelectSingleNode(sNode);
				if( node != null )
				{
					// Xpath selects element that contains the key
					XmlElement targetElem= (XmlElement)node.SelectSingleNode(key) ;
					if (targetElem!=null)
					{
						ro = targetElem.GetAttribute("value");
					}
				}
				if (sType == typeof(string))
					return Convert.ToString(ro);
				else
					if (sType == typeof(bool))
				{
					if (ro.Equals("True") || ro.Equals("False"))
						return Convert.ToBoolean(ro);
					else
						return false;
				}
				else
					if (sType == typeof(int))
					return Convert.ToInt32(ro);
				else
					if (sType == typeof(double))
					return Convert.ToDouble(ro);
				else
					if (sType == typeof(DateTime))
					return Convert.ToDateTime(ro);
				else
					return Convert.ToString(ro);
			}
			catch (Exception ex)
			{
                PLException.AddException(ex);
				return String.Empty;
			}
		}

		public bool SetValue (string key, string val)
		{
			XmlDocument doc = new XmlDocument();
			loadDoc(doc);
			try
			{
				// retrieve the target node
				string sNode = key.Substring(0, key.LastIndexOf("//"));
				node =  doc.SelectSingleNode(sNode);
				if( node == null )
					return false;
				// Set element that contains the key
				XmlElement targetElem= (XmlElement) node.SelectSingleNode(key);
				if (targetElem!=null)
				{
					// set new value
					targetElem.SetAttribute("value", val);
				}
					// create new element with key/value pair and add it
				else
				{
					
					sNode = key.Substring(key.LastIndexOf("//")+2);
					
					XmlElement entry = doc.CreateElement(sNode.Substring(0, sNode.IndexOf("[@")).Trim());
					sNode =  sNode.Substring(sNode.IndexOf("'")+1);
					
					entry.SetAttribute("key", sNode.Substring(0, sNode.IndexOf("'")) );
					
					entry.SetAttribute("value", val);
					node.AppendChild(entry);
				}
				saveDoc(doc, this._cfgFile);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool removeElement (string key)
		{
			XmlDocument doc = new XmlDocument();
			loadDoc(doc);
			try
			{
				string sNode = key.Substring(0, key.LastIndexOf("//"));
				// retrieve the appSettings node
				node =  doc.SelectSingleNode(sNode);
				if( node == null )
					return false;
				// XPath select setting "add" element that contains this key to remove
				XmlNode nd = node.SelectSingleNode(key);
				node.RemoveChild(nd);
				saveDoc(doc, this._cfgFile);
				return true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		private void loadDoc ( XmlDocument doc )
		{
            string data = ConfigFile.Load(this._cfgFile);
            if (data == "")
            {
                Option.saveDoc(this._cfgFile);
                data = ConfigFile.Load(this._cfgFile);
            }
            System.IO.StringReader sr = new System.IO.StringReader(data);
            doc.Load(sr);

			//doc.Load(this._cfgFile);
		}

        
        private void saveDoc(XmlDocument doc, string docPath)
        {            
            try
            {
                ConfigFile.WriteXML(docPath, doc.InnerXml);

                //XmlTextWriter writer = new XmlTextWriter(docPath, null);
                //writer.Formatting = Formatting.Indented;
                //doc.WriteTo(writer);
                //writer.Flush();
                //writer.Close();
                return;
            }
            catch
            { }
        }
	}
}
