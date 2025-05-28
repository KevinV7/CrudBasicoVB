Imports System.Data
Imports System.Data.SqlClient


Public Class FormDetalle

    Private Sub FormDetalle_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CargarDetalle()
    End Sub

    Private Sub CargarDetalle()
        Try
            dgvDetalle.DataSource = DetalleDAL.ObtenerTodos()
            LimpiarCampos()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar detalles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInsertar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertar.Click
        ' Validar IdFactura
        If String.IsNullOrWhiteSpace(txtIdFactura.Text) OrElse Not IsNumeric(txtIdFactura.Text) Then
            MessageBox.Show("Ingresa un IdFactura válido.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIdFactura.Focus()
            Return
        End If
        ' Validar IdProducto
        If String.IsNullOrWhiteSpace(txtIdProducto.Text) OrElse Not IsNumeric(txtIdProducto.Text) Then
            MessageBox.Show("Ingresa un IdProducto válido.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIdProducto.Focus()
            Return
        End If
        ' Validar Cantidad
        If String.IsNullOrWhiteSpace(txtCantidad.Text) OrElse Not IsNumeric(txtCantidad.Text) Then
            MessageBox.Show("Ingresa una Cantidad válida.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCantidad.Focus()
            Return
        End If
        ' Validar Total
        If String.IsNullOrWhiteSpace(txtTotal.Text) OrElse Not Decimal.TryParse(txtTotal.Text, Nothing) Then
            MessageBox.Show("Ingresa un Total válido.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTotal.Focus()
            Return
        End If

        Try
            DetalleDAL.Insertar(
                CInt(txtIdFactura.Text),
                CInt(txtIdProducto.Text),
                CInt(txtCantidad.Text),
                CDec(txtTotal.Text)
            )
            MessageBox.Show("Detalle insertado con éxito.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarDetalle()
        Catch ex As Exception
            MessageBox.Show($"Error al insertar detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnModificar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnModificar.Click
        ' Validar NumeroDetalle
        If String.IsNullOrWhiteSpace(txtnumero.Text) OrElse Not IsNumeric(txtnumero.Text) Then
            MessageBox.Show("Selecciona primero un detalle del listado.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Reusar validaciones de insertar
        btnInsertar_Click(sender, e)

        Try
            DetalleDAL.Modificar(
                CInt(txtnumero.Text),
                CInt(txtIdFactura.Text),
                CInt(txtIdProducto.Text),
                CInt(txtCantidad.Text),
                CDec(txtTotal.Text)
            )
            MessageBox.Show("Detalle modificado con éxito.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarDetalle()
        Catch ex As Exception
            MessageBox.Show($"Error al modificar detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If String.IsNullOrWhiteSpace(txtnumero.Text) OrElse Not IsNumeric(txtnumero.Text) Then
            MessageBox.Show("Selecciona primero un detalle del listado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Seguro que deseas eliminar este detalle?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                DetalleDAL.Eliminar(CInt(txtnumero.Text))
                MessageBox.Show("Detalle eliminado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarDetalle()
            Catch ex As Exception
                MessageBox.Show($"Error al eliminar detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub dgvDetalle_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvDetalle.SelectionChanged
        If dgvDetalle.CurrentRow Is Nothing Then Return

        With dgvDetalle.CurrentRow.Cells
            txtnumero.Text = .Item("NumeroDetalle").Value.ToString()
            txtIdFactura.Text = .Item("IdFactura").Value.ToString()
            txtIdProducto.Text = .Item("IdProducto").Value.ToString()
            txtCantidad.Text = .Item("Cantidad").Value.ToString()

            Dim rawTotal = .Item("Total").Value
            If Not IsDBNull(rawTotal) Then
                txtTotal.Text = CDec(rawTotal).ToString("F2")
            Else
                txtTotal.Clear()
            End If
        End With
    End Sub

    Private Sub LimpiarCampos()
        txtnumero.Clear()
        txtIdFactura.Clear()
        txtIdProducto.Clear()
        txtCantidad.Clear()
        txtTotal.Clear()
    End Sub

End Class
