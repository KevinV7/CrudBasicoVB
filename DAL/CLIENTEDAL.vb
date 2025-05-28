Imports System.Data
Imports System.Data.SqlClient


Public Class ClienteDAL

        ''' <summary>
        ''' Inserta un nuevo cliente pasando todos los campos como parámetros.
        ''' </summary>
        Public Shared Sub Insertar(
            nombre As String,
            apellido As String,
            direccion As String,
            fechaNacimiento As Date,
            telefono As String,
            email As String,
            categoria As String)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("InsertarCliente", cn)

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nombre
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = apellido
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = direccion
                cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = fechaNacimiento
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 15).Value = telefono
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email
                cmd.Parameters.Add("@Categoria", SqlDbType.VarChar, 20).Value = categoria

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>
        ''' Modifica un cliente pasando todos los campos como parámetros.
        ''' </summary>
        Public Shared Sub Modificar(
            idCliente As Integer,
            nombre As String,
            apellido As String,
            direccion As String,
            fechaNacimiento As Date,
            telefono As String,
            email As String,
            categoria As String)

            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("ModificarCliente", cn)

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nombre
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = apellido
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = direccion
                cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = fechaNacimiento
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 15).Value = telefono
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email
                cmd.Parameters.Add("@Categoria", SqlDbType.VarChar, 20).Value = categoria

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>
        ''' Elimina un cliente por su Id.
        ''' </summary>
        Public Shared Sub Eliminar(idCliente As Integer)
            Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
                  cmd As New SqlCommand("EliminarCliente", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Sub

    ''' <summary>
    ''' Recupera todos los clientes como DataTable (sin usar DTO).
    ''' </summary>
    Public Shared Function ObtenerTodos() As DataTable
        Dim dt As New DataTable()

        Using cn As SqlConnection = ConexionDB.ObtenerConexion(),
    cmd As New SqlCommand("SELECT * FROM CLIENTE", cn)

            cmd.CommandType = CommandType.Text
            cn.Open()

            Using rdr As SqlDataReader = cmd.ExecuteReader()
                dt.Load(rdr)   ' Carga directamente todo el reader en el DataTable
            End Using

        End Using

        Return dt
    End Function


    Public Shared Function ObtenerCategorias() As List(Of String)
        Dim lista As New List(Of String)
        Using cn = ConexionDB.ObtenerConexion(),
        cmd = New SqlCommand(
          "SELECT DISTINCT Categoria 
             FROM CLIENTE 
            WHERE Categoria IS NOT NULL", cn)

            cmd.CommandType = CommandType.Text
            cn.Open()
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    lista.Add(rdr.GetString(0))
                End While
            End Using
        End Using
        Return lista
    End Function


End Class

