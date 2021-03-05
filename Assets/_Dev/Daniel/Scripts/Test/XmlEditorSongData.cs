using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[System.Serializable]
public class CustomTiming
{
    public float time;
    public float normalized_distance;
}

[XmlRoot(ElementName = "XmlEditorSongData")]
public class XmlEditorSongData
{

    [XmlElement(ElementName = "catalog")]
    public Catalog catalog { get; set; }
    [XmlElement(ElementName = "lyrics")]
    public Lyrics lyrics { get; set; }


    [XmlRoot(ElementName = "catalog")]
    public class Catalog
    {
        [XmlElement(ElementName = "catalogID")]
        public string CatalogID { get; set; }
        [XmlElement(ElementName = "artist")]
        public string Artist { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
    }

    [XmlRoot(ElementName = "CustomLine")]
    public class CustomLine
    {
        [XmlElement(ElementName = "actor")]
        public int Actor { get; set; }
        [XmlElement(ElementName = "timings")]
        public string Timings { get; set; }
        [XmlElement(ElementName = "line_position")]
        public float Line_position { get; set; }
        [XmlElement(ElementName = "start")]
        public float Start { get; set; }
        [XmlElement(ElementName = "end")]
        public float End { get; set; }
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "lyrics")]
    public class Lyrics
    {
        [XmlElement(ElementName = "CustomLine")]
        public List<CustomLine> CustomLine { get; set; }
    }
}
