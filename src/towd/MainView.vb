Imports towd.business
Imports towd.ui

Friend Class MainView
    Inherits Window
    Private ReadOnly dialogView As DialogView
    Public ReadOnly Context As IUIContext(Of IWorld) = New UIContext
    Public Sub New()
        MyBase.New()
        dialogView = New DialogView(Me)
        Add(dialogView)
        Context.Dialog = New SplashUIDialog(Context)
        dialogView.UpdateView()
    End Sub
End Class
