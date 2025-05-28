Imports System.Data
Imports System.Data.SqlClient

Public Class DetalleDAL

        ''' <summary>
        ''' Inserta un nuevo detalle utilizando el SP InsertarDetalle.
        ''' </summary>
        Public Shared Sub Insertar(
            idFactura As Integer,
            idProducto As Integer,
            cantidad As Integer,
            total As Decimal)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("InsertarDetalle", cn)

                cmd.CommandType = CommandType.StoredProcedure
                With cmd.Parameters
                    .Add("@IdFactura", SqlDbType.Int).Value = idFactura
                    .Add("@IdProducto", SqlDbType.Int).Value = idProducto
                    .Add("@Cantidad", SqlDbType.Int).Value = cantidad

                    Dim p As SqlParameter = .Add("@Total", SqlDbType.Decimal)
                    p.Precision = 10
                    p.Scale = 2
                    p.Value = total
                End With

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Modifica un detalle existente mediante el SP ModificarDetalle.
        ''' </summary>
        Public Shared Sub Modificar(
            numeroDetalle As Integer,
            idFactura As Integer,
            idProducto As Integer,
            cantidad As Integer,
            total As Decimal)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("ModificarDetalle", cn)

                cmd.CommandType = CommandType.StoredProcedure
                With cmd.Parameters
                    .Add("@NumeroDetalle", SqlDbType.Int).Value = numeroDetalle
                    .Add("@IdFactura", SqlDbType.Int).Value = idFactura
                    .Add("@IdProducto", SqlDbType.Int).Value = idProducto
                    .Add("@Cantidad", SqlDbType.Int).Value = cantidad

                    Dim p As SqlParameter = .Add("@Total", SqlDbType.Decimal)
                    p.Precision = 10
                    p.Scale = 2
                    p.Value = total
                End With

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Elimina un detalle por su número utilizando el SP EliminarDetalle.
        ''' </summary>
        Public Shared Sub Eliminar(numeroDetalle As Integer)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("EliminarDetalle", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumeroDetalle", SqlDbType.Int).Value = numeroDetalle

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Recupera todos los detalles como DataTable (para bindear en un grid).
        ''' </summary>
        Public Shared Function ObtenerTodos() As DataTable
            Dim dt As New DataTable()

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("SELECT * FROM DETALLE", cn)

                cmd.CommandType = CommandType.Text
                cn.Open()
                Using rdr As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(rdr)
                End Using
            End Using

            Return dt
        End Function

    End Class

