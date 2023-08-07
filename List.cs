using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace UnFollowers
{
	public partial class List : Form
	{
		public List(List<string> itemList)
		{
			InitializeComponent();
			lb1.Items.AddRange(itemList.ToArray());
		}

		private void List_Load(object sender, EventArgs e)
		{
			this.Text = lb1.Items.Count.ToString();
		}

	}
}
