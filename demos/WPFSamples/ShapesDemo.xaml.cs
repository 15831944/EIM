﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SC.WPFSamples
{
    /// <summary>
    /// Interaction logic for ShapesDemo.xaml
    /// </summary>
    public partial class ShapesDemo : Window
    {
        public ShapesDemo()
        {
            InitializeComponent();
            mouth.Data = Geometry.Parse("M 40,92 Q 57,75 80,92");
        }
    }
}
