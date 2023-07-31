using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;

namespace OrdersLibrary
{
    public class Orders
    {
        private DataSet _dataSet;
        private SqlDataAdapter _sqlDataAdapter;
        private DataTable _dataTable;

        public Orders(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            FillOrderDataSet(connection);
            SetUpPrimaryKey();
            SetUpInsertOrderStoredProcedure(connection);
        }

        public int Add(Order order) {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var newRow = _dataTable.NewRow();
            newRow["Status"] = order.Status;
            newRow["CreatedDate"] = order.CreatedDate;
            newRow["UpdatedDate"] = order.UpdatedDate;
            newRow["ProductId"] = order.ProductId;
            _dataTable.Rows.Add(newRow);

            new SqlCommandBuilder(_sqlDataAdapter);
            _sqlDataAdapter.Update(_dataSet, "Order");

            return (int)newRow["Id"];
        }

        public Order Get(int id)
        {
            var orderRow = _dataTable.Rows.Find(id);

            if (orderRow == null)
                throw new KeyNotFoundException();

            return new Order
            {
                Id = id,
                Status = (string)orderRow["Status"],
                CreatedDate = (DateTime)orderRow["CreatedDate"],
                UpdatedDate = (DateTime)orderRow["UpdatedDate"],
                ProductId = (int)orderRow["ProductId"]
            };
        }

        public void Remove(int id)
        {
            var orderRow = _dataTable.Rows.Find(id);

            if (orderRow == null)
                throw new KeyNotFoundException();

            _dataTable.Rows.Remove(orderRow);
            new SqlCommandBuilder(_sqlDataAdapter);
            _sqlDataAdapter.Update(_dataSet, "Order");
        }

        public void Update(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var orderRow = _dataTable.Rows.Find(order.Id);

            if (orderRow == null)
                throw new KeyNotFoundException();

            orderRow["Status"] = order.Status;
            orderRow["CreatedDate"] = order.CreatedDate;
            orderRow["UpdatedDate"] = order.UpdatedDate;
            orderRow["ProductId"] = order.ProductId;

            _dataTable.AcceptChanges();
            new SqlCommandBuilder(_sqlDataAdapter);
            _sqlDataAdapter.Update(_dataSet, "Order");
        }

        public IEnumerable<Order> GetAll(int month = 0, int year = 0, string status = null, int productId = 0)
        {
            if (month > 0)
            {
                _sqlDataAdapter.SelectCommand.Parameters.Add(
                   new SqlParameter("@Month", SqlDbType.Int)).Value = month;
            }
            if (year > 0)
            {
                _sqlDataAdapter.SelectCommand.Parameters.Add(
                   new SqlParameter("@Year", SqlDbType.Int)).Value = year;
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                _sqlDataAdapter.SelectCommand.Parameters.Add(
                   new SqlParameter("@Status", SqlDbType.NVarChar, 50)).Value = status;
            }
            if (productId > 0)
            {
                _sqlDataAdapter.SelectCommand.Parameters.Add(
                   new SqlParameter("@ProductId", SqlDbType.Int)).Value = productId;
            }

            var fetchDataSet = new DataSet();
            try
            {
                _sqlDataAdapter.Fill(fetchDataSet, "Order");
                _sqlDataAdapter.SelectCommand.Parameters.Clear();
                foreach (DataRow orderRow in fetchDataSet.Tables["Order"].Rows)
                {
                    yield return new Order()
                    {
                        Id = (int)orderRow["Id"],
                        Status = (string)orderRow["Status"],
                        CreatedDate = (DateTime)orderRow["CreatedDate"],
                        UpdatedDate = (DateTime)orderRow["UpdatedDate"],
                        ProductId = (int)orderRow["ProductId"]
                    };
                }
            }
            finally
            {
                fetchDataSet.Dispose();
            }
        }

        private void FillOrderDataSet(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("dbo.FetchOrders", connection);
            command.CommandType = CommandType.StoredProcedure;
            _sqlDataAdapter = new SqlDataAdapter();
            _sqlDataAdapter.SelectCommand = command;
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet, "Order");
        }

        private void SetUpPrimaryKey()
        {
            _dataTable = _dataSet.Tables["Order"];
            var primaryKey = _dataTable.Columns[0];
            primaryKey.AutoIncrement = true;
            _dataTable.PrimaryKey = new DataColumn[] { primaryKey };
        }

        private void SetUpInsertOrderStoredProcedure(SqlConnection connection)
        {
            _sqlDataAdapter.InsertCommand = new SqlCommand("dbo.InsertOrder", connection);
            _sqlDataAdapter.InsertCommand.CommandType = CommandType.StoredProcedure;

            _sqlDataAdapter.InsertCommand.Parameters.Add(
               new SqlParameter("@Status", SqlDbType.NVarChar, 50,
               "Status"));
            _sqlDataAdapter.InsertCommand.Parameters.Add(
               new SqlParameter("@CreatedDate", SqlDbType.DateTime, 0, "CreatedDate"));
            _sqlDataAdapter.InsertCommand.Parameters.Add(
               new SqlParameter("@UpdatedDate", SqlDbType.DateTime, 0, "UpdatedDate"));
            _sqlDataAdapter.InsertCommand.Parameters.Add(
               new SqlParameter("@ProductId", SqlDbType.Int, 0, "ProductId"));

            SqlParameter parameter =
                _sqlDataAdapter.InsertCommand.Parameters.Add(
                "@Identity", SqlDbType.Int, 0, "Id");
            parameter.Direction = ParameterDirection.Output;
        }

    }
}
