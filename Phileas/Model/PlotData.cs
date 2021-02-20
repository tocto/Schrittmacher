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
    public class PlotData : INotifyPropertyChanged, IXmlSerializable
    {
        private string title = string.Empty;

        private string xAxisTitle = string.Empty;

        private string yAxisTitle = string.Empty;

        private uint numberOfSteps = 100;

        private Dictionary<string, List<double>> dataPoints = new Dictionary<string, List<double>>();

        [XmlElement("DataPointOrder")]
        public List<string> DataPointOrder
        {
            get => this.dataPoints.Keys.ToList();
        }

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
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "Name":
                        this.title = reader.ReadContentAsString();
                        break;
                    case "XAxisTitle":
                        this.XAxisTitle = reader.ReadContentAsString(); 
                        break;
                    case "YAxisTitle":
                        this.YAxisTitle = reader.ReadContentAsString(); 
                        break;
                    case "XParameter":
                        this.xParameterKey = reader.ReadContentAsString(); 
                        break;
                    case "YParameter":
                        this.yParameterKey = reader.ReadContentAsString(); 
                        break;
                    case "NumberOfSteps":
                        this.numberOfSteps = (uint) reader.ReadContentAsInt(); 
                        break;
                    default: break;
                }

                if (reader.Name == "DataPoints" && reader.IsStartElement())
                {
                    while (reader.Read())
                    {
                        string key = reader.Name;
                        this.DataPoints.Add(key,reader.ReadContentAsString().Split(",").Select(double.Parse).ToList());
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
    }
}
