using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GTAVMapConverter
{
    public partial class Form1 : Form
    {
        private List<string> objects = new List<string>();
        private List<string> vehicles = new List<string>();
        private List<string> peds = new List<string>();
        private List<string> pickups = new List<string>();
        private List<string> markers = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != 0)
            {
                MessageBox.Show("The Settings available to C# API Code Convertor only!");
                return;
            }
            ConvertSettings f = new ConvertSettings();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            objects.Clear();
            vehicles.Clear();
            peds.Clear();
            pickups.Clear();
            markers.Clear();
            XmlDocument xmld = new XmlDocument();
            try
            {
                xmld.LoadXml(richTextBox1.Text);
            }
            catch
            {
                MessageBox.Show("Not a valid XML Code.");
                return;
            }
            string type = "";
            string[] pos = new string[3];
            string[] rot = new string[3];
            string[] qua = new string[4];
            string[] scale = new string[3];
            string[] args = new string[5];
            string hash = "";

            if (comboBox2.SelectedIndex == 2 || comboBox2.SelectedIndex == 0 && richTextBox1.Text.IndexOf("SpoonerPlacements") != -1)
            {
                XmlNodeList nodelist = xmld.SelectNodes("//Placement");
                foreach (XmlNode node in nodelist)
                {
                    hash = HexToNumString(node.SelectSingleNode("ModelHash").InnerText);
                    pos[0] = node.SelectSingleNode("PositionRotation/X").InnerText;
                    pos[1] = node.SelectSingleNode("PositionRotation/Y").InnerText;
                    pos[2] = node.SelectSingleNode("PositionRotation/Z").InnerText;
                    rot[0] = node.SelectSingleNode("PositionRotation/Pitch").InnerText;
                    rot[1] = node.SelectSingleNode("PositionRotation/Roll").InnerText;
                    rot[2] = node.SelectSingleNode("PositionRotation/Yaw").InnerText;

                    if (node.SelectSingleNode("ObjectProperties") != null)
                        type = "Prop";
                    else if (node.SelectSingleNode("VehicleProperties") != null)
                    {
                        args[0] = node.SelectSingleNode("VehicleProperties/Colours/Primary").InnerText;
                        args[1] = node.SelectSingleNode("VehicleProperties/Colours/Secondary").InnerText;
                        type = "Vehicle";
                    }
                    else if (node.SelectSingleNode("PedProperties") != null)
                        type = "Ped";
                    else if (node.SelectSingleNode("PickupProperties") != null)
                    {
                        args[0] = "-1";
                        args[1] = "-1";
                        type = "Pickup";
                    }

                    AddLine(type, hash, pos, rot, qua, scale, args, false);
                }

                nodelist = xmld.SelectNodes("//Marker");
                foreach (XmlNode node in nodelist)
                {
                    hash = node.SelectSingleNode("Type").InnerText;
                    pos[0] = node.SelectSingleNode("Position").Attributes["X"].InnerText;
                    pos[1] = node.SelectSingleNode("Position").Attributes["Y"].InnerText;
                    pos[2] = node.SelectSingleNode("Position").Attributes["Z"].InnerText;
                    rot[0] = node.SelectSingleNode("Rotation").Attributes["X"].InnerText;
                    rot[1] = node.SelectSingleNode("Rotation").Attributes["Y"].InnerText;
                    rot[2] = node.SelectSingleNode("Rotation").Attributes["Z"].InnerText;
                    scale[0] = node.SelectSingleNode("Scale").InnerText;
                    scale[1] = node.SelectSingleNode("Scale").InnerText;
                    scale[2] = node.SelectSingleNode("Scale").InnerText;
                    args[0] = node.SelectSingleNode("Colour").Attributes["R"].InnerText;
                    args[1] = node.SelectSingleNode("Colour").Attributes["G"].InnerText;
                    args[2] = node.SelectSingleNode("Colour").Attributes["B"].InnerText;
                    args[3] = node.SelectSingleNode("Colour").Attributes["A"].InnerText;

                    AddLine("Marker", hash, pos, rot, qua, scale, args, false);
                }
            }
            else if (comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 0 && richTextBox1.Text.IndexOf("MapObject") != -1)
            {

                XmlNodeList nodelist = xmld.SelectNodes("//MapObject");

                foreach (XmlNode node in nodelist)
                {
                    type = node.SelectSingleNode("Type").InnerText;
                    hash = node.SelectSingleNode("Hash").InnerText;
                    pos[0] = node.SelectSingleNode("Position/X").InnerText;
                    pos[1] = node.SelectSingleNode("Position/Y").InnerText;
                    pos[2] = node.SelectSingleNode("Position/Z").InnerText;
                    rot[0] = node.SelectSingleNode("Rotation/X").InnerText;
                    rot[1] = node.SelectSingleNode("Rotation/Y").InnerText;
                    rot[2] = node.SelectSingleNode("Rotation/Z").InnerText;
                    qua[0] = node.SelectSingleNode("Quaternion/X").InnerText;
                    qua[1] = node.SelectSingleNode("Quaternion/Y").InnerText;
                    qua[2] = node.SelectSingleNode("Quaternion/Z").InnerText;
                    qua[3] = node.SelectSingleNode("Quaternion/W").InnerText;

                    if (type == "Vehicle")
                    {
                        args[0] = node.SelectSingleNode("PrimaryColor").InnerText;
                        args[1] = node.SelectSingleNode("SecondaryColor").InnerText;
                    }
                    else if (type == "Pickup")
                    {
                        args[0] = node.SelectSingleNode("Amount").InnerText;
                        args[1] = node.SelectSingleNode("RespawnTime").InnerText;
                    }

                    AddLine(type, hash, pos, rot, qua, scale, args);
                }

                nodelist = xmld.SelectNodes("//Marker");
                foreach (XmlNode node in nodelist)
                {

                    type = node.SelectSingleNode("Type").InnerText;
                    pos[0] = node.SelectSingleNode("Position/X").InnerText;
                    pos[1] = node.SelectSingleNode("Position/Y").InnerText;
                    pos[2] = node.SelectSingleNode("Position/Z").InnerText;
                    rot[0] = node.SelectSingleNode("Rotation/X").InnerText;
                    rot[1] = node.SelectSingleNode("Rotation/Y").InnerText;
                    rot[2] = node.SelectSingleNode("Rotation/Z").InnerText;
                    scale[0] = node.SelectSingleNode("Scale/X").InnerText;
                    scale[1] = node.SelectSingleNode("Scale/Y").InnerText;
                    scale[2] = node.SelectSingleNode("Scale/Z").InnerText;
                    args[0] = node.SelectSingleNode("Red").InnerText;
                    args[1] = node.SelectSingleNode("Green").InnerText;
                    args[2] = node.SelectSingleNode("Blue").InnerText;
                    args[3] = node.SelectSingleNode("Alpha").InnerText;
                    args[4] = node.SelectSingleNode("BobUpAndDown").InnerText;

                    string[] marklist = {"UpsideDownCone","VerticalCylinder","ThickCevronUp","ThinCevronUp","CheckeredFlagRect",
                                "CheckeredFlagCircle","VerticalCircle","PlaneModel","LostMCDark","LostMCLight","Number0","Number1","Number2",
                                "Number3","Number4","Number5","Number6","Number7","Number8","Number9","ChevronUpX1","ChevronUpX2","ChevronUpX3",
                                "HorizontalCircleFlat","ReplayIcon","HorizontalCircleSkinny","HorizontalCircleArrow","HorizontalSplitArrowCircle",
                                "DebugSphere"};

                    int i = 0;
                    for (i = 0; i < marklist.Length; i++)
                        if (marklist[i] == type) break;

                    AddLine("Marker", i.ToString(), pos, rot, qua, scale, args);

                }
            }

            richTextBox1.Text = "";
            if (comboBox1.SelectedIndex == 0)
            {
                if (conSettings.objectsToArray && objects.Count > 0) richTextBox1.Text += "NetHandle[] " + conSettings.objectsArrayName + " = new NetHandle[" + (objects.Count + conSettings.objectsArrayStartCount) + "];\r\n";
                if (conSettings.vehiclesToArray && vehicles.Count > 0) richTextBox1.Text += "NetHandle[] " + conSettings.vehiclesArrayName + " = new NetHandle[" + (vehicles.Count + conSettings.vehiclesArrayStartCount) + "];\r\n";
                if (conSettings.pedsToArray && peds.Count > 0) richTextBox1.Text += "NetHandle[] " + conSettings.pedsArrayName + " = new NetHandle[" + (peds.Count + conSettings.pedsArrayStartCount) + "];\r\n";
                if (conSettings.pickupsToArray && pickups.Count > 0) richTextBox1.Text += "NetHandle[] " + conSettings.pickupsArrayName + "  = new NetHandle[" + (pickups.Count + conSettings.pickupsArrayStartCount) + "];\r\n";
                if (conSettings.markersToArray && markers.Count > 0) richTextBox1.Text += "NetHandle[] " + conSettings.markersArrayName + "  = new NetHandle[" + (markers.Count + conSettings.markersArrayStartCount) + "];\r\n";

                AddLineAPI("Objects", objects, conSettings.objectsArrayStartCount, conSettings.objectsToArray, conSettings.objectsArrayName);
                AddLineAPI("Vehicles", vehicles, conSettings.vehiclesArrayStartCount, conSettings.vehiclesToArray, conSettings.vehiclesArrayName);
                AddLineAPI("Peds", peds, conSettings.pedsArrayStartCount, conSettings.pedsToArray, conSettings.pedsArrayName);
                AddLineAPI("Pickups", pickups, conSettings.pickupsArrayStartCount, conSettings.pickupsToArray, conSettings.pickupsArrayName);
                AddLineAPI("Markers", markers, conSettings.markersArrayStartCount, conSettings.markersToArray, conSettings.markersArrayName);      
            }
            else
            {
                richTextBox1.Text += "<map>\r\n";

                AddLineMap("Objects", objects);
                AddLineMap("Vehicles", vehicles);
                AddLineMap("Peds", peds);
                AddLineMap("Pickups", pickups);
                AddLineMap("Markers", markers);

                richTextBox1.Text += "</map>";
            }
            label2.Text = "Total Converted: " + (objects.Count + vehicles.Count + peds.Count + pickups.Count + markers.Count);
        }

        private void AddLineAPI(string Type,List<string> list,int startCount,bool isArray,string arrayName)
        {
            if(list.Count > 0)
            {
                int count = startCount;
                richTextBox1.Text += "// " + Type + " (Total: " + list.Count + ")\r\n";
                foreach (string line in list)
                {
                    if (isArray) richTextBox1.Text += arrayName + "[" + count++ + "] = ";
                    richTextBox1.Text += line + "\r\n";
                }
            }
        }

        private void AddLineMap(string Type, List<string> list)
        {
            richTextBox1.Text += "<!-- " + Type +  " (Total: " + list.Count + ") -->\r\n";
            foreach (string line in list)
                richTextBox1.Text += line + "\r\n";
        }

        private void AddLine(string type,string hash,string[] pos,string[] rot,string[] qua,string[] scale,string[] args,bool allowqua = true)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (type == "Prop")
                {
                    if (conSettings.objectRotType == 0 || !allowqua)
                        objects.Add(String.Format("{0}({1}, new Vector3({2}, {3}, {4}), new Vector3({5}, {6}, {7}), {8});", conSettings.objectString, hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], conSettings.objectsDimension));
                    else
                        objects.Add(String.Format("{0}({1}, new Vector3({2}, {3}, {4}), new Quaternion ({5}, {6}, {7}, {8}), {9});", conSettings.objectString, hash, pos[0], pos[1], pos[2], qua[0], qua[1], qua[2], qua[3], conSettings.objectsDimension));
                }
                else if (type == "Vehicle")
                    vehicles.Add(String.Format("{0}((VehicleHash){1}, new Vector3({2}, {3}, {4}), new Vector3({5}, {6}, {7}), {8}, {9}, {10});", conSettings.vehicleString, hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], args[0], args[1], conSettings.vehiclesDimension));

                else if (type == "Ped")
                    peds.Add(String.Format("{0}((PedHash){1}, new Vector3({2}, {3}, {4}), {5}, {6});", conSettings.pedString, hash, pos[0], pos[1], pos[2], rot[2], conSettings.pedsDimension));

                else if (type == "Pickup")
                    pickups.Add(String.Format("{0}((PickupHash){1}, new Vector3({2}, {3}, {4}), new Vector3({5}, {6}, {7}), {8}, {9}, {10});", conSettings.pickupString, hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], args[0], args[1], conSettings.pickupsDimension));
                else if(type == "Marker")
                    markers.Add(String.Format("{0}({1}, new Vector3({2}, {3}, {4}), new Vector3(), new Vector3({5}, {6}, {7}), new Vector({8}, {9}, {10}), {11}, {12}, {13}, {14}, {15}, {16});", conSettings.markerString, hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], scale[0], scale[1], scale[2],
                            args[0], args[1], args[2], args[3], conSettings.markersDimension, args[4]));

            }
            else
            {
                if (type == "Prop")
                    objects.Add(String.Format("<prop model=\"{0}\" posX=\"{1}\" posY=\"{2}\" posZ=\"{3}\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" />", hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2]));

                else if (type == "Vehicle")
                    vehicles.Add(String.Format("<vehicle model=\"{0}\" posX=\"{1}\" posY=\"{2}\" posZ=\"{3}\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" color1=\"{7}\" color2=\"{8}\" />", hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], args[0], args[1]));

                else if (type == "Ped")
                    peds.Add(String.Format("<ped model=\"{0}\" posX=\"{1}\" posY=\"{2}\"  posZ=\"{3})\" heading=\"{4}\" />", hash, pos[0], pos[1], pos[2], rot[2]));

                else if (type == "Pickup")
                    pickups.Add(String.Format("<pickup model=\"{0}\" posX=\"{1}\" posY=\"{2}\"  posZ=\"{3})\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" amount=\"{7}\" respawn=\"{8}\" />", hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], args[0], args[1]));
                else if(type == "Marker")
                    markers.Add(String.Format("<marker model=\"{0}\" posX=\"{1}\" posY=\"{2}\" posZ=\"{3}\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" scaleX=\"{7}\" scaleY=\"{8}\" scale=\"{9}\"Z red=\"{10}\" green=\"{11}\" blue=\"{12}\" alpha=\"{13}\" />",
                            hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], scale[0], scale[1], scale[2], args[0], args[1], args[2], args[3]));

            }
        }

        private string HexToNumString(string hex)
        {
            long temp = 0;
            long.TryParse(hex.Replace("x",string.Empty), System.Globalization.NumberStyles.HexNumber,null,out temp);
            return temp.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
    }
}
