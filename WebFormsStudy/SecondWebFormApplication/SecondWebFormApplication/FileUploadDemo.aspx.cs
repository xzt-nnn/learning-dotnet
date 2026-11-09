using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class FileUploadDemo : System.Web.UI.Page
    {
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // 1. 判断用户有没有选中文件
            if (!fuFile.HasFile)
            {
                lblMsg.Text = "❌ 请先选择文件！";
                return;
            }

            //2,获取文件信息
            string fileName = fuFile.FileName; //获取文件名（带后缀）
            Console.WriteLine("文件名：" + fileName);
            int fileSize = fuFile.PostedFile.ContentLength; //获取文件大小（单位：字节）
            Console.WriteLine("文件大小：" + fileSize + "字节");
            string ext = Path.GetExtension(fileName).ToLower(); //获取文件后缀名
            Console.WriteLine("文件后缀：" + ext);

            // 3. 限制：只允许图片，限制大小（2MB以内）
            if (fileSize > 2 * 1024 * 1024)
            {
                lblMsg.Text = "❌ 文件不能超过2MB！";
                return;
            }
            if (ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                lblMsg.Text = "❌ 只允许jpg/png/gif图片！";
                return;
            }

            // 4. 保存到服务器的文件夹
            // Server.MapPath("~/Upload") 把网站相对路径转成服务器真实物理路径
            string saveDir = Server.MapPath("~/Upload");
            // 如果文件夹不存在，自动创建
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            // 生成新文件名，防止重名覆盖（时间戳+后缀）
            string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}{ext}";
            string fullPath = Path.Combine(saveDir, newFileName);

            // 保存文件
            fuFile.SaveAs(fullPath);

            lblMsg.Text = $"✅ 上传成功！原文件名：{fileName}，新文件名：{newFileName}";
        }
    }
}