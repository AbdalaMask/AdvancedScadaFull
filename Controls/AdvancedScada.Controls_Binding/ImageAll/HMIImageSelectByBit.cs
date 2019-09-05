using System.Drawing;
using System.Windows.Forms;

namespace AdvancedScada.Controls_Binding.ImageAll
{
    public class HMIImageSelectByBit : PictureBox
    {
        #region Properties
        private bool m_ValueSelect1;
        public bool ValueSelect1
        {
            get
            {
                return m_ValueSelect1;
            }
            set
            {
                if (value != m_ValueSelect1)
                {
                    m_ValueSelect1 = value;

                    RefreshImage();
                }
            }
        }

        private bool m_ValueSelect2;
        public bool ValueSelect2
        {
            get
            {
                return m_ValueSelect2;
            }
            set
            {
                if (value != m_ValueSelect2)
                {
                    m_ValueSelect2 = value;

                    RefreshImage();
                }
            }
        }

        //*****************************************
        //* Property - Image to Show when All is off
        //*****************************************
        //Private m_LightOnColor As Color = Color.Green
        private Bitmap m_GraphicAllOff;
        public Bitmap GraphicAllOff
        {
            get
            {
                return (m_GraphicAllOff);
            }
            set
            {
                m_GraphicAllOff = value;
                RefreshImage();
            }
        }

        //*****************************************
        //* Property - Image to Show when
        //*****************************************
        private Bitmap m_GraphicSelect1;
        public Bitmap GraphicSelect1
        {
            get
            {
                return (m_GraphicSelect1);
            }
            set
            {
                m_GraphicSelect1 = value;
                RefreshImage();
            }
        }

        //*****************************************
        //* Property - Image to Show when
        //*****************************************
        private Bitmap m_GraphicSelect2;
        public Bitmap GraphicSelect2
        {
            get
            {
                return (m_GraphicSelect2);
            }
            set
            {
                m_GraphicSelect2 = value;
                RefreshImage();
            }
        }
        #endregion

        #region Private Methods
        private void RefreshImage()
        {
            if (m_ValueSelect1)
            {
                if (m_GraphicSelect1 != null)
                {
                    Image = m_GraphicSelect1;
                }
            }
            else if (m_ValueSelect2)
            {
                if (m_GraphicSelect2 != null)
                {
                    Image = m_GraphicSelect2;
                }
            }
            else
            {
                if (m_GraphicAllOff != null)
                {
                    Image = m_GraphicAllOff;
                }
            }

            Invalidate();
        }
        #endregion
    }

}