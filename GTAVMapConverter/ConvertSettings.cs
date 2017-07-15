using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAVMapConverter
{
    public partial class ConvertSettings : Form
    {
        public ConvertSettings()
        {
            InitializeComponent();
        }

        private void ConvertSettings_Load(object sender, EventArgs e)
        {
            textBox1.Text = conSettings.objectString;
            textBox2.Text = conSettings.vehicleString;
            textBox3.Text = conSettings.pedString;
            textBox4.Text = conSettings.pickupString;
            textBox10.Text = conSettings.markerString;

            textBox5.Text = conSettings.objectsArrayName;
            textBox6.Text = conSettings.vehiclesArrayName;
            textBox7.Text = conSettings.pedsArrayName;
            textBox8.Text = conSettings.pickupsArrayName;
            textBox9.Text = conSettings.markersArrayName;

            comboBox1.SelectedIndex = conSettings.objectRotType;
            numericUpDown1.Value = conSettings.vehiclesDimension;
            numericUpDown2.Value = conSettings.pedsDimension;
            numericUpDown3.Value = conSettings.pickupsDimension;
            numericUpDown4.Value = conSettings.objectsDimension;
            numericUpDown10.Value = conSettings.markersDimension;

            numericUpDown5.Value = conSettings.objectsArrayStartCount;
            numericUpDown6.Value = conSettings.vehiclesArrayStartCount;
            numericUpDown7.Value = conSettings.pedsArrayStartCount;
            numericUpDown8.Value = conSettings.pickupsArrayStartCount;
            numericUpDown9.Value = conSettings.markersArrayStartCount;
            

            checkBox1.Checked = conSettings.objectsToArray;
            checkBox2.Checked = conSettings.vehiclesToArray;
            checkBox3.Checked = conSettings.pedsToArray;
            checkBox4.Checked = conSettings.pickupsToArray;
            checkBox5.Checked = conSettings.markersToArray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conSettings.objectString = textBox1.Text;
            conSettings.vehicleString = textBox2.Text;
            conSettings.pedString = textBox3.Text;
            conSettings.pickupString = textBox4.Text;
            conSettings.markerString = textBox10.Text;

            conSettings.objectRotType = comboBox1.SelectedIndex;
            conSettings.vehiclesDimension = (int)numericUpDown1.Value;
            conSettings.pedsDimension = (int)numericUpDown2.Value;
            conSettings.pickupsDimension = (int)numericUpDown3.Value;
            conSettings.objectsDimension = (int)numericUpDown4.Value;
            conSettings.markersDimension = (int)numericUpDown10.Value;

            conSettings.objectsToArray = checkBox1.Checked;
            conSettings.vehiclesToArray = checkBox2.Checked;
            conSettings.pedsToArray = checkBox3.Checked;
            conSettings.pickupsToArray = checkBox4.Checked;
            conSettings.markersToArray = checkBox5.Checked;

            conSettings.objectsArrayName = textBox5.Text;
            conSettings.vehiclesArrayName = textBox6.Text;
            conSettings.pedsArrayName = textBox7.Text;
            conSettings.pickupsArrayName = textBox8.Text;
            conSettings.markersArrayName = textBox9.Text;

            conSettings.objectsArrayStartCount = (int)numericUpDown5.Value;
            conSettings.vehiclesArrayStartCount = (int)numericUpDown6.Value;
            conSettings.pedsArrayStartCount = (int)numericUpDown7.Value;
            conSettings.pickupsArrayStartCount = (int)numericUpDown8.Value;
            conSettings.markersArrayStartCount = (int)numericUpDown9.Value;

            Close();
        }
    }
}
