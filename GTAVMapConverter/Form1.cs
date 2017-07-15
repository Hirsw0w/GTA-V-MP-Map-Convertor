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
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != 0)
            {
                MessageBox.Show("The Settings available to GTA:Network C# Code Convertor only!");
                return;
            }
            ConvertSettings f = new ConvertSettings();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            List<string> objects = new List<string>();
            List<string> vehicles = new List<string>();
            List<string> peds = new List<string>();
            List<string> pickups = new List<string>();
            List<string> markers = new List<string>();

            XmlDocument xmld = new XmlDocument();
            xmld.LoadXml(richTextBox1.Text);

            XmlNodeList nodelist = xmld.SelectNodes("//MapObject");

            foreach (XmlNode node in nodelist)
            {
                string type = "";
                string[] pos = new string[3];
                string[] rot = new string[3];
                string[] qua = new string[4];
                string[] args = new string[2];
                string hash = "";

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
                    args[1] = node.SelectSingleNode("Respawn").InnerText;
                }

                if (comboBox1.SelectedIndex == 0)
                {
                    if (type == "Prop")
                    {
                        if (conSettings.objectRotType == 0)
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

                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    if (type == "Prop")
                        objects.Add(String.Format("<prop model=\"{0}\" posX=\"{1}\" posY=\"{2}\" posZ=\"{3}\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" />", hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2]));

                    else if (type == "Vehicle")
                        vehicles.Add(String.Format("<vehicle model=\"{0}\" posX=\"{1}\" posY=\"{2}\" posZ=\"{3}\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" color1=\"{7}\" color2=\"{8}\" />", hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], args[0], args[1]));

                    else if (type == "Ped")
                        peds.Add(String.Format("<ped model=\"{0}\" posX=\"{1}\" posY=\"{2}\"  posZ=\"{3})\" heading=\"{4}\" />", hash, pos[0], pos[1], pos[2], rot[2]));

                    else if (type == "Pickup")
                        pickups.Add(String.Format("<pickup model=\"{0}\" posX=\"{1}\" posY=\"{2}\"  posZ=\"{3})\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" amount=\"{7}\" respawn=\"{8}\" />", hash, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], args[0], args[1]));

                }
            }

            nodelist = xmld.SelectNodes("//Marker");
            foreach(XmlNode node in nodelist)
            {
                string type = "";
                string[] pos = new string[3];
                string[] rot = new string[3];
                string[] scale = new string[3];
                string[] args = new string[5];

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

                if (comboBox1.SelectedIndex == 0)
                    markers.Add(String.Format("{0}({1}, new Vector3({2}, {3}, {4}), new Vector3(), new Vector3({5}, {6}, {7}), new Vector({8}, {9}, {10}), {11}, {12}, {13}, {14}, {15}, {16});", conSettings.markerString, i, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], scale[0], scale[1], scale[2],
                        args[0], args[1], args[2], args[3], conSettings.markersDimension, args[4]));

                else if (comboBox1.SelectedIndex == 1)
                    markers.Add(String.Format("<marker model=\"{0}\" posX=\"{1}\" posY=\"{2}\" posZ=\"{3}\" rotX=\"{4}\" rotY=\"{5}\" rotZ=\"{6}\" scaleX=\"{7}\" scaleY=\"{8}\" scale=\"{9}\"Z red=\"{10}\" green=\"{11}\" blue=\"{12}\" alpha=\"{13}\" />",
                        i, pos[0], pos[1], pos[2], rot[0], rot[1], rot[2], scale[0], scale[1], scale[2], args[0], args[1], args[2], args[3]));

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
            else if(comboBox1.SelectedIndex == 1)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
