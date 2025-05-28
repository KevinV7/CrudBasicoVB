Imports System.Data
Imports System.Data.SqlClient


Public Class FormFactura

    Private Sub FormFactura_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CargarFacturas()
    End Sub

    Private Sub CargarFacturas()
        Try
            dgvFacturas.DataSource = FacturaDAL.ObtenerTodos()
            LimpiarCampos()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInsertar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertar.Click
        If String.IsNullOrWhiteSpace(txtIdCliente.Text) OrElse Not IsNumeric(txtIdCliente.Text) Then
            MessageBox.Show("Ingresa un IdCliente válido.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIdCliente.Focus()
            Return
        End If

        Try
            FacturaDAL.Insertar(
                CInt(txtIdCliente.Text),
                dtpFecha.Value
            )
            MessageBox.Show("Factura insertada con éxito.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarFacturas()
        Catch ex As Exception
            MessageBox.Show($"Error al insertar factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnModificar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnModificar.Click
        If String.IsNullOrWhiteSpace(txtIdFactura.Text) OrElse Not IsNumeric(txtIdFactura.Text) Then
            MessageBox.Show("Selecciona primero una factura del listado.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(txtIdCliente.Text) OrElse Not IsNumeric(txtIdCliente.Text) Then
            MessageBox.Show("Ingresa un IdCliente válido.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIdCliente.Focus()
            Return
        End If

        Try
            FacturaDAL.Modificar(
                CInt(txtIdFactura.Text),
                CInt(txtIdCliente.Text),
                dtpFecha.Value
            )
            MessageBox.Show("Factura modificada con éxito.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarFacturas()
        Catch ex As Exception
            MessageBox.Show($"Error al modificar factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If String.IsNullOrWhiteSpace(txtIdFactura.Text) OrElse Not IsNumeric(txtIdFactura.Text) Then
            MessageBox.Show("Selecciona primero una factura del listado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Seguro que deseas eliminar esta factura?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                FacturaDAL.Eliminar(CInt(txtIdFactura.Text))
                MessageBox.Show("Factura eliminada.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarFacturas()
            Catch ex As Exception
                MessageBox.Show($"Error al eliminar factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub dgvFacturas_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvFacturas.SelectionChanged
        If dgvFacturas.CurrentRow Is Nothing Then Return

        With dgvFacturas.CurrentRow.Cells
            txtIdFactura.Text = .Item("IdFactura").Value.ToString()
            txtIdCliente.Text = .Item("IdCliente").Value.ToString()

            Dim rawFecha = .Item("Fecha").Value
            If Not IsDBNull(rawFecha) Then
                dtpFecha.Value = CDate(rawFecha)
            Else
                dtpFecha.Value = Date.Now
            End If
        End With
    End Sub

    Private Sub LimpiarCampos()
        txtIdFactura.Clear()
        txtIdCliente.Clear()
        dtpFecha.Value = Date.Now
    End Sub

End Class