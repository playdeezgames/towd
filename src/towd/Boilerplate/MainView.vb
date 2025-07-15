Friend Class MainView
    Inherits Window
    Private ReadOnly dialogView As DialogView
    Public ReadOnly Context As IUIContext = New UIContext
    Public Sub New()
        MyBase.New()
        dialogView = New DialogView(Me)

        ShowState(New SplashUIDialog(Context))
    End Sub

    Public Sub ShowState(dialog As IUIDialog)
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
