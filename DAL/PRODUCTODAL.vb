Imports System.Data
Imports System.Data.SqlClient


Public Class ProductoDAL

        ''' <summary>
        ''' Inserta un nuevo producto utilizando el SP InsertarProducto.
        ''' </summary>
        Public Shared Sub Insertar(
            nombre As String,
            precio As Decimal,
            stock As Integer)

            Using cn As SqlConnection = ObtenerConexion(),
                  cmd As New SqlCommand("InsertarProducto", cn)

                cmd.CommandType = CommandType.StoredProcedure

                With cmd.Parameters
                    .Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre

                    Dim p As SqlParameter = .Add("@Precio", SqlDbType.Decimal)
                    p.Precision = 10
                    p.Scale = 2
                    p.Value = precio

                    .Add("@Stock", SqlDbType.Int).Value = stock
                End With

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Modifica un producto existente mediante el SP ModificarProducto.
        ''' </summary>
        Public Shared Sub Modificar(
            idProducto As Integer,
            nombre As String,
            precio As Decimal,
            stock As Integer)

            Using cn As SqlConnection = ObtenerConexion(),
                  cmd As New SqlCommand("ModificarProducto", cn)

                cmd.CommandType = CommandType.StoredProcedure

                With cmd.Parameters
                    .Add("@IdProducto", SqlDbType.Int).Value = idProducto
                    .Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre

                    Dim p As SqlParameter = .Add("@Precio", SqlDbType.Decimal)
                    p.Precision = 10
                    p.Scale = 2
                    p.Value = precio

                    .Add("@Stock", SqlDbType.Int).Value = stock
                End With

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        ''' <summary>
        ''' Elimina un producto mediante el SP EliminarProducto.
        ''' </summary>
        Public Shared Sub Eliminar(idProducto As Integer)
            Using cn As SqlConnection = ObtenerConexion(),
                  cmd As New SqlCommand("EliminarProducto", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>
        ''' Recupera todos los productos como DataTable.
        ''' </summary>
        Public Shared Function ObtenerTodos() As DataTable
            Dim dt As New DataTable()

            Using cn As SqlConnection = ObtenerConexion(),
                  cmd As New SqlCommand("SELECT * FROM PRODUCTO", cn)

                cmd.CommandType = CommandType.Text
                cn.Open()
                Using rdr As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(rdr)
                End Using
            End Using

            Return dt
        End Function

    End Class
