using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace Samples.Data.OracleHelper
{
    internal class OracleDataBaseCommand
    {
        internal static int Insert(string commandText,
            Dictionary<string, object> parameters,
            System.Data.CommandType cmdtype)
        {
            int resp = 0;
            using (OracleConnection conn = OracleDataBase.GetConnection())
            {
                using (OracleCommand cmd = new OracleCommand(commandText, conn))
                {
                    cmd.CommandType = cmdtype;
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string,object> pair in parameters)
                        {
                            cmd.Parameters.Add(new OracleParameter(pair.Key, pair.Value));
                        }
                    }
                    resp = cmd.ExecuteNonQuery();
                }
            }
            return resp;
        }

        internal static int Update(string commandText,
            Dictionary<string, object> parameters,
            System.Data.CommandType cmdType)
        {
            return Insert(commandText, parameters, cmdType);
        }

        internal static OracleDataReader GetReader(string commandText,
                Dictionary<string,object> parameters, System.Data.CommandType cmdtype)
        {
            OracleDataReader reader = null;
            OracleConnection conn = OracleDataBase.GetConnection();
            using (OracleCommand cmd = new OracleCommand(commandText, conn))
            {
                if (parameters != null)
                {
                    foreach (KeyValuePair<string,object> pair in parameters)
                    {
                        cmd.Parameters.Add(new OracleParameter(pair.Key, pair.Value));
                    }
                }
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            return reader;
        }


    }
}
