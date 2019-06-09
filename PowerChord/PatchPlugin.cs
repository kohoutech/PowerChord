/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 1995-2019  George E Greaney

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

using Transonic.MIDI;

namespace PatchWorker.Graph
{
    public class PatchPlugin
    {
        //get plugin's name
        public virtual String getName()
        {
            return "patchworker plugin";
        }

        //get number of plugin inputs
        public virtual int getInputCount()
        {
            return 1;
        }

        //get number of plugin output
        public virtual int getOutputCount()
        {
            return 1;
        }

        //show plugin dialog when user clicks on patchbox title
        public virtual void showPluginDialog()
        {
        }

        //send MIDI msg to plugin; plugin will send response back to modifier's <sendMidiMessage> method
        public virtual void handleMidiMessage(Message msg)
        {
        }
    }
}
