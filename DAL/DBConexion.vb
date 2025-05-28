

Imports System.Data.SqlClient

Module ConexionDB
    ' Ajusta el Data Source si usas otra instancia
    Public ReadOnly connectionString As String =
      "Server=localhost\PRUEBAS;Database=PruebaC;Integrated Security=True;"

    Public Function ObtenerConexion() As SqlConnection
        Return New SqlConnection(connectionString)
    End Function
End Module
