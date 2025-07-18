Imports towd.business
Imports towd.ui

Friend Class MainView
    Inherits Window
    Private ReadOnly dialogView As DialogView
    Public ReadOnly Context As IUIContext(Of IWorld) = New UIContext(New Persister)
    Public Sub New()
        MyBase.New()
        dialogView = New DialogView(Me)
        Add(dialogView)
        dialogView.UpdateView()
    End Sub
End Class
