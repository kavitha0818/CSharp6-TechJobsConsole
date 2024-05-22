using System;
using System.Text;

namespace TechJobsConsoleAutograded6
{
	public class JobData
	{
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();
            return AllJobs;
        }

        /*
         * Returns a list of all values contained in a given column,
         * without duplicates. 
         */

        /**
         * Search all columns for the given term
         */

        //TODO: Complete the FindByValue method
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();

            // List to store results
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            // Iterate through each job dictionary
            foreach (Dictionary<string, string> job in AllJobs)
            {
                // Iterate through each key-value pair in the job dictionary
                foreach (KeyValuePair<string, string> keyvaluepair in job)
                {
                    // Check if the value of any key contains the search term (case-insensitive)
                    if (keyvaluepair.Value.ToLower().Contains(value.ToLower()))
                    {
                        // Check if the job is already in the results to avoid duplicates
                        if (!results.Contains(job))
                        {
                            results.Add(job);
                        }
                        break; // Break the inner loop since the job is already added
                    }
                }
            }

            return results;
        }

        /**
         * Returns results of search the jobs data by key/value, using
         * inclusion of the search term.
         *
         * For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */

         //return a list of dictionaries
        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)
        {
            // load data, if not already loaded
            LoadData();
         // This line declares anew list name jobs
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> row in AllJobs)
            {
                string aValue = row[column];


                //TODO: Make search case-insensitive
                if (aValue.ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(row);
                }
            }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()
        {

            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                } 
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         */ 


        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }

        internal static List<string> FindAll(string columnChoice)
        {
            throw new NotImplementedException();
        }
    }
};