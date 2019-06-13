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
using System.Linq;
using System.Text;
using System.Drawing;

using Transonic.MIDI;
using PatchWorker.Plugin;

namespace PowerChord
{
    public class PowerChord : IPatchPlugin
    {
        public IPatchModifer modifier;

        bool[] keysdown;
        public bool holdOn;
        List<int> chordNotes;

        PowerChordDialog plugindlg;
        int controlPanelX;
        int controlPanelY;

        public PowerChord()
        {
            modifier = null;
            keysdown = new bool[128];
            switchOff();
            chordNotes = new List<int>();

            plugindlg = null;
            controlPanelX = 100;
            controlPanelY = 100;
        }

        public void setModifier(IPatchModifer _modifier)
        {
            modifier = _modifier;
        }

        public string getName()
        {
            return "Power Chord";
        }

        public int getInputCount()
        {
            return 1;
        }

        public int getOutputCount()
        {
            return 1;
        }

        //- dialog methods ----------------------------------------------------

        //only show dialog once
        public void showPluginDialog(String title)
        {
            if (plugindlg == null)
            {
                plugindlg = new PowerChordDialog(this);
                plugindlg.Text = title;
                plugindlg.Location = new Point(controlPanelX, controlPanelY);
                plugindlg.FormClosing += new System.Windows.Forms.FormClosingEventHandler(plugindlg_FormClosing);
                plugindlg.Show();
            }
        }

        void plugindlg_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (plugindlg != null)
            {
                controlPanelX = plugindlg.Location.X;
                controlPanelY = plugindlg.Location.Y;
                plugindlg = null;
            }
        }

        public void closePluginDialog()
        {
            if (plugindlg != null)
            {
                plugindlg.Close();
            }
        }

        public void switchOn()
        {
            holdOn = true;
            chordNotes.Clear();
            int baseNote = -1;
            for (int i = 0; i < 128; i++)
            {
                if (keysdown[i])
                {
                    if (baseNote == -1)
                    {
                        baseNote = i;
                    }
                    else
                    {
                        chordNotes.Add(i - baseNote);
                    }
                }
            }
        }

        public void switchOff()
        {
            for (int i = 0; i < 128; i++)
            {
                keysdown[i] = false;
            }

            holdOn = false;
        }

        //- midi processing ---------------------------------------------------

        public void handleMidiMessage(byte[] msgData)
        {

            Message msg = Message.getMessage(msgData);

            if (!holdOn)            //tracking note on msgs when hold is off
            {
                if (msg is NoteOnMessage)
                {
                    NoteOnMessage noteOn = (NoteOnMessage)msg;
                    keysdown[noteOn.noteNumber] = true;
                }
                else if (msg is NoteOffMessage)
                {
                    NoteOffMessage noteOff = (NoteOffMessage)msg;
                    keysdown[noteOff.noteNumber] = false;
                }
                modifier.sendMidiMsg(msg.getDataBytes());
            }
            else
            {
                if (msg is NoteOnMessage)
                {
                    NoteOnMessage noteOn = (NoteOnMessage)msg;
                    int baseNote = noteOn.noteNumber;
                    modifier.sendMidiMsg(msg.getDataBytes());
                    foreach (int interval in chordNotes)
                    {
                        NoteOnMessage newnote = (NoteOnMessage)noteOn.copy();
                        newnote.noteNumber += interval;
                        Console.WriteLine("new note number " + newnote.noteNumber);
                        modifier.sendMidiMsg(newnote.getDataBytes());
                    }
                }
                else if (msg is NoteOffMessage)
                {
                    NoteOffMessage noteOff = (NoteOffMessage)msg;
                    modifier.sendMidiMsg(msg.getDataBytes());
                    foreach (int interval in chordNotes)
                    {
                        NoteOffMessage newnote = (NoteOffMessage)noteOff.copy();
                        newnote.noteNumber += interval;
                        modifier.sendMidiMsg(newnote.getDataBytes());
                    }
                }
                else
                {
                    modifier.sendMidiMsg(msg.getDataBytes());
                }
            }
        }

        public void loadFromPatch(Origami.ENAML.EnamlData data, string dataPath)
        {
            String pcPath = dataPath + ".power-chord";
            controlPanelX = data.getIntValue(pcPath + ".control-panel-x", 100);
            controlPanelY = data.getIntValue(pcPath + ".control-panel-y", 100);
        }

        public void saveToPatch(Origami.ENAML.EnamlData data, string dataPath)
        {
            String pcPath = dataPath + ".power-chord";
            if (plugindlg != null) 
            {
                controlPanelX = plugindlg.Location.X;
                controlPanelY = plugindlg.Location.Y;
            }
            data.setIntValue(pcPath + ".control-panel-x", controlPanelX);
            data.setIntValue(pcPath + ".control-panel-y", controlPanelY);
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");