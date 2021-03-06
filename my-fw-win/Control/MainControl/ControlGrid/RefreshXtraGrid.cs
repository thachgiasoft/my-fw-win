using System;
using System.Data;
using System.Threading;
using DevExpress.XtraGrid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class RefreshXtraGrid
    {
        //refresh thread cho việc thực thi auto refresh data.
        private Thread thread = null;

        public delegate DataSet delegateGetDataset();
        public delegate void delegateRefreshDisplayDataset(GridControl grid, DataSet dataSet);

        public RefreshXtraGrid(GridControl grid, int refreshTime, delegateGetDataset dlgDataset, delegateRefreshDisplayDataset dlgRefreshData)
        {
            this.thread =
               new Thread(delegate()
               {
                   try
                   {                      
                       while (true)
                       {
                           Thread.Sleep(refreshTime);
                           DataSet ds=dlgDataset();//lấy dữ liệu trong mạc trình refresh thread
                           setDisplayData(grid, ds, dlgRefreshData);//thiết đặt gọi an toàn
                       }
                   }
                   catch (ThreadAbortException ex)
                   {
                       PLException.AddException(ex);
                   }
               });
            this.thread.Name = "refreshThread";
            this.thread.Start();
        }

        private  delegate void delegateSetDisplayData(GridControl grid, DataSet dataSet, delegateRefreshDisplayDataset dlgRefreshData);
        private void setDisplayData(GridControl grid, DataSet ds, delegateRefreshDisplayDataset dlgRefreshData)
        {
            try
            {
                if (grid.InvokeRequired)
                {
                    //1-đang là refresh Thread
                    grid.Invoke(new delegateSetDisplayData(setDisplayData), grid, ds, dlgRefreshData);
                    //2-chuyển qua main Thread
                }
                else
                {
                    //3-đang là main Thread, lúc này là an toàn, muốn gọi gì cũng được                                        
                    if (grid.IsDisposed == true)//kiểm tra hủy rferesh thread khi đóng form, nếu như reresh thread vẫn running background                        
                        thread.Abort();//tung biệt lệ và kết thúc
                    dlgRefreshData(grid, ds);
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        /// <summary>Dừng mạch trình refresh data
        /// </summary>
        public void stopAutoRefreshData()
        {
            try
            {
                if (this.thread != null)
                {
                    this.thread.Abort();
                }
            }
            catch (Exception ex) {                
                PLException.AddException(ex);
            }
        }
    }
}
