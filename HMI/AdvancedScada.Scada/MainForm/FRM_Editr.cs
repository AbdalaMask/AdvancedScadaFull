﻿using AdvancedScada.Controls_Binding;
using AdvancedScada.Controls_Binding.Display;
using AdvancedScada.DataAccess;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static AdvancedScada.HMI.Tools.Tools;

namespace AdvancedScada.HMI.MainForm
{

    public partial class FRM_Editr : KryptonForm
    {



        public FRM_Editr()
        {
            InitializeComponent();
            ListTankName = new List<string>();
        }

        private void FRM_Editr_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable DT = new DataTable();
                red_lode.Checked = true;
                red_lode_CheckedChanged(sender, e);
                if (SqlDb.GET_ALL_BatchName().Rows.Count > 0)
                {
                    DT.Clear();

                    DT = SqlDb.GET_ALL_BatchName();
                    comBatchName.DataSource = DT;
                    comBatchName.DisplayMember = "BatchName";
                    //=================================================================
                    comBatchName_2.DataSource = DT;
                    comBatchName_2.DisplayMember = "BatchName";
                }

                if (SqlDb.FillCombo(com_n_T1, "select * from Tanks", "TankName", "TankID").Rows.Count > 0)
                {
                    SqlDb.FillCombo(com_n_T2, "select * from Tanks", "TankName", "TankID");
                    SqlDb.FillCombo(com_n_T3, "select * from Tanks", "TankName", "TankID");
                    SqlDb.FillCombo(com_n_T4, "select * from Tanks", "TankName", "TankID");
                    SqlDb.FillCombo(com_n_T5, "select * from Tanks", "TankName", "TankID");
                    SqlDb.FillCombo(com_n_T6, "select * from Tanks", "TankName", "TankID");
                    SqlDb.FillCombo(com_n_T7, "select * from Tanks", "TankName", "TankID");
                    SqlDb.FillCombo(com_n_T8, "select * from Tanks", "TankName", "TankID");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Write(string adr, dynamic Value)
        {

            Utilities.Write(adr, Value);




        }

        private void lbl_run_Click(object sender, EventArgs e)
        {
            try
            {
                //  MainForm frm = new MainForm();
                MainForm form = Application.OpenForms["MainForm"] as MainForm;
                if (form != null)
                {
                    form.Focus();
                }

                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "Name", comBatchName.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo1", com_n_T1.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo2", com_n_T2.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo3", com_n_T3.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo4", com_n_T4.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo5", com_n_T5.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo6", com_n_T6.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo7", com_n_T7.Text);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\TestApp", "LBL_Name_Silo8", com_n_T8.Text);

                form.LBL_BatchName.Text = comBatchName.Text;
                form.LBL_Name_Silo1.Text = com_n_T1.Text;
                form.LBL_Name_Silo2.Text = com_n_T2.Text;
                form.LBL_Name_Silo3.Text = com_n_T3.Text;
                form.LBL_Name_Silo4.Text = com_n_T4.Text;
                form.LBL_Name_Silo5.Text = com_n_T5.Text;
                form.LBL_Name_Silo6.Text = com_n_T6.Text;
                form.LBL_Name_Silo7.Text = com_n_T7.Text;
                form.LBL_Name_Silo8.Text = com_n_T8.Text;

                //'عنواين التردد السريع

                Write(txt_thnk_sp_hi_1.PLCAddressValueToWrite, txt_thnk_sp_hi_1.Text);

                Write(txt_thnk_sp_hi_2.PLCAddressValueToWrite, txt_thnk_sp_hi_2.Text);

                Write(txt_thnk_sp_hi_3.PLCAddressValueToWrite, txt_thnk_sp_hi_3.Text);

                Write(txt_thnk_sp_hi_4.PLCAddressValueToWrite, txt_thnk_sp_hi_4.Text);

                Write(txt_thnk_sp_hi_5.PLCAddressValueToWrite, txt_thnk_sp_hi_5.Text);

                Write(txt_thnk_sp_hi_6.PLCAddressValueToWrite, txt_thnk_sp_hi_6.Text);

                Write(txt_thnk_sp_hi_7.PLCAddressValueToWrite, txt_thnk_sp_hi_7.Text);

                Write(txt_thnk_sp_hi_8.PLCAddressValueToWrite, txt_thnk_sp_hi_8.Text);




                //عنواين التردد البطى

                Write(txt_thnk_sp_low_1.PLCAddressValueToWrite, txt_thnk_sp_low_1.Text);

                Write(txt_thnk_sp_low_2.PLCAddressValueToWrite, txt_thnk_sp_low_2.Text);

                Write(txt_thnk_sp_low_3.PLCAddressValueToWrite, txt_thnk_sp_low_3.Text);

                Write(txt_thnk_sp_low_4.PLCAddressValueToWrite, txt_thnk_sp_low_4.Text);

                Write(txt_thnk_sp_low_5.PLCAddressValueToWrite, txt_thnk_sp_low_5.Text);

                Write(txt_thnk_sp_low_6.PLCAddressValueToWrite, txt_thnk_sp_low_6.Text);

                Write(txt_thnk_sp_low_7.PLCAddressValueToWrite, txt_thnk_sp_low_7.Text);

                Write(txt_thnk_sp_low_8.PLCAddressValueToWrite, txt_thnk_sp_low_8.Text);


                //عنواين الوزن البطيى
                Write(txt_thnk_low_1.PLCAddressValueToWrite, txt_thnk_low_1.Text);

                Write(txt_thnk_low_2.PLCAddressValueToWrite, txt_thnk_low_2.Text);

                Write(txt_thnk_low_3.PLCAddressValueToWrite, txt_thnk_low_3.Text);

                Write(txt_thnk_low_4.PLCAddressValueToWrite, txt_thnk_low_4.Text);

                Write(txt_thnk_low_5.PLCAddressValueToWrite, txt_thnk_low_5.Text);

                Write(txt_thnk_low_6.PLCAddressValueToWrite, txt_thnk_low_6.Text);

                Write(txt_thnk_low_7.PLCAddressValueToWrite, txt_thnk_low_7.Text);

                Write(txt_thnk_low_8.PLCAddressValueToWrite, txt_thnk_low_8.Text);




                //'استعمالة


                if (com_work_1.Text == "يعمل")
                {
                    Write(com_work_1.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_1.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_1.Text) - int.Parse(txt_thnk_fol_1.Text)}");

                    Write(com_order_1.PLCAddressValueToWrite, com_order_1.Text);

                }
                else
                {
                    Write(com_order_1.PLCAddressValueToWrite, "0");

                    Write(com_work_1.PLCAddressValueToWrite, false);

                }

                if (com_work_2.Text == "يعمل")
                {
                    Write(com_work_2.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_2.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_2.Text) - int.Parse(txt_thnk_fol_2.Text)}");

                    Write(com_order_2.PLCAddressValueToWrite, com_order_2.Text);

                }
                else
                {
                    Write(com_work_2.PLCAddressValueToWrite, false);

                    Write(com_order_2.PLCAddressValueToWrite, "0");

                }
                if (com_work_3.Text == "يعمل")
                {

                    Write(com_work_3.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_3.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_3.Text) - int.Parse(txt_thnk_fol_3.Text)}");

                    Write(com_order_3.PLCAddressValueToWrite, com_order_3.Text);

                }
                else
                {
                    Write(com_work_3.PLCAddressValueToWrite, false);

                    Write(com_order_3.PLCAddressValueToWrite, "0");

                }
                if (com_work_4.Text == "يعمل")
                {
                    Write(com_work_4.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_4.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_4.Text) - int.Parse(txt_thnk_fol_4.Text)}");

                    Write(com_order_4.PLCAddressValueToWrite, com_order_4.Text);

                }
                else
                {
                    Write(com_work_4.PLCAddressValueToWrite, false);

                    Write(com_order_4.PLCAddressValueToWrite, "0");

                }
                if (com_work_5.Text == "يعمل")
                {
                    Write(com_work_5.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_5.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_5.Text) - int.Parse(txt_thnk_fol_5.Text)}");

                    Write(com_order_5.PLCAddressValueToWrite, com_order_5.Text);

                }
                else
                {
                    Write(com_work_5.PLCAddressValueToWrite, false);

                    Write(com_order_5.PLCAddressValueToWrite, "0");

                }
                if (com_work_6.Text == "يعمل")
                {
                    Write(com_work_6.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_6.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_6.Text) - int.Parse(txt_thnk_fol_6.Text)}");

                    Write(com_order_6.PLCAddressValueToWrite, com_order_6.Text);

                }
                else
                {
                    Write(com_work_6.PLCAddressValueToWrite, false);

                    Write(com_order_6.PLCAddressValueToWrite, "0");

                }
                if (com_work_7.Text == "يعمل")
                {
                    Write(com_work_7.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_7.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_7.Text) - int.Parse(txt_thnk_fol_7.Text)}");

                    Write(com_order_7.PLCAddressValueToWrite, com_order_7.Text);
                }
                else
                {
                    Write(com_work_7.PLCAddressValueToWrite, false);

                    Write(com_order_7.PLCAddressValueToWrite, "0");

                }

                if (com_work_8.Text == "يعمل")
                {
                    Write(com_work_8.PLCAddressValueToWrite, true);

                    Write(txt_thnk_set_8.PLCAddressValueToWrite, $"{int.Parse(txt_thnk_set_8.Text) - int.Parse(txt_thnk_fol_8.Text)}");

                    Write(com_order_8.PLCAddressValueToWrite, com_order_8.Text);
                }
                else
                {
                    Write(com_work_8.PLCAddressValueToWrite, false);

                    Write(com_order_8.PLCAddressValueToWrite, "0");

                }


                //الزيت

                //if (com_work_9.Text == "يعمل")
                //{
                //     Write("TEST.PLC.MB.MB8", "1");
                //     Write("40135", txt_thnk_set_oil.Text);
                //}
                //else
                //{
                //    Write("TEST.PLC.MB.MB8", "0");
                //    Write("40135", txt_thnk_set_oil.Text);
                //}




                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void red_lode_CheckedChanged(object sender, EventArgs e)
        {
            if (red_lode.Checked == true)
            {
                btn_lode.Visible = true;
                btn_edit.Visible = false;
                btn_save.Visible = false;
                txt_BatchName.Visible = false;


            }

        }

        private void red_edit_CheckedChanged(object sender, EventArgs e)
        {
            if (red_edit.Checked == true)
            {
                btn_lode.Visible = false;
                btn_save.Visible = false;
                btn_edit.Visible = true;

                txt_BatchName.Visible = true;
            }

        }

        private void red_seva_CheckedChanged(object sender, EventArgs e)
        {
            if (red_seva.Checked == true)
            {
                btn_edit.Visible = false;
                btn_lode.Visible = false;
                btn_save.Visible = true;
                txt_BatchName.Visible = true;

            }

        }


        #region DataBase 
        public void Get_BatchsDetails(ComboBox comBatchName)
        {
            DataTable dt = new DataTable();
            dt = SqlDb.Get_BatchsDetails(comBatchName.Text);

            Get_txt(dt, com_n_T1, ref txt_thnk_set_1, ref txt_thnk_low_1, ref txt_thnk_fol_1, ref txt_thnk_sp_hi_1, ref txt_thnk_sp_low_1, ref com_order_1, ref com_work_1, 0);
            Get_txt(dt, com_n_T2, ref txt_thnk_set_2, ref txt_thnk_low_2, ref txt_thnk_fol_2, ref txt_thnk_sp_hi_2, ref txt_thnk_sp_low_2, ref com_order_2, ref com_work_2, 1);
            Get_txt(dt, com_n_T3, ref txt_thnk_set_3, ref txt_thnk_low_3, ref txt_thnk_fol_3, ref txt_thnk_sp_hi_3, ref txt_thnk_sp_low_3, ref com_order_3, ref com_work_3, 2);
            Get_txt(dt, com_n_T4, ref txt_thnk_set_4, ref txt_thnk_low_4, ref txt_thnk_fol_4, ref txt_thnk_sp_hi_4, ref txt_thnk_sp_low_4, ref com_order_4, ref com_work_4, 3);
            Get_txt(dt, com_n_T5, ref txt_thnk_set_5, ref txt_thnk_low_5, ref txt_thnk_fol_5, ref txt_thnk_sp_hi_5, ref txt_thnk_sp_low_5, ref com_order_5, ref com_work_5, 4);
            Get_txt(dt, com_n_T6, ref txt_thnk_set_6, ref txt_thnk_low_6, ref txt_thnk_fol_6, ref txt_thnk_sp_hi_6, ref txt_thnk_sp_low_6, ref com_order_6, ref com_work_6, 5);
            Get_txt(dt, com_n_T7, ref txt_thnk_set_7, ref txt_thnk_low_7, ref txt_thnk_fol_7, ref txt_thnk_sp_hi_7, ref txt_thnk_sp_low_7, ref com_order_7, ref com_work_7, 6);
            Get_txt(dt, com_n_T8, ref txt_thnk_set_8, ref txt_thnk_low_8, ref txt_thnk_fol_8, ref txt_thnk_sp_hi_8, ref txt_thnk_sp_low_8, ref com_order_8, ref com_work_8, 7);


        }
        public void Get_txt(DataTable dt, ComboBox comTankName, ref HMITextBoxInput txt_MixWeight, ref HMITextBoxInput txt_LowWeight, ref TextBox txt_FreeFallWeight, ref HMITextBoxInput txt_HighSpeed, ref HMITextBoxInput txt_LowSpeed, ref HMITextBoxInput num_Orders, ref HMIComboBoxInput com_Werking, int x)
        {

            comTankName.Text = dt.Rows[x]["TankName"].ToString();
            txt_MixWeight.Text = dt.Rows[x]["MixWeight"].ToString();
            txt_LowWeight.Text = dt.Rows[x]["LowWeight"].ToString();
            txt_FreeFallWeight.Text = dt.Rows[x]["FreeFallWeight"].ToString();
            txt_HighSpeed.Text = dt.Rows[x]["HighSpeed"].ToString();
            txt_LowSpeed.Text = dt.Rows[x]["LowSpeed"].ToString();
            com_Werking.Text = dt.Rows[x]["Working"].ToString();
            num_Orders.Text = dt.Rows[x]["Orders"].ToString();


        }


        private void FillCurrentStudent()
        {
            try
            {
                if (comBatchName.Text == "System.Data.DataRowView")
                {
                    return;
                }

                Get_BatchsDetails(comBatchName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        #endregion

        private void comBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_BatchName.Text = comBatchName.Text;
            // FillCurrentStudent();

        }

        private void comBatchName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode && IsHandleCreated)
                {
                    if (comBatchName.Text == "System.Data.DataRowView")
                    {
                        return;
                    }

                    if (SqlDb.Get_BatchsDetails(comBatchName.Text).Rows.Count > 0)
                    {
                        Tools.Tools.ListTankName.Clear();
                        DataTable dt = new DataTable();
                        dt.Clear();

                        dt = SqlDb.Get_BatchsDetails(comBatchName.Text);
                        for (int i = 0; i <= 7; i++)
                        {
                            Tools.Tools.ListTankName.Add(dt.Rows[i]["TankName"].ToString());
                        }


                        FillCurrentStudent();
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Label2_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                DataTable dt = new DataTable();
                // If SqlDb.GetRecors("select * from Tanks").Rows.Count > 0 Then

                dt.Clear();

                dt = SqlDb.GetRecors("select * from Tanks");
                x = dt.Rows.Count + 1;
                SqlDb.ADD_Tanks(x, Txt_TankName.Text);
                Txt_TankName.Clear();
                Txt_TankName.Focus();
                // End If


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_BatchName.Text))
                {
                    MessageBox.Show("احذر خانت الاسم فارغة", " خانت الاسم فارغة", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                string sqlstr = "SELECT * FROM Batchs WHERE BatchName =@BatchName";
                using (SqlCommand cmd = new SqlCommand(sqlstr, SqlDb.con))
                {
                    cmd.Parameters.AddWithValue("@BatchName", SqlDbType.VarChar).Value = txt_BatchName.Text;

                    if (SqlDb.con.State == ConnectionState.Open)
                    {
                        SqlDb.con.Close();
                    }
                    SqlDb.con.Open();

                    using (SqlDataReader dr1 = cmd.ExecuteReader())
                    {

                        dr1.Read();

                        if (dr1.HasRows)
                        {
                            MessageBox.Show("الاسم مشابة لخلطة اخرة", "احذر", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }

                    SqlDb.con.Close();
                }
                string test = SqlDb.GET_LAST_Batchs_ID().Rows[0][0].ToString();
                int x = int.Parse(test);
                SqlDb.ADD_Batchs(x, txt_BatchName.Text);
                SqlDb.ADD_Batchs_Details(x, 1, com_n_T1.Text, int.Parse(txt_thnk_set_1.Text), int.Parse(txt_thnk_low_1.Text), int.Parse(txt_thnk_fol_1.Text), int.Parse(txt_thnk_sp_hi_1.Text), int.Parse(txt_thnk_sp_low_1.Text), int.Parse(com_order_1.Text), com_work_1.Text);
                SqlDb.ADD_Batchs_Details(x, 2, com_n_T2.Text, int.Parse(txt_thnk_set_2.Text), int.Parse(txt_thnk_low_2.Text), int.Parse(txt_thnk_fol_2.Text), int.Parse(txt_thnk_sp_hi_2.Text), int.Parse(txt_thnk_sp_low_2.Text), int.Parse(com_order_2.Text), com_work_2.Text);
                SqlDb.ADD_Batchs_Details(x, 3, com_n_T3.Text, int.Parse(txt_thnk_set_3.Text), int.Parse(txt_thnk_low_3.Text), int.Parse(txt_thnk_fol_3.Text), int.Parse(txt_thnk_sp_hi_3.Text), int.Parse(txt_thnk_sp_low_3.Text), int.Parse(com_order_3.Text), com_work_3.Text);
                SqlDb.ADD_Batchs_Details(x, 4, com_n_T4.Text, int.Parse(txt_thnk_set_4.Text), int.Parse(txt_thnk_low_4.Text), int.Parse(txt_thnk_fol_4.Text), int.Parse(txt_thnk_sp_hi_4.Text), int.Parse(txt_thnk_sp_low_4.Text), int.Parse(com_order_4.Text), com_work_4.Text);
                SqlDb.ADD_Batchs_Details(x, 5, com_n_T5.Text, int.Parse(txt_thnk_set_5.Text), int.Parse(txt_thnk_low_5.Text), int.Parse(txt_thnk_fol_5.Text), int.Parse(txt_thnk_sp_hi_5.Text), int.Parse(txt_thnk_sp_low_5.Text), int.Parse(com_order_5.Text), com_work_5.Text);
                SqlDb.ADD_Batchs_Details(x, 6, com_n_T6.Text, int.Parse(txt_thnk_set_6.Text), int.Parse(txt_thnk_low_6.Text), int.Parse(txt_thnk_fol_6.Text), int.Parse(txt_thnk_sp_hi_6.Text), int.Parse(txt_thnk_sp_low_6.Text), int.Parse(com_order_6.Text), com_work_6.Text);
                SqlDb.ADD_Batchs_Details(x, 7, com_n_T7.Text, int.Parse(txt_thnk_set_7.Text), int.Parse(txt_thnk_low_7.Text), int.Parse(txt_thnk_fol_7.Text), int.Parse(txt_thnk_sp_hi_7.Text), int.Parse(txt_thnk_sp_low_7.Text), int.Parse(com_order_7.Text), com_work_7.Text);
                SqlDb.ADD_Batchs_Details(x, 8, com_n_T8.Text, int.Parse(txt_thnk_set_8.Text), int.Parse(txt_thnk_low_8.Text), int.Parse(txt_thnk_fol_8.Text), int.Parse(txt_thnk_sp_hi_8.Text), int.Parse(txt_thnk_sp_low_8.Text), int.Parse(com_order_8.Text), com_work_8.Text);
                update_base();
                MessageBox.Show("تمت العملية بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SqlDb.con.Close();
            }

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {

                int BatchID = Convert.ToInt32(SqlDb.GetRecors(
                    $"SELECT * FROM Batchs WHERE BatchName='{comBatchName.Text}'").Rows[0][0]);

                SqlDb.UPDATE_Batchs_Details(BatchID, 1, com_n_T1.Text, int.Parse(txt_thnk_set_1.Text), int.Parse(txt_thnk_low_1.Text), int.Parse(txt_thnk_fol_1.Text), int.Parse(txt_thnk_sp_hi_1.Text), int.Parse(txt_thnk_sp_low_1.Text), com_work_1.Text, int.Parse(com_order_1.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 2, com_n_T2.Text, int.Parse(txt_thnk_set_2.Text), int.Parse(txt_thnk_low_2.Text), int.Parse(txt_thnk_fol_2.Text), int.Parse(txt_thnk_sp_hi_2.Text), int.Parse(txt_thnk_sp_low_2.Text), com_work_2.Text, int.Parse(com_order_2.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 3, com_n_T3.Text, int.Parse(txt_thnk_set_3.Text), int.Parse(txt_thnk_low_3.Text), int.Parse(txt_thnk_fol_3.Text), int.Parse(txt_thnk_sp_hi_3.Text), int.Parse(txt_thnk_sp_low_3.Text), com_work_3.Text, int.Parse(com_order_3.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 4, com_n_T4.Text, int.Parse(txt_thnk_set_4.Text), int.Parse(txt_thnk_low_4.Text), int.Parse(txt_thnk_fol_4.Text), int.Parse(txt_thnk_sp_hi_4.Text), int.Parse(txt_thnk_sp_low_4.Text), com_work_4.Text, int.Parse(com_order_4.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 5, com_n_T5.Text, int.Parse(txt_thnk_set_5.Text), int.Parse(txt_thnk_low_5.Text), int.Parse(txt_thnk_fol_5.Text), int.Parse(txt_thnk_sp_hi_5.Text), int.Parse(txt_thnk_sp_low_5.Text), com_work_5.Text, int.Parse(com_order_5.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 6, com_n_T6.Text, int.Parse(txt_thnk_set_6.Text), int.Parse(txt_thnk_low_6.Text), int.Parse(txt_thnk_fol_6.Text), int.Parse(txt_thnk_sp_hi_6.Text), int.Parse(txt_thnk_sp_low_6.Text), com_work_6.Text, int.Parse(com_order_6.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 7, com_n_T7.Text, int.Parse(txt_thnk_set_7.Text), int.Parse(txt_thnk_low_7.Text), int.Parse(txt_thnk_fol_7.Text), int.Parse(txt_thnk_sp_hi_7.Text), int.Parse(txt_thnk_sp_low_7.Text), com_work_7.Text, int.Parse(com_order_7.Text));
                SqlDb.UPDATE_Batchs_Details(BatchID, 8, com_n_T8.Text, int.Parse(txt_thnk_set_8.Text), int.Parse(txt_thnk_low_8.Text), int.Parse(txt_thnk_fol_8.Text), int.Parse(txt_thnk_sp_hi_8.Text), int.Parse(txt_thnk_sp_low_8.Text), com_work_8.Text, int.Parse(com_order_8.Text));

                btn_edit.Enabled = false;
                btn_edit.Text = "تعديل";

                btn_lode.Enabled = true;
                MessageBox.Show("تمت العملية بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void update_base()
        {

            comBatchName.DataSource = SqlDb.GET_ALL_BatchName();
            comBatchName.DisplayMember = "BatchName";
            comBatchName.ValueMember = "BatchID";
        }

        private void lbl_btn_del_Click(object sender, EventArgs e)
        {
            SqlDb.Del_BatchName(comBatchName_2.Text);
            update_base();
        }

        private void btn_lode_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
