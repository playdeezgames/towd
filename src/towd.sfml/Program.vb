Imports System
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window

Module Program
    Sub Main(args As String())
        Dim window As RenderWindow = New RenderWindow(New VideoMode(1280UI, 720UI), "OHAI!")
        Dim rectangleSize As Vector2f = New Vector2f(100, 100)
        Dim rectangle As RectangleShape = New RectangleShape(rectangleSize)
        Dim view As New View(New FloatRect(0F, 0F, 320.0F, 180.0F))
        rectangle.FillColor = Color.Blue
        AddHandler window.KeyPressed, AddressOf OnKeyPressed
        AddHandler window.Resized, AddressOf OnResized
        AddHandler window.Closed, AddressOf OnClosed
        AddHandler window.GainedFocus, AddressOf OnGainedFocus
        AddHandler window.LostFocus, AddressOf OnLostFocus
        AddHandler window.TextEntered, AddressOf OnTextEntered
        window.SetView(view)
        While window.IsOpen
            window.DispatchEvents()
            window.Clear(Color.Black)
            window.Draw(rectangle)
            window.Display()
        End While
    End Sub

    Private Sub OnTextEntered(sender As Object, e As TextEventArgs)
    End Sub

    Private Sub OnLostFocus(sender As Object, e As EventArgs)
    End Sub

    Private Sub OnGainedFocus(sender As Object, e As EventArgs)
    End Sub

    Private Sub OnClosed(sender As Object, e As EventArgs)
        Dim window As Window = CType(sender, Window)
        window.Close()
    End Sub

    Private Sub OnResized(sender As Object, e As SizeEventArgs)
    End Sub

    Private Sub OnKeyPressed(sender As Object, e As KeyEventArgs)
        Dim window As Window = CType(sender, Window)
        If e.Code = Keyboard.Key.Escape Then
            window.Close()
        End If
    End Sub
End Module
