Imports System.Windows.Forms

Public Class FormMenu

    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Menú Principal"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.IsMdiContainer = True   ' Activa modo MDI (opcional)
    End Sub

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        Dim frm As New FormCliente()
        frm.MdiParent = Me        ' Quita esta línea si no usas MDI
        frm.Show()
    End Sub

    Private Sub btnProductos_Click(sender As Object, e As EventArgs) Handles btnProductos.Click
        Dim frm As New FormProducto()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btnFacturas_Click(sender As Object, e As EventArgs) Handles btnFacturas.Click
        Dim frm As New FormFactura()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btnDetalle_Click(sender As Object, e As EventArgs) Handles btnDetalle.Click
        Dim frm As New FormDetalle()
        frm.MdiParent = Me
        frm.Show()
    End Sub

End Class
