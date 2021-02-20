using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Phileas.Model
{
    [Serializable]
    public class PlotData : INotifyPropertyChanged, IXmlSerializable, IEquatable<PlotData>
    {
        private string title = string.Empty;

        private string xAxisTitle = string.Empty;

        private string yAxisTitle = string.Empty;

        private uint numberOfSteps = 100;

        private Dictionary<string, List<double>> dataPoints = new Dictionary<string, List<double>>();

        private string xParameterKey { get; set; } = string.Empty;

        private string yParameterKey { get; set; } = string.Empty;

        private bool isLineSmothnessOn = false;

        #region public properties
        public string Title
        {
            get => this.title;

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public string XAxisTitle
        {
            get => this.xAxisTitle;

            set
            {
                if (this.xAxisTitle != value)
                {
                    this.xAxisTitle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XAxisTitle"));
                }
            }
        }

        public string YAxisTitle
        {
            get => this.yAxisTitle;

            set
            {
                if (this.yAxisTitle != value)
                {
                    this.yAxisTitle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("YAxisTitle"));
                }
            }
        }

        public uint NumberOfSteps
        {
            get => this.numberOfSteps;

            set
            {
                if (this.numberOfSteps != value)
                {
                    this.numberOfSteps = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfSteps"));
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, List<double>> DataPoints
        {
            get => this.dataPoints;

            set
            {
                if (this.dataPoints != value)
                {
                    this.dataPoints = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataPoints"));
                }
            }
        }

        public string XParameterKey
        {
            get => this.xParameterKey;

            set
            {
                if (this.xParameterKey != value)
                {
                    this.xParameterKey = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XParameterKey"));
                }
            }
        }

        public string YParameterKey
        {
            get => this.yParameterKey;

            set
            {
                if (this.yParameterKey != value)
                {
                    this.yParameterKey = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("YParameterKey"));
                }
            }
        }

        public bool IsLineSmothnessOn
        {
            get => this.isLineSmothnessOn;

            set
            {
                if (this.isLineSmothnessOn != value)
                {
                    this.isLineSmothnessOn = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLineSmothnessOn"));
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read(); // move reader to first node

            while(reader.NodeType != XmlNodeType.EndElement && !reader.Name.Equals("PlotData")) // stop if finished with PlotData instance
            {
                if (reader.NodeType == XmlNodeType.EndElement) reader.Read(); // move to next node

                switch (reader.Name)
                {
                    case "Name":
                        this.title = (string) reader.ReadElementContentAsString();
                        break;
                    case "XAxisTitle":
                        this.XAxisTitle = (string)reader.ReadElementContentAsString(); 
                        break;
                    case "YAxisTitle":
                        this.YAxisTitle = (string)reader.ReadElementContentAs(typeof(string), null); 
                        break;
                    case "XParameter":
                        this.xParameterKey = (string)reader.ReadElementContentAs(typeof(string), null); 
                        break;
                    case "YParameter":
                        this.yParameterKey = (string)reader.ReadElementContentAs(typeof(string), null); 
                        break;
                    case "NumberOfSteps":
                        this.numberOfSteps = (uint) reader.ReadElementContentAs(typeof(uint), null); 
                        break;
                    case "DataPoints":
                        ReadDataPoints(reader);
                        break;
                    default:
                        reader.Read(); // skip all unknown, e.g. for obsolete elements
                        break;
                }
            }
        }

        private void ReadDataPoints(XmlReader reader)
        {
            if (reader.Name == "DataPoints" && reader.NodeType == XmlNodeType.Element)
            {
                reader.Read(); // move to first child element or end node

                while (!reader.Name.Equals("DataPoints")) // stop when end is reached
                {
                    if (reader.NodeType == XmlNodeType.Element) // ensures skipping end element nodes
                    {
                        string key = reader.Name;
                        this.DataPoints.Add(key, reader.ReadElementContentAsString().Split(",").Select(double.Parse).ToList());
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Name", this.title);
            writer.WriteElementString("XAxisTitle", this.xAxisTitle);
            writer.WriteElementString("YAxisTitle", this.yAxisTitle);
            writer.WriteElementString("XParameter", this.XParameterKey);
            writer.WriteElementString("YParameter", this.yParameterKey);
            writer.WriteElementString("NumberOfSteps", this.numberOfSteps.ToString());

            // transfrom data point dictionary into xml tree
            writer.WriteStartElement("DataPoints");

            foreach(var key in dataPoints.Keys)
            {
                writer.WriteElementString(key, string.Join(",", this.dataPoints[key].ToArray()));
            }

            writer.WriteEndElement();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlotData);
        }

        public bool Equals(PlotData other)
        {
            return other != null &&
                   title == other.title &&
                   xAxisTitle == other.xAxisTitle &&
                   yAxisTitle == other.yAxisTitle &&
                   numberOfSteps == other.numberOfSteps &&
                   IsDataPointsEquals(other.dataPoints) &&
                   xParameterKey == other.xParameterKey &&
                   yParameterKey == other.yParameterKey &&
                   isLineSmothnessOn == other.isLineSmothnessOn;
        }

        public bool IsDataPointsEquals(Dictionary<string, List<double>> other)
        {
            if (!this.dataPoints.Keys.Count.Equals(other.Keys.Count)) return false;

            foreach(string key in this.dataPoints.Keys)
            {
                if (!other.Keys.Contains(key) 
                    || !this.dataPoints[key].SequenceEqual(other[key])) 
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = -1119939092;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(xAxisTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(yAxisTitle);
            hashCode = hashCode * -1521134295 + numberOfSteps.GetHashCode();
            foreach (string key in dataPoints.Keys)
            {
                hashCode = hashCode * -1521134295 + key.GetHashCode();
                foreach (var point in dataPoints[key]) hashCode = hashCode * -1521134295 + point.GetHashCode();
            }
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(xParameterKey);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(yParameterKey);
            hashCode = hashCode * -1521134295 + isLineSmothnessOn.GetHashCode();
            return hashCode;
        }
    }
}
