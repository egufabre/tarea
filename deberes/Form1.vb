Imports System.Data.Odbc

Public Class Form1

    Private StringParaConectar As String = "DRIVER=MySQL ODBC 8.0 UNICODE Driver;UID=root;PWD=EGUfabre924960;PORT=3306;DATABASE=deberes;SERVER=localhost"

    Private lector As OdbcDataReader

    Private Sub tomarDatos()
        Dim conexion As New OdbcConnection(StringParaConectar)
        conexion.Open()

        Dim comando As New OdbcCommand
        comando.CommandText = "SELECT * FROM persona"

        comando.Connection = conexion

        Me.lector = comando.ExecuteReader()

    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        Dim conexion As New OdbcConnection(StringParaConectar)
        conexion.Open()

        Dim comando As New OdbcCommand
        If txtMail.Text = "" Then
            comando.CommandText = "INSERT INTO PERSONA(id,nombre,apellido) VALUES(" + txtId.Text + ",'" + txtNombre.Text + "','" + txtApellido.Text + "')"
        Else
            comando.CommandText = "INSERT INTO PERSONA(id,nombre,apellido,mail) VALUES(" + txtId.Text + ",'" + txtNombre.Text + "','" + txtApellido.Text + "','" + txtMail.Text + "')"

        End If
        comando.Connection = conexion
        conexion.Close()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim conexion As New OdbcConnection(StringParaConectar)
        conexion.Open()

        Dim comando As New OdbcCommand

        comando.CommandText = "UPDATE persona SET nombre = '" + txtNombre.Text + "', apellido = '" + txtApellido.Text + "', mail = '" + txtMail.Text + "' WHERE id = " + txtId.Text + ""
        comando.Connection = conexion
        comando.ExecuteNonQuery()

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim conexion As New OdbcConnection(StringParaConectar)
        conexion.Open()

        Dim comando As New OdbcCommand

        comando.CommandText = "DELETE from persona WHERE id = " + txtId.Text + ""
        comando.Connection = conexion
        comando.ExecuteNonQuery()


    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        tomarDatos()

    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click

        lector.Read()
        txtId.Text = lector(0).ToString()
        txtNombre.Text = lector(1).ToString()
        txtApellido.Text = lector(2).ToString()
        txtMail.Text = lector(3).ToString()

    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        tomarDatos()
        Dim tabla As New DataTable

        tabla.Load(Me.lector)
        GrillaDeDatos.DataSource = tabla
    End Sub

    Private Sub btnContar_Click(sender As Object, e As EventArgs) Handles btnContar.Click
        Dim conexion As New OdbcConnection(StringParaConectar)
        conexion.Open()

        Dim comando As New OdbcCommand

        comando.CommandText = "SELECT  COUNT(*) FROM persona"

        MsgBox(comando.CommandText)
        comando.Connection = conexion

        MsgBox("Cantidad de personas: " + comando.ExecuteScalar().ToString)
        conexion.Close()
    End Sub
End Class
