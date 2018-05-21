using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace UniSpell
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		Dictionary<string, string> UnicodeNames = null;

		private void button1_Click(object sender, EventArgs e)
		{
			if (UnicodeNames == null)
				InitUnicodeNames();

			var gv = dataGridView1;
			gv.Rows.Clear();

			foreach(var c in textBox1.Text)
			{
				var x = ((int)c).ToString("X4");
				var s = "[not found]";
				UnicodeNames.TryGetValue(x, out s);

				//sb.AppendLine(string.Format("U+{1:X4} {0} {2}", c, (int)c, s));

				var row = new DataGridViewRow();
				row.Cells.Add(new DataGridViewTextBoxCell { Value = string.Format("U+{0:X4}", (int)c) });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = c });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = s });
				gv.Rows.Add(row);
			}
		}

		private void InitUnicodeNames()
		{
			UnicodeNames = new Dictionary<string, string>();

			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "UniSpell.UnicodeData.txt";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
				using (StreamReader reader = new StreamReader(stream))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						var rgs = line.Split(";".ToArray());

						UnicodeNames.Add(rgs[0], rgs[1]);
					}
				}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://devio.at/");
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.unicode.org/copyright.html");
		}
	}
}
