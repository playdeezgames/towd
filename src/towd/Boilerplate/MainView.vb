Friend Class MainView
    Inherits Window
    Private ReadOnly dialogView As DialogView
    Public ReadOnly Context As IUIContext = New UIContext
    Public Sub New()
        MyBase.New()
        dialogView = New DialogView(Me)

        ShowState(Nothing, New SplashUIDialog(Context))
    End Sub

    Public Sub ShowState(gameState As String, Optional dialog As IUIDialog = Nothing)
        Context.Dialog = dialog
        RemoveAll()
        If Context.Dialog IsNot Nothing Then
            Add(dialogView)
            dialogView.UpdateView()
        Else
            Application.RequestStop()
        End If
    End Sub
End Class
