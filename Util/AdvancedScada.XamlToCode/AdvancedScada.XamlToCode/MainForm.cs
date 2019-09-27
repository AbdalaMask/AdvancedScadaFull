/*
 * Created by SharpDevelop.
 * User: User
 * Date: 27/09/2019
 * Time: 02:33 م
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Markup;
using AdvancedScada.ConvertControls;
using ComponentFactory.Krypton.Toolkit;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.XamlToCode
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : KryptonForm
    {
		private bool _validXaml;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
        private void GenerateXAMLVisualTree(string xaml)
        {
            using (var ms = new MemoryStream(xaml.Length))
            {
                using (var sw = new StreamWriter(ms))
                {
                    try
                    {
                        sw.Write(xaml);
                        sw.Flush();

                        ms.Seek(0, SeekOrigin.Begin);

                        // Load the Xaml
                        // object content = ActivityXamlServices.Load(ms);


                    }
                    catch (XamlParseException x)
                    {
                        Debug.WriteLine("XAML Parse error: Line:{0}, Position:{1}, Error: {2}", x.LineNumber, x.LinePosition, x.Message);
                        
                        _validXaml = false;
                    }
                    catch (Exception ex)
                    {
                       
                        _validXaml = false;
                    }
                }
            }
        }

        void BtnConvertClick(object sender, EventArgs e)
        {
            try
            {
                var srcCode = new XamlConvertor().ConvertToString(txtXAML.Text);
                txtCode.Text = srcCode;
                tbXamlToCode.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }


        }
        void MainFormLoad(object sender, EventArgs e)
		{
	
		}

        private void TsUserControl_Click(object sender, EventArgs e)
        {

            try
            {
                _validXaml = true;

                // Generate and display the XAMl visual tree
                GenerateXAMLVisualTree(txtXAML.Text);
                // Only continue if the XAML is valid
                if (_validXaml)
                {
                    var cnv = new XamlToCodeConverter();

                    // Generate the code for this XAML
                    var srcCode = cnv.Convert(txtXAML.Text);


                    txtCode.Text = srcCode;
                    tbXamlToCode.SelectedIndex = 1;
                    // Compile the code and show the visual tree for the code
                    var res = cnv.CompileAssemblyFromLastCodeCompileUnit();
                    if (res.Errors.Count > 0)
                        foreach (CompilerError err in res.Errors)
                        {
                            var errorMsg = string.Format("Line: {0}, Column: {1}: {2}", err.Line, err.Column,
                                err.ErrorText);
                            Debug.WriteLine(errorMsg);

                        }
                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }

        }

        private void TsBake_Click(object sender, EventArgs e)
        {
            tbXamlToCode.SelectedIndex = 0;
           
        }

        private void TsClear_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtXAML.Clear();
        }
    }
}
