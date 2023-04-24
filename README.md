# ADO.NET Helper para Oracle (Update)

Todos los programas que utilizan las clases e interfaces del proveedor predeterminado de ADO.NET para Oracle que se encuentran en el ensamblado System.Data.Oraclient deben actualizarse por las clases e interfaces que se encuentran en el ensamblado Oracle.DataAccess.Client, que es el proveedor nativo de Oracle para ADO.NET, este ensamblado se descarga del siguiente enlace Oracle Data Provider for .NET



A continuación el código actualizado de las clases Helper de ADO.NET que publique en este post.

El código de la clase OracleDataBase



El código de la clase OracleDataBaseCommand
<pre>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace OracleHelper
{
    internal sealed class OracleDataBaseCommand
    {

internal static int Insert(string commandText, Dictionary parameters, 
System.Data.CommandType cmdtype)
{
            
int resp = 0;
try
{
    using (OracleConnection conn = OracleDataBase.GetInstance().GetConnection())
    {
        using (OracleCommand cmd = new OracleCommand(commandText, conn))
        {
            cmd.CommandType = cmdtype;
            if (parameters != null)
            {
                foreach (KeyValuePair pair in parameters)
                {
                    cmd.Parameters.Add(new OracleParameter(pair.Key, pair.Value));
                }
            }
            resp = cmd.ExecuteNonQuery();
        }
    }
}
catch (OracleException ex)
{
    Logger.LogWriteError(ex.Message);
    throw ex;
}
return resp;
}
internal static int Update(string commandText, Dictionary parameters, 
System.Data.CommandType cmdtype)
{
            
try
{
    return Insert(commandText, parameters, cmdtype);
}
catch (OracleException ex)
{
    Logger.LogWriteError(ex.Message);
    throw ex;
}
}
internal static OracleDataReader GetReader(string commandText, Dictionary parameters,
System.Data.CommandType cmdtype)
{
OracleDataReader reader = null;
try
{
    OracleConnection conn = OracleDataBase.GetInstance().GetConnection();
    using (OracleCommand cmd = new OracleCommand(commandText, conn)) {
        if (parameters != null)
        {
            foreach (KeyValuePair pair in parameters)
            {
                cmd.Parameters.Add(new OracleParameter(pair.Key, pair.Value));
            }
        }
        reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }
}
catch (Exception ex) {
    Logger.LogWriteError(ex.Message);
    throw ex;
}
return reader;
}

internal OracleDataReader GetParameterizedReader(string commandText, OracleParameter[] parameters, 
System.Data.CommandType cmdtype)
{
OracleDataReader reader = null;
OracleConnection conn = OracleDataBase.GetInstance().GetConnection();
using (OracleCommand cmd = new OracleCommand(commandText, conn))
{
    cmd.CommandType = cmdtype;
    if (parameters != null)
        cmd.Parameters.AddRange(parameters);
    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
}
return reader;
}
}
}
</pre>
Como ejemplo del uso del helper, tenemos el código de la clase CustomerDAC cuya funcionalidad es tener los métodos CRUD para una entidad Customer.



El código de la clase Customer



Para finalizar el código de una aplicación de consola que muestra el uso de la clase CustomerDAC.

