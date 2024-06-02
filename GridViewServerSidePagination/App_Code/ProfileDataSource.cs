using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GridViewServerSidePagination.App_Code
{
    [DataObject(true)]
    public class ProfileDataSource
    {
        public ProfileDataSource()
        {
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Int32 TotalRowCount(Int32 startRowIndex, Int32 pageSize, String sortExpression)
        {
            Int32 intTotalProfile = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString))
            {
                SqlCommand cmdSelect = new SqlCommand();

                conn.Open();
                cmdSelect.CommandText = "Profile_Total";
                cmdSelect.CommandType = CommandType.StoredProcedure;
                cmdSelect.Connection = conn;

                SqlDataReader dataReader = cmdSelect.ExecuteReader();

                dataReader.Read();
                intTotalProfile = Convert.ToInt32(dataReader[0]);
            }
            return intTotalProfile;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetProfileData(Int32 startRowIndex, Int32 pageSize, String sortExpression)
        {
            DataTable profileDataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                SqlCommand cmdSelect = new SqlCommand();

                conn.Open();
                cmdSelect.CommandText = "Profile_GET";
                cmdSelect.CommandType = CommandType.StoredProcedure;
                cmdSelect.Connection = conn;

                startRowIndex = Convert.ToInt32(startRowIndex / pageSize) + 1;

                if (String.IsNullOrEmpty(sortExpression))
                    sortExpression = "ProfileId";

                cmdSelect.Parameters.AddWithValue("@CurrentPage", startRowIndex);
                cmdSelect.Parameters.AddWithValue("@PageSize", pageSize);
                cmdSelect.Parameters.AddWithValue("@SortExpression", sortExpression);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmdSelect;

                dataAdapter.Fill(profileDataTable);
            }
            return profileDataTable;
        }
    }
}