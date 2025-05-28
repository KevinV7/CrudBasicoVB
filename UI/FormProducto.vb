Imports System.Data
Imports System.Data.SqlClient

Public Class FormProducto

    Private Sub FormProducto_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CargarProductos()
    End Sub

    Private Sub CargarProductos()
        Try
            dgvProductos.DataSource = ProductoDAL.ObtenerTodos()
            LimpiarCampos()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInsertar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertar.Click
        Try
            ProductoDAL.Insertar(
                txtNombre.Text,
                CDec(txtpPrecio.Text),
                CInt(txtStock.Text)
            )
            MessageBox.Show("Producto insertado con éxito.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarProductos()
        Catch ex As Exception
            MessageBox.Show($"Error al insertar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnModificar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnModificar.Click
        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MessageBox.Show("Selecciona primero un producto del listado.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ProductoDAL.Modificar(
                CInt(txtId.Text),
                txtNombre.Text,
                CDec(txtpPrecio.Text),
                CInt(txtStock.Text)
            )
            MessageBox.Show("Producto modificado con éxito.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarProductos()
        Catch ex As Exception
            MessageBox.Show($"Error al modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MessageBox.Show("Selecciona primero un producto del listado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Seguro que deseas eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                ProductoDAL.Eliminar(CInt(txtId.Text))
                MessageBox.Show("Producto eliminado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarProductos()
            Catch ex As Exception
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub dgvProductos_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvproductos.SelectionChanged
        If dgvproductos.CurrentRow Is Nothing Then Return

        With dgvproductos.CurrentRow.Cells
            txtId.Text = .Item("IdProducto").Value.ToString()
            txtNombre.Text = .Item("Nombre").Value.ToString()

            ' Precio puede venir DBNull si la tabla lo permite
            Dim rawPrecio = .Item("Precio").Value
            If Not IsDBNull(rawPrecio) Then
                txtpPrecio.Text = CDec(rawPrecio).ToString("F2")
            Else
                txtpPrecio.Clear()
            End If

            ' Stock puede venir DBNull si la tabla lo permite
            Dim rawStock = .Item("Stock").Value
            If Not IsDBNull(rawStock) Then
                txtStock.Text = rawStock.ToString()
            Else
                txtStock.Clear()
            End If
        End With
    End Sub

    Private Sub LimpiarCampos()
        txtId.Clear()
        txtNombre.Clear()
        txtpPrecio.Clear()
        txtStock.Clear()
    End Sub

End Class
