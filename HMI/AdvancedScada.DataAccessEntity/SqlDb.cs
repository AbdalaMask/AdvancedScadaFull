using AdvancedScada.DataAccessEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedScada.DataAccessEntity
{
    public class SqlDb
    {
        private BatchsDbContext db = new BatchsDbContext();

        private Batchs BTH = new Batchs();
        private BatchsDetails BTHD = new BatchsDetails();
        private BatchFinal GetBatchFinal = new BatchFinal();

        public SqlDb()
        {

        }


        #region اضافة تركيبة وتعديلها


        public int ADD_Batchs(string BatchName)
        {
            var BatchID = db.Batchs.Max(b => b.BatchID) + 1;
            BTH.BatchName = BatchName;
            BTH.BatchID = BatchID;
            db.Batchs.Add(BTH);
            db.SaveChanges();
            return BatchID;

        }
        public void ADD_Batchs_Details(int BatchID, int TankID, string TankName, int MixWeight, int LowWeight, int FreeFallWeight, int HighSpeed, int LowSpeed, int Orders, string Working)
        {
            try
            {

                var newBTH = db.Batchs.SingleOrDefault((i) => i.BatchID == BatchID);
                BTHD.Batchs = newBTH;
                BTHD.BatchID = BatchID;
                BTHD.TankID = TankID;
                BTHD.TankName = TankName;
                BTHD.MixWeight = MixWeight;
                BTHD.LowWeight = LowWeight;
                BTHD.FreeFallWeight = FreeFallWeight;
                BTHD.HighSpeed = HighSpeed;
                BTHD.LowSpeed = LowSpeed;
                BTHD.Orders = Orders;
                BTHD.Working = Working;
                db.BatchsDetails.Add(BTHD);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }

        }

        public void UPDATE_Batchs_Details(int BatchID, int TankID, string TankName, int MixWeight, int LowWeight, int FreeFallWeight, int HighSpeed, int LowSpeed, int Orders, string Working)
        {
            try
            {

                var newBTH = db.Batchs.SingleOrDefault((i) => i.BatchID == BatchID);
                BTHD.Batchs = newBTH;
                BTHD.BatchID = BatchID;
                BTHD.TankID = TankID;
                BTHD.TankName = TankName;
                BTHD.MixWeight = MixWeight;
                BTHD.LowWeight = LowWeight;
                BTHD.FreeFallWeight = FreeFallWeight;
                BTHD.HighSpeed = HighSpeed;
                BTHD.LowSpeed = LowSpeed;
                BTHD.Orders = Orders;
                BTHD.Working = Working;
                db.BatchsDetails.Add(BTHD);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }

        }

        #endregion
        #region حذف
        /// <summary>
        /// حذف اسم الباتشة
        /// </summary>
        /// <param name="BatchName"></param>
        private void Delete_Batch(string BatchName)
        {

            var BTH = db.Batchs.SingleOrDefault((i) => i.BatchName == BatchName);
            db.Batchs.Remove(BTH);
            db.SaveChanges();

        }
        /// <summary>
        /// حذف جميع بيانات الخلاطات
        /// </summary>
        private void Delete_Batchs()
        {

            foreach (var item in db.Batchs)
            {
                db.Batchs.Remove(item);
            }
            db.SaveChanges();

        }
        /// <summary>
        /// حذف التقرير كل
        /// </summary>
        private void Delete_BatchFinal()
        {

            foreach (var item in db.BatchFinal)
            {
                db.BatchFinal.Remove(item);
            }
            db.SaveChanges();

        }

        #endregion





        private void FillCurrentBatchs(string BatchName)
        {
            try
            {


                var list = db.Batchs.Where((i) => i.BatchName.Contains(BatchName)).ToList();

            }
            catch (Exception ex)
            {

            }
        }


        public IList<string> List()
        {
            return (from BTH in db.Batchs select BTH.BatchName).Distinct().ToArray();

        }
        //public void Get_txt(ComboBox comBatchName, ComboBox comTankName, ref TextBox txt_MixWeight, ref TextBox txt_LowWeight, ref TextBox txt_FreeFallWeight, ref TextBox txt_HighSpeed, ref TextBox txt_LowSpeed, ref TextBox num_Orders, ref ComboBox com_Werking, int x)
        //{

        //    var C = (
        //        from Sal in context.BatchsDetails
        //        join BTH in context.Batchs on Sal.BatchID equals (BTH.BatchID)
        //        where BTH.BatchID == xid
        //        select new
        //        {
        //            Sal = BTH.BatchName,
        //            Sal.BatchID,
        //            Sal.TankName,
        //            Sal.MixWeight,
        //            Sal.LowWeight,
        //            Sal.FreeFallWeight,
        //            Sal.HighSpeed,
        //            Sal.LowSpeed,
        //            Sal.Orders,
        //            Sal.Working
        //        }).ToList();

        //    comTankName.Text = C[x].TankName;
        //    txt_MixWeight.Text = C[x].MixWeight;
        //    txt_LowWeight.Text = C[x].LowWeight;
        //    txt_FreeFallWeight.Text = C[x].FreeFallWeight;
        //    txt_HighSpeed.Text = C[x].HighSpeed;
        //    txt_LowSpeed.Text = C[x].LowSpeed;
        //    num_Orders.Text = C[x].Orders;
        //    com_Werking.Text = C[x].Working;


        //}

    }
}
