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

        private void FillOrderDataSet(SqlConnection connection)
        {
            string commandString = "SELECT * FROM [dbo].[Order]";
            SqlCommand command = new SqlCommand(commandString, connection);
            _sqlDataAdapter = new SqlDataAdapter(command);
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
               new SqlParameter("@CreatedDate", SqlDbType.NVarChar, 50,
               "CreatedDate"));
            _sqlDataAdapter.InsertCommand.Parameters.Add(
               new SqlParameter("@UpdatedDate", SqlDbType.NVarChar, 50,
               "UpdatedDate"));
            _sqlDataAdapter.InsertCommand.Parameters.Add(
               new SqlParameter("@ProductId", SqlDbType.NVarChar, 50,
               "ProductId"));

            SqlParameter parameter =
                _sqlDataAdapter.InsertCommand.Parameters.Add(
                "@Identity", SqlDbType.Int, 0, "Id");
            parameter.Direction = ParameterDirection.Output;
        }
    }
}
