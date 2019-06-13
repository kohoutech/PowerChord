/* ----------------------------------------------------------------------------
PowerChord : a harmonizer plugin for PatchWorker
Copyright (C) 1998-2019  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PowerChord
{
    public partial class PowerChordDialog : Form
    {
        private Button btnHoldOn;
        private Label lblChordHold;
        PowerChord powerChord;

        public PowerChordDialog(PowerChord _powerChord)
        {
            powerChord = _powerChord;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerChordDialog));
            this.btnHoldOn = new System.Windows.Forms.Button();
            this.lblChordHold = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnHoldOn
            // 
            this.btnHoldOn.BackgroundImage = global::PowerChord.Properties.Resources.offswitch;
            this.btnHoldOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnHoldOn.Location = new System.Drawing.Point(62, 46);
            this.btnHoldOn.Name = "btnHoldOn";
            this.btnHoldOn.Size = new System.Drawing.Size(30, 50);
            this.btnHoldOn.TabIndex = 0;
            this.btnHoldOn.UseVisualStyleBackColor = true;
            this.btnHoldOn.Click += new System.EventHandler(this.btnHoldIt_Click);
            // 
            // lblChordHold
            // 
            this.lblChordHold.AutoSize = true;
            this.lblChordHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChordHold.ForeColor = System.Drawing.Color.Black;
            this.lblChordHold.Location = new System.Drawing.Point(43, 25);
            this.lblChordHold.Name = "lblChordHold";
            this.lblChordHold.Size = new System.Drawing.Size(69, 15);
            this.lblChordHold.TabIndex = 1;
            this.lblChordHold.Text = "Chord Hold";
            // 
            // PowerChordDialog
            // 
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(154, 121);
            this.Controls.Add(this.lblChordHold);
            this.Controls.Add(this.btnHoldOn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PowerChordDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Power Chord-001";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnHoldIt_Click(object sender, EventArgs e)
        {
            if (powerChord.holdOn)
            {
                powerChord.switchOff();
                btnHoldOn.BackgroundImage = Properties.Resources.offswitch;
            }
            else
            {
                powerChord.switchOn();
                btnHoldOn.BackgroundImage = Properties.Resources.onswitch;
            }
        }

    }
}
