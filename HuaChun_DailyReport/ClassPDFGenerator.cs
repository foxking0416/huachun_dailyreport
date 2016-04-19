using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace HuaChun_DailyReport
{
    class ClassPDFGenerator
    {
        private Document docInitial;
        private iTextSharp.text.Font ChFont;
        private iTextSharp.text.Font ChFont_blue;
        private iTextSharp.text.Font ChFont_msg;
        private FileStream file;

        public ClassPDFGenerator()
        {
            this.initialSetting();
            this.setFont();
            this.createTable();
        }

        private void initialSetting()
        {
            this.docInitial = new Document(PageSize.A4, 50, 50, 80, 50);//iTextSharp 提供的class (page size, Margin left, right, top, down)
            //this.docInitial.SetPageSize(new Rectangle( 800, 600));
            this.file = new FileStream("D:\\MyPDF.pdf", FileMode.Create);

            PdfWriter PdfWriter = PdfWriter.GetInstance(docInitial, this.file);
        }

        private void setFont()
        {
            //設定中文字型
            BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            ChFont = new iTextSharp.text.Font(bfChinese, 8);

            ChFont_blue = new iTextSharp.text.Font(bfChinese, 40, iTextSharp.text.Font.NORMAL, new BaseColor(51, 0, 153));

            ChFont_msg = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.ITALIC, BaseColor.RED);
        }

        private void createTable()
        {
            string path = "D:\\12.jpg";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(path));

            //以下加入table
            float[] tableCol = new float[42];//總共N個column, 
            for (int i = 0; i < tableCol.Length; i++)
            {
                tableCol[i] = 1;//每個column的寬度比例
            }

            PdfPTable table = new PdfPTable(tableCol);
            table.TotalWidth = 400f;//整個table的總寬度
            table.LockedWidth = true;
            //table.AddCell(
            
            
            //PdfPCell header = new PdfPCell(new Phrase("Header", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 28f, iTextSharp.text.Font.BOLD)));
            //header.Colspan = 4;
            
            //jpg.ScaleToFit(50f, 50f);
            //header.AddElement(jpg);
            //header.AddElement(new Phrase("Test"));
            //table.AddCell(header);

 

            PdfPCell weekNo = new PdfPCell(new Phrase("週別", ChFont));
            weekNo.Colspan = 2;
            
            table.AddCell(weekNo);

            PdfPCell week1st = new PdfPCell(new Phrase("第一週", ChFont));
            week1st.Colspan = 7;
            week1st.PaddingLeft = 20;
            table.AddCell(week1st);

            PdfPCell week2nd = new PdfPCell(new Phrase("第二週", ChFont));
            week2nd.Colspan = 7;
            table.AddCell(week2nd);

            PdfPCell week3rd = new PdfPCell(new Phrase("第三週", ChFont));
            week3rd.Colspan = 7;
            table.AddCell(week3rd);

            PdfPCell week4th = new PdfPCell(new Phrase("第四週", ChFont));
            week4th.Colspan = 7;
            table.AddCell(week4th);

            PdfPCell week5th = new PdfPCell(new Phrase("第五週", ChFont));
            week5th.Colspan = 7;
            table.AddCell(week5th);

            PdfPCell week6th = new PdfPCell(new Phrase("第六週", ChFont));
            week6th.Colspan = 2;
            table.AddCell(week6th);

            PdfPCell numOfDays = new PdfPCell(new Phrase("天數", ChFont));
            numOfDays.Rowspan = 2;
            PdfPCell notCount = new PdfPCell(new Phrase("不計", ChFont));
            notCount.Rowspan = 2;
            PdfPCell workingDay = new PdfPCell(new Phrase("工作天", ChFont));
            workingDay.Rowspan = 2;
            table.AddCell(numOfDays);
            table.AddCell(notCount);
            table.AddCell(workingDay);



            PdfPCell week = new PdfPCell(new Phrase("星期", ChFont));
            week.Colspan = 2;
            table.AddCell(week);
            for (int i = 0; i < 5; i++)
            {
                table.AddCell(new PdfPCell(new Phrase("日", ChFont)));
                table.AddCell(new PdfPCell(new Phrase("一", ChFont)));
                table.AddCell(new PdfPCell(new Phrase("二", ChFont)));
                table.AddCell(new PdfPCell(new Phrase("三", ChFont)));
                table.AddCell(new PdfPCell(new Phrase("四", ChFont)));
                table.AddCell(new PdfPCell(new Phrase("五", ChFont)));
                table.AddCell(new PdfPCell(new Phrase("六", ChFont)));
            }

            table.AddCell(new PdfPCell(new Phrase("日", ChFont)));
            table.AddCell(new PdfPCell(new Phrase("一", ChFont)));

            PdfPCell year = new PdfPCell(new Phrase("104年", ChFont));
            year.Rowspan = 12;
            table.AddCell(year);

            string[] weekArray = new string[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };

            for (int j = 0; j < 12; j++)
            {
                PdfPCell weekCount = new PdfPCell(new Phrase(weekArray[j], ChFont));
                weekCount.MinimumHeight = 25;
                //weekCount.Height = new Rectangle(15,15).Height;
                table.AddCell(weekCount);
                //table.AddCell(new PdfPCell(new Phrase(weekArray[j], ChFont)));
                for (int i = 0; i < 40; i++)
                {
                    table.AddCell("");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////
            table.AddCell(new PdfPCell(new Phrase("A", ChFont)));
            PdfPCell contractDuration = new PdfPCell(new Phrase("合約工期", ChFont));
            contractDuration.Colspan = 8;
            table.AddCell(contractDuration);

            PdfPCell calenderDay = new PdfPCell(new Phrase("日曆天", ChFont));
            calenderDay.Colspan = 3;
            table.AddCell(calenderDay);

            PdfPCell contractStartEnd = new PdfPCell(new Phrase("開工完工", ChFont));
            contractStartEnd.Colspan = 15;
            table.AddCell(contractStartEnd);

            PdfPCell blank1 = new PdfPCell(new Phrase("", ChFont));
            blank1.Colspan = 10;
            table.AddCell(blank1);


            table.AddCell("");
            table.AddCell("");
            PdfPCell blank2 = new PdfPCell(new Phrase("", ChFont));
            blank2.Colspan = 3;
            blank2.Rowspan = 8;
            table.AddCell(blank2);


            //////////////////////////////////////////////////////////////////////////////
            table.AddCell(new PdfPCell(new Phrase("B", ChFont)));
            PdfPCell realDuration = new PdfPCell(new Phrase("實際完工日期", ChFont));
            realDuration.Colspan = 8;
            table.AddCell(realDuration);

            PdfPCell days = new PdfPCell(new Phrase("天", ChFont));
            days.Colspan = 3;
            table.AddCell(days);

            PdfPCell realStartEnd = new PdfPCell(new Phrase("自 至", ChFont));
            realStartEnd.Colspan = 15;
            table.AddCell(realStartEnd);


            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");
            //table.AddCell(blank2);
            //////////////////////////////////////////////////////////////////////////////
            PdfPCell C = new PdfPCell(new Phrase("C", ChFont));
            C.Rowspan = 4;
            table.AddCell(C);

            PdfPCell deductDays = new PdfPCell(new Phrase("扣除工作天", ChFont));
            deductDays.Rowspan = 4;
            table.AddCell(deductDays);

            PdfPCell stopByReason = new PdfPCell(new Phrase("因故停工", ChFont));
            stopByReason.Colspan = 5;
            table.AddCell(stopByReason);

            PdfPCell days2 = new PdfPCell(new Phrase("天", ChFont));
            days2.Colspan = 2;
            table.AddCell(days2);

            PdfPCell total = new PdfPCell(new Phrase("合計", ChFont));
            total.Rowspan = 4;
            table.AddCell(total);

            PdfPCell totalDays = new PdfPCell(new Phrase("天", ChFont));
            totalDays.Colspan = 2;
            totalDays.Rowspan = 4;
            table.AddCell(totalDays);

            PdfPCell notCountReport = new PdfPCell(new Phrase("不計工期文：", ChFont));
            notCountReport.Colspan = 15;
            table.AddCell(notCountReport);

            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");
            //////////////////////////////////////////////////////////////////////////////
            PdfPCell holidays = new PdfPCell(new Phrase("國定假日", ChFont));
            holidays.Colspan = 5;
            table.AddCell(holidays);

            PdfPCell days3 = new PdfPCell(new Phrase("天", ChFont));
            days3.Colspan = 2;
            table.AddCell(days3);

            PdfPCell blank3 = new PdfPCell(new Phrase("", ChFont));
            blank3.Colspan = 15;

            table.AddCell(blank3);
            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");
            //////////////////////////////////////////////////////////////////////////////
            PdfPCell rainyDay = new PdfPCell(new Phrase("雨天", ChFont));
            rainyDay.Colspan = 5;
            table.AddCell(rainyDay);

            PdfPCell days4 = new PdfPCell(new Phrase("天", ChFont));
            days4.Colspan = 2;

            table.AddCell(days4);
            table.AddCell(blank3);

            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");
            //////////////////////////////////////////////////////////////////////////////
            PdfPCell extend = new PdfPCell(new Phrase("准延日期", ChFont));
            extend.Colspan = 5;
            table.AddCell(extend);

            PdfPCell days5 = new PdfPCell(new Phrase("天", ChFont));
            days5.Colspan = 2;

            table.AddCell(days4);
            table.AddCell(blank3);

            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");
            //////////////////////////////////////////////////////////////////////////////
            table.AddCell(new PdfPCell(new Phrase("D", ChFont)));
            PdfPCell realworkingDay = new PdfPCell(new Phrase("B - C 實際工作天", ChFont));
            realworkingDay.Colspan = 8;
            table.AddCell(realworkingDay);

            PdfPCell days6 = new PdfPCell(new Phrase("天", ChFont));
            days6.Colspan = 3;
            table.AddCell(days6);

            table.AddCell(blank3);
            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");
            //////////////////////////////////////////////////////////////////////////////
            table.AddCell(new PdfPCell(new Phrase("E", ChFont)));
            PdfPCell lastworkingDay = new PdfPCell(new Phrase("A - D 逾期天數", ChFont));
            lastworkingDay.Colspan = 8;
            table.AddCell(lastworkingDay);

            PdfPCell days7 = new PdfPCell(new Phrase("天", ChFont));
            days7.Colspan = 3;
            table.AddCell(days7);

            table.AddCell(blank3);
            table.AddCell(blank1);
            table.AddCell("");
            table.AddCell("");



            PdfPCell jpgCell = new PdfPCell(jpg);
            jpgCell.Colspan = 2;
            jpgCell.Rowspan = 1;
            jpgCell.BackgroundColor = new BaseColor(255, 255, 255);
            jpgCell.Padding = 15f;


            //table.AddCell(jpgCell);

            //table.WriteSelectedRows(

            PdfContentByte over;// = PdfWriter.direcncontent;


            docInitial.Open();
            docInitial.Add(table);


            docInitial.Close();
        }





    }
}
