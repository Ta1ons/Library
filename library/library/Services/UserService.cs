using Library.Models;
using System;
using System.Collections.Generic;
using System.IO;


namespace Library.Services
{
    class UserService
    {
        private string borrowerspath = @"C:\Gitprojects\Library\library\library\Data\Borrowers.txt";
        private string historypath = @"C:\Gitprojects\Library\library\library\Data\BookHistory.txt";
        
        private void AddHistory(History history)
        {
            var historyString = ConvertHistoryToString(history);

            if (!File.Exists(historypath))
            {
                File.Create(historypath);
            }

            using StreamWriter sw = File.AppendText(historypath);
            {
                sw.WriteLine(historyString);
            }
        }

        public List<Borrower> SearchBorrowers(string searchCriteria)
        {
            var returnList = new List<Borrower>();

            string[] lines = File.ReadAllLines(borrowerspath);

            foreach (string line in lines)
            {
                var borrower = ConvertStringToBorrower(line);

                if (borrower.name.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                {
                    returnList.Add(borrower);
                }
            }
            return returnList;
        }

        public void CreateLend(Book book, Borrower borrower)
        {
            var TodaysDate = DateTime.Now;
            string dateIn = "";

            string[] lines = File.ReadAllLines(historypath);
            int historyID = 0;

            foreach (string line in lines)
            {
                string[] stringToList = line.Split(",");
                int.TryParse(stringToList[0], out int number);
                if (number > historyID) //make sure this is the largest number
                {
                    historyID = number;
                }
            }

            historyID ++;

            string history = String.Join(',', new string[] { historyID.ToString(), book.bookID.ToString(), borrower.ID.ToString(), TodaysDate.ToString(), dateIn });

            if (!File.Exists(historypath))
            {
                File.Create(historypath);
            }

            using StreamWriter sw = File.AppendText(historypath);
            {
                sw.WriteLine(history);
            }
        }

        public List<History> GetLendHistory(int bookID)
        {
            var historyList = GetAllLendHistory();

            foreach ( History history in historyList)
            {
                if (history.bookID == bookID)
                {
                    historyList.Add(history);
                }
            }

            return historyList;
        }

        public List<History> GetAllLendHistory()
        {
            var returnList = new List<History>();
            string[] lines = File.ReadAllLines(historypath);

            foreach (string line in lines)
            {
                var book = ConvertStringToHistory(line);
                returnList.Add(book);
            }

            return returnList;
        }

        public void ReturnABook(Book book)
        {
            var allBookHistory = GetAllLendHistory();

            foreach (var bookhist in allBookHistory)
            {
                if (book.bookID == bookhist.bookID && !bookhist.dateIn.HasValue)
                {
                    bookhist.dateIn = DateTime.Now;
                }
            }

            File.Create(historypath).Close();

            foreach (History item in allBookHistory)
            {
                AddHistory(item);
            }
        }

        private Borrower ConvertStringToBorrower(string line)
        {
            string[] stringToList = line.Split(",");

            Borrower borrower = new Borrower();

            int.TryParse(stringToList[0], out borrower.ID);
            borrower.name = stringToList[1];

            return borrower;
        }

        private History ConvertStringToHistory(string line)
        {
            string[] stringToHistory = line.Split(",");

            History history = new History();

            int.TryParse(stringToHistory[0], out history.historyID);
            int.TryParse(stringToHistory[1], out history.userID);
            int.TryParse(stringToHistory[2], out history.bookID);
            DateTime.TryParse(stringToHistory[3], out DateTime dateTimeOut);
            history.dateOut = dateTimeOut;
            
            if (string.IsNullOrEmpty(stringToHistory[4]))
            {
                history.dateIn = null;
            }
            else
            {
                DateTime.TryParse(stringToHistory[4], out DateTime dateTimeIn);
                history.dateIn = dateTimeIn;
            }

            return history;
        }

        private string ConvertHistoryToString(History history)
        {
            return String.Join(',', new string[] { history.historyID.ToString(), history.bookID.ToString(), history.userID.ToString(), history.dateOut.ToString(), history.dateIn.ToString() });
        }

        public List<Borrower> GetBorrowers()
        {
            var returnList = new List<Borrower>();
            string[] lines = File.ReadAllLines(borrowerspath);

            foreach (string line in lines)
            {
                var borrower = ConvertStringToBorrower(line);
                returnList.Add(borrower);
            }
            return returnList;
        }
    }
}
