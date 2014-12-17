﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSVLib;

namespace StudentRecordApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string filePath = "E:\\studentRecord.csv";
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string studentReg = studentRegTextBox.Text;
            string studentName = studentNameTextBox.Text;
            if(File.Exists(filePath))
            {
               
                bool uniqueReg = UniqueCheker(studentReg);

                if (uniqueReg == true)
                {
                    FileStream aFileStream = new FileStream(filePath, FileMode.Append);
                    CsvFileWriter aWriter = new CsvFileWriter(aFileStream);
                    List<string> aStudentRecord = new List<string>();
                    aStudentRecord.Add(studentReg);
                    aStudentRecord.Add(studentName);
                    aWriter.WriteRow(aStudentRecord);
                    aFileStream.Close();
                }
                else
                {
                    MessageBox.Show("Registration Number Already added");
                }
            }
            else
            {
                FileStream aFileStream = new FileStream(filePath, FileMode.Append);
                CsvFileWriter aWriter = new CsvFileWriter(aFileStream);
                List<string> aStudentRecord = new List<string>();
                aStudentRecord.Add(studentReg);
                aStudentRecord.Add(studentName);
                aWriter.WriteRow(aStudentRecord);
                aFileStream.Close();
            }
            
           
        }

        private bool UniqueCheker(string studentReg)
        {
            int studentRegInt = Convert.ToInt32(studentReg);
            FileStream aFileStream = new FileStream(filePath, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aStudentDataRead = new List<string>();
            showStudentDataListBox.Items.Clear();
            bool a = true;
            while (aCsvFileReader.ReadRow(aStudentDataRead))
            {
                int studentRegNo = Convert.ToInt32(aStudentDataRead[0]);
                if (studentRegInt == studentRegNo)
                {
                    a = false;
                    break;
                }
                else
                {
                    a = true;
                }
            }
            aFileStream.Close();
            return a; 
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            FileStream aFileStream = new FileStream(filePath, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aStudentDataRead = new List<string>();
            showStudentDataListBox.Items.Clear();
            while (aCsvFileReader.ReadRow(aStudentDataRead))
            {
                string studentRegNo = aStudentDataRead[0];
                string studentName = aStudentDataRead[1];
                showStudentDataListBox.Items.Add(studentRegNo + " " + studentName);
            }
            aFileStream.Close();
        }
    }
}
