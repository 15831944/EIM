﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Kingdee.CAPP.Componect
{
    public partial class BaseForm : DockContent
    {
        //public BaseForm()
        //{
        //    InitializeComponent();
        //}
        public virtual void SetPropertyEvent(object obj) { }        
    }
}