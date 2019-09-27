using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    public class EmailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public class Email
        {
            public string FromAddress { get; set; }
            public string FromUsername { get; set; }
            public string FromPwd { get; set; }
            public string Subject { get; set; }
            public string FromHost { get; set; }
            public int? FromPort { get; set; }
            public string ToAddress { get; set; }

            /// <summary>
            /// 抄送
            /// </summary>
            public string CCAddress { get; set; }

            /// <summary>
            /// 邮件内容
            /// </summary>
            public string body { get; set; }

            public bool IsBodyHtml { get; set; }
        }

        ///<summary>
        /// 邮件的发送
        ///</summary>
        public static void Send(Email email)
        {
            try
            {

                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress from = new MailAddress(email.FromAddress);
                message.From = from;
                //设置收件人,可添加多个,添加方法与下面的一样
                message.To.Add(email.ToAddress);
                //设置抄送人
                if ((email.CCAddress ?? "").Trim().Length > 0)
                {
                    message.CC.Add(email.CCAddress);
                }
                //设置邮件标题
                message.Subject = email.Subject;
                //设置邮件内容
                message.Body = email.body;
                //
                message.IsBodyHtml = email.IsBodyHtml;
                //设置邮件发送服务器,
                SmtpClient client = null;
                if (email.FromPort != null)
                {
                    client = new SmtpClient(email.FromHost, (int)(email.FromPort));
                }
                else
                {
                    client = new SmtpClient(email.FromHost);
                }
                //设置发送人的邮箱账号和密码                
                client.Credentials = new NetworkCredential(email.FromUsername, email.FromPwd);
                //启用ssl,也就是安全发送
                client.EnableSsl = true;
                //发送邮件
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
