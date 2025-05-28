Imports System.Data
Imports System.Data.SqlClient

Public Class FacturaDAL

        ''' <summary>
        ''' Inserta una nueva factura utilizando el SP InsertarFactura.
        ''' </summary>
        Public Shared Sub Insertar(
            idCliente As Integer,
            fecha As DateTime)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("InsertarFactura", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Modifica una factura existente mediante el SP ModificarFactura.
        ''' </summary>
        Public Shared Sub Modificar(
            idFactura As Integer,
            idCliente As Integer,
            fecha As DateTime)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("ModificarFactura", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Elimina una factura por su Id utilizando el SP EliminarFactura.
        ''' </summary>
        Public Shared Sub Eliminar(idFactura As Integer)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("EliminarFactura", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Recupera todas las facturas como DataTable (para bindear en un grid).
        ''' </summary>
        Public Shared Function ObtenerTodos() As DataTable
            Dim dt As New DataTable()

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("SELECT * FROM FACTURA", cn)

                cmd.CommandType = CommandType.Text
                cn.Open()

                Using rdr As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(rdr)
                End Using
            End Using

            Return dt
        End Function

    End Class

