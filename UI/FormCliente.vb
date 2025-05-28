Imports System.Data
Imports System.Data.SqlClient


Public Class FormCliente
    Inherits System.Windows.Forms.Form



    ' ✔️ Constructor del formulario
    Public Sub New()
        InitializeComponent()
    End Sub

    ' Al cargar el formulario, llenamos el grid
    Private Sub FormCliente_Load(
        sender As Object,
        e As EventArgs
      ) Handles MyBase.Load

        CargarClientes()
        CargarCategorias()
    End Sub

    ' Método que obtiene el DataTable desde el DAL y lo asigna al DataGridView
    Private Sub CargarClientes()
        Try
            dgvClientes.DataSource = ClienteDAL.ObtenerTodos()
            LimpiarCampos()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarCategorias()
        Dim cats = ClienteDAL.ObtenerCategorias()
        cboCategoria.DataSource = cats
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub


    ' Inserta un cliente con los valores de los controles
    Private Sub btnInsertar_Click(
        sender As Object,
        e As EventArgs
      ) Handles btnInsertar.Click

        Try
            ClienteDAL.Insertar(
              txtNombre.Text,
              txtApellido.Text,
              txtDireccion.Text,
              dtpFechaNacimiento.Value,
              txtTelefono.Text,
              txtEmail.Text,
              cboCategoria.SelectedItem?.ToString()
            )
            MessageBox.Show("Cliente agregado con éxito.", "Insertar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarClientes()
        Catch ex As Exception
            MessageBox.Show($"Error al insertar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Modifica el cliente seleccionado usando el Id del txtId
    Private Sub btnModificar_Click(
        sender As Object,
        e As EventArgs
      ) Handles btnModificar.Click

        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MessageBox.Show("Selecciona primero un cliente del listado.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ClienteDAL.Modificar(
              CInt(txtId.Text),
              txtNombre.Text,
              txtApellido.Text,
              txtDireccion.Text,
              dtpFechaNacimiento.Value,
              txtTelefono.Text,
              txtEmail.Text,
              cboCategoria.SelectedItem?.ToString()
            )
            MessageBox.Show("Cliente modificado con éxito.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarClientes()
        Catch ex As Exception
            MessageBox.Show($"Error al modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Elimina el cliente seleccionado
    Private Sub btnEliminar_Click(
        sender As Object,
        e As EventArgs
      ) Handles btnEliminar.Click

        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MessageBox.Show("Selecciona primero un cliente del listado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Seguro que deseas eliminar este cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) =
           DialogResult.Yes Then

            Try
                ClienteDAL.Eliminar(CInt(txtId.Text))
                MessageBox.Show("Cliente eliminado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarClientes()
            Catch ex As Exception
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub

    ' Cada vez que cambia la selección en el grid, volcamos los datos a los controles
    Private Sub dgvClientes_SelectionChanged(
      sender As Object,
      e As EventArgs
    ) Handles dgvClientes.SelectionChanged

        If dgvClientes.CurrentRow Is Nothing Then Return

        With dgvClientes.CurrentRow.Cells
            txtId.Text = .Item("IdCliente").Value.ToString()
            txtNombre.Text = .Item("Nombre").Value.ToString()
            txtApellido.Text = .Item("Apellido").Value.ToString()
            txtDireccion.Text = .Item("Direccion").Value.ToString()

            ' --- Manejo de DBNull para FechaNacimiento ---
            Dim rawFecha = .Item("FechaNacimiento").Value
            If Not IsDBNull(rawFecha) Then
                dtpFechaNacimiento.Value = CDate(rawFecha)
            Else
                ' Por defecto hoy (o puedes usar MinDate, o deshabilitar el control, etc.)
                dtpFechaNacimiento.Value = Date.Today
            End If

            txtTelefono.Text = .Item("Telefono").Value.ToString()
            txtEmail.Text = .Item("Email").Value.ToString()
            cboCategoria.Text = .Item("Categoria").Value.ToString()
        End With

    End Sub

    ' Manejador para el botón “Limpiar”
    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimpiarCampos()
    End Sub



    ' Limpia todos los campos para ingresar un nuevo cliente
    Private Sub LimpiarCampos()
        txtId.Clear()
        txtNombre.Clear()
        txtApellido.Clear()
        txtDireccion.Clear()
        dtpFechaNacimiento.Value = Date.Today
        txtTelefono.Clear()
        txtEmail.Clear()
        cboCategoria.SelectedIndex = -1
    End Sub

End Class
