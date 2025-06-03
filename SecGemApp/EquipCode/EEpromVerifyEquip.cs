using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace SecGemApp.EquipCode
{
    public class EEpromVerifyEquip
    {
        public const string BASE_LOG_MMDDATA_PATH = "D:\\EVMS\\LOG\\MMD_DATA";
        public List<MesEEpromCsvData> VMesEEpromData { get; set; } = new List<MesEEpromCsvData>();
        public EEpromVerifyEquip()
        {
            VMesEEpromData.Clear();
        }

        public bool SaveExcelData(string LotData, List<MesEEpromCsvData> CsvData)
        {
            DateTime currentDate = DateTime.Now; ;// DateTime.Today;
            DateTime startDate = currentDate; // 시작 날짜는 오늘


            string basePath = BASE_LOG_MMDDATA_PATH;  //@"D:\EVMS\LOG\MMD_DATA";

            string searchFileName = SanitizeFileName(LotData); // <- 바코드에서 특수문자 삭제
            if (searchFileName.Length < 1)
            {
                return false;
            }
            string _time = currentDate.ToString("_HHmmss"); //underbar 추가

            searchFileName += _time + ".csv";


            string year = currentDate.ToString("yyyy");
            string month = currentDate.ToString("MM");
            string day = currentDate.ToString("dd");

            string fullPath = Path.Combine(basePath, year, month, day);
            // aaa.csv 파일 경로 생성
            string targetFilePath = Path.Combine(fullPath, searchFileName);

            if (CsvData.Count < 1)
            {
                return false;
            }
            try
            {
                //string filePath = string.Format(@"{0}\30.csv", Application.StartupPath); //file path
                WriteCsvFromList(targetFilePath, CsvData);// CsvRead_MMd_DataList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SaveExcelData: {ex.Message}");
                return false;
            }
            


            return true;
        }
        public static string SanitizeFileName(string fileName)
        {
            // 윈도우에서 사용 불가능한 문자 목록을 가져옴
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // 사용 불가능한 문자를 빈 문자열로 대체하여 제거
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            // 파일명이 공백이 되지 않도록 기본값 설정
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "default_filename";

            return fileName;
        }

        private void WriteCsvFromList(string filePath, List<MesEEpromCsvData> dataList)
        {

            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath)); // 임시 파일 생성

            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath)) // 폴더가 존재하지 않으면
            {
                Directory.CreateDirectory(directoryPath); // 폴더 생성
            }

            

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",", //  콤마(,) 구분자 적용
                TrimOptions = TrimOptions.Trim // 공백 자동 제거
            }))
            {
                csv.WriteHeader<MesEEpromCsvData>(); //  헤더 작성
                csv.NextRecord(); //  다음 줄로 이동
                csv.WriteRecords(dataList); //  데이터 작성
            }
        }
    }
}
